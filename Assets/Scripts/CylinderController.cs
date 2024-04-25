using UnityEngine;

public class CylinderController : MonoBehaviour
{
    // Speed of the cylinder movement
    public float speed = 10f;
    public float distanceFromCamera;
    public GameObject particle;
    public GameObject followEnd;
    public Camera cam;
    // Update is called once per frame
    void Update()
    {
        transform.position = cam.transform.position;
        followEnd.transform.position = particle.transform.position;
    }
}
