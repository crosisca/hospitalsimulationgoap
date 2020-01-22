public class GoToToilet : GAction
{
    public override bool PrePerform ()
    {
        target = GWorld.Instance.GetQueue(ResourceType.Toilets).RemoveResource();
        if (target == null)
            return false;

        inventory.AddItem(target);//toilet

        GWorld.Instance.GetWorld().ModifyState(WorldStateName.FreeToilet, -1);

        return true;
    }

    public override bool PostPerform ()
    {
        GWorld.Instance.GetQueue(ResourceType.Toilets).AddResource(target);
        inventory.RemoveItem(target);
        GWorld.Instance.GetWorld().ModifyState(WorldStateName.FreeToilet, 1);

        beliefs.RemoveState(AgentBeliefs.Busting);

        return true;
    }
}