using UnityEngine;

public class GetPatient : GAction
{
    GameObject resource;

    public override bool PrePerform ()
    {
        target = GWorld.Instance.GetQueue(ResourceType.Patients).RemoveResource();

        if (target == null)
            return false;

        resource = GWorld.Instance.GetQueue(ResourceType.Cubicles).RemoveResource();
        if (resource != null)
            inventory.AddItem(resource);
        else
        {
            GWorld.Instance.GetQueue(ResourceType.Patients).AddResource(target);//if theres no free cubicle, release the patient (add it back to world)
            target = null;
        }

        GWorld.Instance.GetWorld().ModifyState(WorldStateName.FreeCubicle, -1);
        return true;
    }

    public override bool PostPerform ()
    {
        GWorld.Instance.GetWorld().ModifyState(WorldStateName.PatientWaiting, -1);

        if (target)
            target.GetComponent<GAgent>().inventory.AddItem(resource);//nurse gives cubicle to patient

        return true;
    }
}