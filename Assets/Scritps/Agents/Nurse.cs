public class Nurse : GAgent
{
   new void Start ()
    {
        base.Start();
        SubGoal s1 = new SubGoal(AgentGoalName.TreatPatient, 1, true);
        goals.Add(s1, 3);
    }
}