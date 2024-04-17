using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float sensX;                 //x axis sensitivity
    public float sensY;                 //y axis sensitivity
    public Transform orientation;       //camera orientation
    float xRot;                         //variable for use
    float yRot;                         //variable for use


    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;         //can hide cursor
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))                    //use right click to look around
        {
            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;
            yRot += mouseX;
            xRot -= mouseY;
            xRot = Mathf.Clamp(xRot, -90f, 90f);

            transform.rotation = Quaternion.Euler(xRot, yRot, 0);
            orientation.rotation = Quaternion.Euler(0, yRot, 0);
        }

    }

}
