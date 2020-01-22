public class GoToCubicle : GAction
{
    public override bool PrePerform ()
    {
        target = inventory.FindItemWithTag(ObjectTag.Cubicle);

        if (target == null)
            return false;

        GWorld.Instance.GetWorld().ModifyState(WorldStateName.TreatingPatient, 1);
        return true;
    }

    public override bool PostPerform ()
    {
        GWorld.Instance.GetWorld().ModifyState(WorldStateName.TreatingPatient, -1);

        GWorld.Instance.GetQueue(ResourceType.Cubicles).AddResource(target);//return cubicle to world

        inventory.RemoveItem(target);//cubicle

        GWorld.Instance.GetWorld().ModifyState(WorldStateName.FreeCubicle, 1);

        return true;
    }
}