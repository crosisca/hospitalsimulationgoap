﻿public class GetTreated : GAction
{
    public override bool PrePerform()
    {
        target = inventory.FindItemWithTag("Cubicle");

        if (target == null)
            return false;

        return true;
    }

    public override bool PostPerform()
    {
        GWorld.Instance.GetWorld().ModifyState(WorldStateName.Treated, 1);

        inventory.RemoveItem(target);//cubicle

        return true;
    }
}