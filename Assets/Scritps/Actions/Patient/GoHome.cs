public class GoHome : GAction
{
    public override bool PrePerform ()
    {
        beliefs.RemoveState(AgentBeliefs.AtHospital);
        return true;
    }

    public override bool PostPerform ()
    {
        Destroy(this.gameObject, 1);
        return true;
    }
}