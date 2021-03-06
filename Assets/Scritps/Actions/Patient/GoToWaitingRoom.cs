﻿public class GoToWaitingRoom : GAction
{
    public override bool PrePerform()
    {
        return true;
    }

    public override bool PostPerform()
    {
        GWorld.Instance.GetWorld().ModifyState(WorldStateName.PatientWaiting, 1);
        GWorld.Instance.GetQueue(ResourceType.Patients).AddResource(this.gameObject);//agent.gameObj

        beliefs.ModifyState(AgentBeliefs.AtHospital, 1);

        return true;
    }
}