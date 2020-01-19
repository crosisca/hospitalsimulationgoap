using UnityEngine;

public class Nurse : GAgent
{
   new void Start ()
   {
        base.Start();

        SubGoal s1 = new SubGoal(AgentGoalName.TreatPatient, 1, false);
        goals.Add(s1, 3);

        SubGoal s2 = new SubGoal(AgentGoalName.Rested, 1, false);
        goals.Add(s2, 1);

       Invoke("GetTired", Random.Range(10, 20));
    }

    void GetTired()
    {
        beliefs.ModifyState(AgentBeliefs.Exhausted, 0);
        Invoke("GetTired", Random.Range(10, 20));
    }
}