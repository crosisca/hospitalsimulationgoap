using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class GAction : MonoBehaviour
{
    public string actionName = "Action";
    public float cost = 1;
    public GameObject target;
    public string targetTag;
    public float duration;
    public WorldState[] preConditions;
    public WorldState[] afterEffects;
    public NavMeshAgent agent;

    public Dictionary<string, int> preconditions;
    public Dictionary<string, int> effects;

    public WorldStates afentBeliefs;

    public GInventory inventory;
    public WorldStates beliefs;

    public bool running = false;

    public GAction()
    {
        preconditions = new Dictionary<string, int>();
        effects = new Dictionary<string, int>();
    }

    public void Awake()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();

        if (preConditions != null)
        {
            foreach (WorldState worldState in preConditions)
                preconditions.Add(worldState.key, worldState.value);
        }

        if (afterEffects != null)
        {
            foreach (WorldState worldState in afterEffects)
                effects.Add(worldState.key, worldState.value);
        }

        inventory = this.GetComponent<GAgent>().inventory;
        beliefs = this.GetComponent<GAgent>().beliefs;
    }

    public bool IsAchievcable()
    {
        return true;
    }

    public bool IsAchievableGiven(Dictionary<string, int> conditions)
    {
        foreach (KeyValuePair<string, int> kvp in preconditions)
        {
            if (!conditions.ContainsKey(kvp.Key))
                return false;
        }

        return true;
    }

    public abstract bool PrePerform();

    public abstract bool PostPerform ();
}