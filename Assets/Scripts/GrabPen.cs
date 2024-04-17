using UnityEngine;
using System.Collections;
using UnityEditor;
using System;
using UnityEngine.UI;

[RequireComponent(typeof(MeshCollider))]

public class GrabPen : MonoBehaviour
{

    public Camera cam;                                                      //main scene camera (on capsule for player)
    public Camera penCam;                                                   //cam attached at tip of pen
    public Transform penCamTransform;                                       //transform of pen cam
    public Animator penAnim;                                                //animator for pen floating to mouth

    public MouseParticleController mouseParticleController;                 //particle effects on mouse position
    public Material fade;                                                   //fade to black for transition to pen cam
    public float transitionSpeed;                                           //speed of transition
    private bool switcher = false;                                          //flag for before camera has transitioned to pen cam
    private bool penCamSwitched = false;                                    //flag for after switch to pen cam
    public Texture2D cursorImg;                                             //custom cursor texture
    public GameObject penLaser;                                             //Laser gameobject on pen (line renderer)
    public GameObject camCylinder;                                          //cam laser game object (3d object)
    public AddOutline outlineScript;                                        //AddOutline script attached to shape
    public MouseTrace tracer;                                               //MouseTrace script attached to shape        
    public BoxCollider left, right, up, down;                               //box colliders on each side of shape    
    public float speed = 3.5f;                                              //speed of moving pen camera
    private float X;                                                        //variable for cam control
    private float Y;                                                        //variable for cam control    

    public Image checklistImg;                                              //green box on checklist

    void Start()
    {
        Color c = fade.color;                                               //reset fade to transparent
        c.a = 0;
        fade.color = c;
    }
    void OnMouseDown()
    {
        if (penCamSwitched)
        {

        }
        else
        {
            penAnim.enabled = false;
            transform.position = new Vector3(2.195f, 1.1886f, -0.1536f);                //if clicked on pen, then reposition the pen and initiate transition
            checklistImg.color = Color.green;
            switcher = true;
        }


    }

    void Update()
    {
        if (switcher)                                                                       //if in process of switching
        {
            if (fade.color.a < 2)                                                           //if fade to black isn't done yet
            {

                Color c = fade.color;
                c.a += 1 * transitionSpeed;                                                 //update alpha transparency of player cam
                if (c.a > 2)                                                                //if fade complete set values and switch to pen cam
                {
                    switcher = false;
                    cam.enabled = false;
                    penCam.enabled = true;
                    penCamSwitched = true;
                }
                fade.color = c;
            }
            else                                                                            //if transition is complete
            {
                switcher = false;                                                           //switching done, so set switcher to false
                cam.enabled = false;                                                        //disable player cam        
                penLaser.SetActive(false);                                                  //switch off laser
                camCylinder.SetActive(false);                                               //switch off laser
                CursorMode cursorMode = CursorMode.Auto;                                    //update cursor to imitate laser
                Vector2 hotSpot = Vector2.zero;
                Cursor.SetCursor(cursorImg, hotSpot, cursorMode);
                outlineScript.enabled = true;                                               //show outline of shape to trace
                penCam.enabled = true;                                                      //enable pen cam
                penCamSwitched = true;                                                      //set penCamSwitched to true to control pen cam    
                tracer.enabled = true;                                                      //enable tracing module
                left.enabled = true;                                                        //enable all colliders for tracing
                right.enabled = true;
                up.enabled = true;
                down.enabled = true;
                mouseParticleController.enabled = true;
            }
        }
        if (penCamSwitched)                                                                 //if user is now pen cam
        {
            if (Input.GetMouseButton(1))                                                    //right click to look around
            {
                penCamTransform.Rotate(new Vector3(Input.GetAxis("Mouse Y") * speed, Input.GetAxis("Mouse X") * speed, 0));
                X = penCamTransform.rotation.eulerAngles.x;
                Y = penCamTransform.rotation.eulerAngles.y;
                penCamTransform.rotation = Quaternion.Euler(X, Y, 0);
            }
        }

    }

}