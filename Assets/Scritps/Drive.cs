using UnityEngine;
using UnityEngine.AI;

public class Drive : MonoBehaviour
{
    Camera cam;
    public float speed = 10f;
    public float rotationSpeed = 20f;

    // Start is called before the first frame update
    void Start ()
    {
        cam = this.GetComponentInChildren<Camera>();
        cam.transform.LookAt(this.transform.position);
    }

    // Update is called once per frame
    void Update ()
    {
        float translation = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        float translation2 = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        this.transform.Translate(0, 0, translation);
        this.transform.Translate(translation2, 0, 0);

        //Rotate
        if(Input.GetKey(KeyCode.Z))
            transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0);
        if (Input.GetKey(KeyCode.C))
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);

        //Zoom
        if(Input.GetKey(KeyCode.R) && cam.transform.position.y > 5)
            cam.transform.Translate(0, 0, speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.F) && cam.transform.position.y < 45)
            cam.transform.Translate(0, 0, -speed * Time.deltaTime);

        float angle = Vector3.Angle(cam.transform.forward, Vector3.up);
        //Debug.Log(angle);

        //Tilt
        if (Input.GetKey(KeyCode.T) && angle < 175)
        {
            cam.transform.Translate(Vector3.up);
            cam.transform.LookAt(this.transform.position);
        }
        if (Input.GetKey(KeyCode.G) && angle > 95)
        {
            cam.transform.Translate(Vector3.down);
            cam.transform.LookAt(this.transform.position);
        }
    }

}