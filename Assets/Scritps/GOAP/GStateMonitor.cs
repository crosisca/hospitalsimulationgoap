using UnityEngine;

public class GStateMonitor : MonoBehaviour
{
    /// <summary>
    /// AgentBeliefs
    /// </summary>
    public string state;

    public float stateStrength;
    public float stateDecayRate;
    public WorldStates beliefs;
    public GameObject resourcePrefab;

    /// <summary>
    /// ResourceType
    /// </summary>
    public string queueName;

    /// <summary>
    /// WorldStateName
    /// </summary>
    public string worldState;

    public GAction action;

    bool stateFound;
    float initialStrength;

    void Awake()
    {
        beliefs = this.GetComponent<GAgent>().beliefs;
        initialStrength = stateStrength;
    }

    void LateUpdate()
    {
        if (action.running)//reset
        {
            stateFound = false;
            stateStrength = initialStrength;
        }

        if (!stateFound && beliefs.HasState(state))
            stateFound = true;

        if (stateFound)//start counting down
        {
            stateStrength -= stateDecayRate * Time.deltaTime;
            if (stateStrength <= 0)
            {
                Vector3 location = new Vector3(this.transform.position.x, resourcePrefab.transform.position.y, this.transform.position.z);
                GameObject go = Instantiate(resourcePrefab, location, resourcePrefab.transform.rotation);
                stateFound = false;
                stateStrength = initialStrength;
                beliefs.RemoveState(state);
                GWorld.Instance.GetQueue(queueName).AddResource(go);
                GWorld.Instance.GetWorld().ModifyState(worldState, 1);
            }
        }
    }
}