using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseParticleController : MonoBehaviour
{
    public float speed = 8.0f;
    public float distanceFromCamera = 5.0f;

    public Camera cam;
    public GameObject particles;
    public float xOffset;
    public float yOffset;

    void Start()
    {
        particles.SetActive(false);
    }


    void Update()
    {
        particles.SetActive(true);
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = distanceFromCamera;

        Vector3 mouseScreenToWorld = cam.ScreenToWorldPoint(mousePosition);
        mouseScreenToWorld.x += xOffset;
        mouseScreenToWorld.y += yOffset;
        Vector3 position = Vector3.Lerp(particles.transform.position, mouseScreenToWorld, 1.0f - Mathf.Exp(-speed * Time.deltaTime));
        //position.x = position.x + 0.1f;
        //position.y = position.y + 0.1f;

        particles.transform.position = position;
        particles.transform.LookAt(cam.transform);
        if (Input.GetMouseButtonDown(0))
        {
            particles.GetComponent<ParticleSystemRenderer>().enabled = true;

        }
        if (Input.GetMouseButtonUp(0))
        {
            particles.GetComponent<ParticleSystemRenderer>().enabled = false;
        }
    }

}
