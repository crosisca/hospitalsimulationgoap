public class GoToCubicle : GAction
{
    public override bool PrePerform ()
    {
        target = inventory.FindItemWithTag("Cubicle");

        if (target == null)
            return false;

        return true;
    }

    public override bool PostPerform ()
    {
        GWorld.Instance.GetWorld().ModifyState(WorldStateName.TreatingPatient, 1);
        GWorld.Instance.AddCubicle(target);//return cubicle to world

        inventory.RemoveItem(target);//cubicle

        GWorld.Instance.GetWorld().ModifyState(WorldStateName.FreeCubicle, 1);

        return true;
    }
}