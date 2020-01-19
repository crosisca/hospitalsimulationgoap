using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GWorld
{
    public static GWorld Instance { get; } = new GWorld();
    static WorldStates world;
    static Queue<GameObject> patients;
    static Queue<GameObject> cubicles;
    static Queue<GameObject> offices;
    static Queue<GameObject> toilets;

    static GWorld()
    {
        world = new WorldStates();
        patients = new Queue<GameObject>();
        cubicles = new Queue<GameObject>();
        offices = new Queue<GameObject>();
        toilets = new Queue<GameObject>();

        //Cubicles
        GameObject[] cubes = GameObject.FindGameObjectsWithTag("Cubicle");
        foreach (GameObject cubicle in cubes)
            cubicles.Enqueue(cubicle);

        if(cubes.Length > 0)
            world.ModifyState(WorldStateName.FreeCubicle, cubes.Length);

        //Offices
        GameObject[] offs = GameObject.FindGameObjectsWithTag("Office");
        foreach (GameObject ofc in offs)
            offices.Enqueue(ofc);

        if (offs.Length > 0)
            world.ModifyState(WorldStateName.FreeOffice, offs.Length);

        //Toilets
        GameObject[] toilts = GameObject.FindGameObjectsWithTag("Toilet");
        foreach (GameObject ofc in toilts)
            toilets.Enqueue(ofc);

        if (toilts.Length > 0)
            world.ModifyState(WorldStateName.FreeToilet, toilts.Length);

        Time.timeScale = 5;
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

    public void AddCubicle (GameObject cubicle)
    {
        cubicles.Enqueue(cubicle);
    }

    public GameObject RemoveCubicle ()
    {
        if (cubicles.Count <= 0)
            return null;

        return cubicles.Dequeue();
    }

    public void AddOffice (GameObject office)
    {
        offices.Enqueue(office);
    }

    public GameObject RemoveOffice ()
    {
        if (offices.Count <= 0)
            return null;

        return offices.Dequeue();
    }

    public void AddToilet (GameObject toilet)
    {
        toilets.Enqueue(toilet);
    }

    public GameObject RemoveToilet ()
    {
        if (toilets.Count <= 0)
            return null;

        return toilets.Dequeue();
    }
}