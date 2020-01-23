using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WInterface : MonoBehaviour
{
    GameObject focusObj;
    public GameObject newResourcePrefab;

    Vector3 goalPos;

    public NavMeshSurface surface;
    public GameObject hospital;

    Vector3 clickOffset = Vector3.zero;
    bool offsetCalc = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out hit))
                return;

            if (hit.transform.gameObject.tag == ObjectTag.Toilet)
            {
                focusObj = hit.transform.gameObject;
            }
            else
            {
                goalPos = hit.point;

                focusObj = Instantiate(newResourcePrefab, goalPos, newResourcePrefab.transform.rotation);
            }

            focusObj.GetComponent<Collider>().enabled = false;
        }
        else if (focusObj && Input.GetMouseButtonUp(0))
        {
            focusObj.transform.parent = hospital.transform;
            surface.BuildNavMesh();

            GWorld.Instance.GetQueue(ResourceType.Toilets).AddResource(focusObj);//TODO hard-coded
            GWorld.Instance.GetWorld().ModifyState(WorldStateName.FreeToilet, 1);

            focusObj.GetComponent<Collider>().enabled = true;

            focusObj = null;
        }
        else if (focusObj && Input.GetMouseButton(0))
        {
            RaycastHit hitMove;
            Ray rayMove = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(rayMove, out hitMove))
                return;

            goalPos = hitMove.point;
            focusObj.transform.position = goalPos;
        }

        if (focusObj && Input.GetKeyDown(KeyCode.Less) || Input.GetKeyDown(KeyCode.Comma))
            focusObj.transform.Rotate(0, 90, 0);
        else if (focusObj && Input.GetKeyDown(KeyCode.Greater) || Input.GetKeyDown(KeyCode.Period))
            focusObj.transform.Rotate(0, -90, 0);
    }
}
