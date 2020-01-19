public class Patient : GAgent
{
    new void Start()
    {
        base.Start();

        SubGoal s1 = new SubGoal(AgentGoalName.IsWaiting, 1, true);
        goals.Add(s1, 3);

        SubGoal s2 = new SubGoal(AgentGoalName.IsTreated, 1, true);
        goals.Add(s2, 5);
    }
}