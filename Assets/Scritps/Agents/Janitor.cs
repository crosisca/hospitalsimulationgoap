using UnityEngine;

public class Janitor : GAgent
{
    new void Start ()
    {
        base.Start();

        SubGoal s1 = new SubGoal(AgentGoalName.Clean, 1, false);
        goals.Add(s1, 3);

        SubGoal s2 = new SubGoal(AgentGoalName.Relief, 1, false);
        goals.Add(s2, 2);

        SubGoal s3 = new SubGoal(AgentGoalName.Rested, 1, false);
        goals.Add(s3, 1);

        Invoke("GetTired", Random.Range(10, 20));
        Invoke("NeedRelief", Random.Range(10, 20));
    }

    void GetTired ()
    {
        beliefs.ModifyState(AgentBeliefs.Exhausted, 0);
        Invoke("GetTired", Random.Range(10, 20));
    }

    void NeedRelief ()
    {
        beliefs.ModifyState(AgentBeliefs.Busting, 0);
        Invoke("NeedRelief", Random.Range(10, 20));
    }
}