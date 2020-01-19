using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GWorld
{
    public static GWorld Instance { get; } = new GWorld();
    static WorldStates world;
    static Queue<GameObject> patients;
    static Queue<GameObject> cubicles;

    static GWorld()
    {
        world = new WorldStates();
        patients = new Queue<GameObject>();
        cubicles = new Queue<GameObject>();

        GameObject[] cubes = GameObject.FindGameObjectsWithTag("Cubicle");
        foreach (GameObject cubicle in cubes)
            cubicles.Enqueue(cubicle);

        if(cubes.Length > 0)
            world.ModifyState(WorldStateName.FreeCubicle, cubes.Length);
    }

    private GWorld()
    {
        
    }
    
    public WorldStates GetWorld()
    {
        return world;
    }

    public void AddPatient (GameObject patient)
    {
        patients.Enqueue(patient);
    }

    public GameObject RemovePatient ()
    {
        if (patients.Count <= 0)
            return null;

        return patients.Dequeue();
    }

    public void AddCubicle (GameObject Cubicle)
    {
        cubicles.Enqueue(Cubicle);
    }

    public GameObject RemoveCubicle ()
    {
        if (cubicles.Count <= 0)
            return null;

        return cubicles.Dequeue();
    }
}