using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class SubGoal
{
    public Dictionary<string, int> sgoals;
    public bool remove;

    public SubGoal(string key, int value, bool remove)
    {
        sgoals = new Dictionary<string, int>();
        sgoals.Add(key, value);
        this.remove = remove;
    }
}

public class GAgent : MonoBehaviour
{
    public List<GAction> actions = new List<GAction>();
    public Dictionary<SubGoal, int> goals = new Dictionary<SubGoal, int>();
    public GInventory inventory = new GInventory();
    public WorldStates beliefs = new WorldStates();

    GPlanner planner;
    Queue<GAction> actionQueue;
    public GAction currentAction;
    SubGoal currentGoal;

    public void Start ()
    {
        GAction[] acts = this.GetComponents<GAction>();
        foreach (GAction action in acts)
            actions.Add(action);
    }


    bool invoked = false;

    void CompleteAction()
    {
        currentAction.running = false;
        currentAction.PostPerform();
        invoked = false;
    }

    void LateUpdate()
    {
        if (currentAction != null && currentAction.running)
        {
            float distanceToTarget = Vector3.Distance(currentAction.target.transform.position, this.transform.position);
            //HACK comentei o hasPath pq tava bugando
            if (/*currentAction.agent.hasPath && */distanceToTarget < 2f)//currentAction.agent.remainingDistance < 1f
            {
                if (!invoked)
                {
                    Invoke("CompleteAction", currentAction.duration);
                    invoked = true;
                }
            }
            return;
        }

        if (planner == null || actionQueue == null)
        {
            planner = new GPlanner();

            IOrderedEnumerable<KeyValuePair<SubGoal, int>> sortedGoals = from entry in goals orderby entry.Value descending select entry;
            IOrderedEnumerable<KeyValuePair<SubGoal, int>> sortGoals = goals.OrderByDescending(x => x.Value);

            foreach (KeyValuePair<SubGoal, int> subGoal in sortedGoals)
            {
                actionQueue = planner.Plan(actions, subGoal.Key.sgoals, beliefs);

                if (actionQueue != null)
                {
                    currentGoal = subGoal.Key;
                    break;
                }
            }
        }

        if (actionQueue != null && actionQueue.Count == 0)
        {
            if (currentGoal.remove)
                goals.Remove(currentGoal);

            planner = null;
        }

        if (actionQueue != null && actionQueue.Count > 0)
        {
            currentAction = actionQueue.Dequeue();
            if (currentAction.PrePerform())
            {
                if (currentAction.target == null && currentAction.targetTag != "")
                    currentAction.target = GameObject.FindGameObjectWithTag(currentAction.targetTag);

                if (currentAction.target != null)
                {
                    currentAction.running = true;
                    currentAction.agent.SetDestination(currentAction.target.transform.position);
                }
            }
            else
            {
                actionQueue = null;//any action failed, re-plan
            }
        }
    }
}