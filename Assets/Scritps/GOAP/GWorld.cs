using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GWorld
{
    public static GWorld Instance { get; } = new GWorld();
    static WorldStates world;
    static ResourceQueue patients;
    static ResourceQueue cubicles;
    static ResourceQueue offices;
    static ResourceQueue toilets;
    static ResourceQueue puddles;


    /// <summary>
    /// string = ResourceType
    /// </summary>
    static Dictionary<string, ResourceQueue> resources = new Dictionary<string, ResourceQueue>();

    static GWorld()
    {
        world = new WorldStates();
        patients = new ResourceQueue("","", world);
        resources.Add(ResourceType.Patients, patients);

        cubicles = new ResourceQueue(ObjectTag.Cubicle, WorldStateName.FreeCubicle, world);
        resources.Add(ResourceType.Cubicles, cubicles);

        offices = new ResourceQueue(ObjectTag.Office, WorldStateName.FreeOffice, world);
        resources.Add(ResourceType.Offices, offices);

        toilets = new ResourceQueue(ObjectTag.Toilet, WorldStateName.FreeToilet, world);
        resources.Add(ResourceType.Toilets, toilets);

        puddles = new ResourceQueue(ObjectTag.Puddle, WorldStateName.FreePuddle, world);
        resources.Add(ResourceType.Puddles, puddles);

        Time.timeScale = 5;
    }

    public ResourceQueue GetQueue(string type)
    {
        return resources[type];
    }

    private GWorld()
    {
        
    }
    
    public WorldStates GetWorld()
    {
        return world;
    }
}