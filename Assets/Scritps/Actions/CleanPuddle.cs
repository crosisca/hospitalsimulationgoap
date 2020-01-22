public class CleanPuddle : GAction
{
    public override bool PrePerform ()
    {
        target = GWorld.Instance.GetQueue(ResourceType.Puddles).RemoveResource();
        if (target == null)
            return false;

        inventory.AddItem(target);//puddle

        GWorld.Instance.GetWorld().ModifyState(WorldStateName.FreePuddle, -1);

        return true;
    }

    public override bool PostPerform ()
    {
        inventory.RemoveItem(target);
        Destroy(target);

        return true;
    }
}