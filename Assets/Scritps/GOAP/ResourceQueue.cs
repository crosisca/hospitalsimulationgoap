using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ResourceQueue
{
    public Queue<GameObject> queue = new Queue<GameObject>();
    public string tag;
    public string modState;

    public ResourceQueue (string tag, string modState, WorldStates worldStates)
    {
        this.tag = tag;
        this.modState = modState;

        if (tag != "")
        {
            GameObject[] resources = GameObject.FindGameObjectsWithTag(tag);

            foreach (GameObject resource in resources)
                queue.Enqueue(resource);
        }

        if (modState != "")
        {
            worldStates.ModifyState(modState, queue.Count);
        }
    }

    public void AddResource(GameObject resource)
    {
        queue.Enqueue(resource);
    }

    public void RemoveResource (GameObject resource)
    {
        queue = new Queue<GameObject>(queue.Where(x => x != resource));
    }

    public GameObject RemoveResource()
    {
        if (queue.Count <= 0)
            return null;

        return queue.Dequeue();
    }
}