using UnityEngine;

public class Doctor : GAgent
{
    new void Start ()
    {
        base.Start();

        SubGoal s1 = new SubGoal(AgentGoalName.Rested, 1, false);
        goals.Add(s1, 1);
        
        Invoke("GetTired", Random.Range(10, 20));
    }

    void GetTired ()
    {
        beliefs.ModifyState(AgentBeliefs.Exhausted, 0);
        Invoke("GetTired", Random.Range(10, 20));
    }
}