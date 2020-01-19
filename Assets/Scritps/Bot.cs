using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Bot : MonoBehaviour
{
    RaycastHit hitInfo;
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out hitInfo))
            {
                agent.destination = hitInfo.point;
            }
        }
    }
}