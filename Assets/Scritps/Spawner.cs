using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject patientPrefab;
    public int numPatients;
    public bool keepSpawning = false;

    void Start()
    {
        for (int i = 0; i < numPatients; i++)
        {
            Instantiate(patientPrefab, transform.position, Quaternion.identity);
        }

        if(keepSpawning)
            Invoke("SpawnPatient", 5);
    }

    void SpawnPatient()
    {
        Instantiate(patientPrefab, transform.position, Quaternion.identity);
        Invoke("SpawnPatient", Random.Range(2, 10));
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            Instantiate(patientPrefab, transform.position, Quaternion.identity);
    }
}