using UnityEngine;

public class Research : GAction
{
    public override bool PrePerform ()
    {
        target = GWorld.Instance.GetQueue(ResourceType.Offices).RemoveResource();

        if (target == null)
            return false;

        inventory.AddItem(target);
        GWorld.Instance.GetWorld().ModifyState(WorldStateName.FreeOffice, -1);

        //Debug.Log("Research Started");

        return true;
    }

    public override bool PostPerform ()
    {
        GWorld.Instance.GetQueue(ResourceType.Offices).AddResource(target);
        inventory.RemoveItem(target);
        GWorld.Instance.GetWorld().ModifyState(WorldStateName.FreeOffice, 1);

        //Debug.Log("Research Finished");

        return true;
    }
}