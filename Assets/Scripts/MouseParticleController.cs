using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseParticleController : MonoBehaviour
{
    public float speed = 8.0f;
    public float distanceFromCamera = 5.0f;

    public Camera cam;
    public GameObject particles;
    public GameObject laser;
    public float xOffset;
    public float yOffset;

    private bool started = false;
    private float startTime = 0;
    public float timer = 0;
    float holdDuration = 0;
    void Start()
    {
        particles.SetActive(false);
    }


    void Update()
    {
        var ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        particles.SetActive(true);
        laser.SetActive(true);
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = distanceFromCamera;

        Vector3 mouseScreenToWorld = cam.ScreenToWorldPoint(mousePosition);
        mouseScreenToWorld.x += xOffset;
        mouseScreenToWorld.y += yOffset;
        Vector3 position = Vector3.Lerp(particles.transform.position, mouseScreenToWorld, 1.0f - Mathf.Exp(-speed * Time.deltaTime));
        //position.x = position.x + 0.1f;
        //position.y = position.y + 0.1f;

        particles.transform.position = position;
        if (started)
        {
            holdDuration = Time.time - startTime;
            //Debug.Log("Mouse holding: " + holdDuration + " seconds");
            if (holdDuration > 2)
            {
                particles.GetComponent<ParticleSystemRenderer>().enabled = true;
                holdDuration = 0;
            }
            else
            {
                particles.GetComponent<ParticleSystemRenderer>().enabled = false;
            }
        }
        //particles.transform.LookAt(cam.transform);
        //laser.transform.LookAt(mouseScreenToWorld);
        if (Input.GetMouseButtonDown(0))
        {
            if (!started)
            {
                startTime = Time.time;
                started = true;
                Debug.Log("Starting timer");
            }
            if (Physics.Raycast(ray, out hitInfo))
            {
                particles.transform.position = hitInfo.transform.position;
                /*if (holdDuration > 2)
                {
                    particles.GetComponent<ParticleSystemRenderer>().enabled = true;
                    //startTime = Time.time;
                }*/
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            particles.GetComponent<ParticleSystemRenderer>().enabled = false;
            started = false;
            timer = 0;
        }
    }

    void OnMouseDown()
    {
        startTime = Time.time;
        timer = startTime;
    }




}
