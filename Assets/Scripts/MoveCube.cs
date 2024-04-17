using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveCube : MonoBehaviour
{
    public enum ConstraintAxis
    {
        X = 0,
        Y,
        Z
    }

    private float _sensitivity;           // sensitivity for grabbing jaw
    private Vector3 _mouseReference;      //mouse position reference
    private Vector3 _mouseOffset;         //mouse offset vector  
    private Vector3 _rotation;            //rotation vector
    private bool _isRotating;             //flag for if jaw is being rotated 
    public ConstraintAxis axis;           // Rotation around this axis is constrained
    public float min;                     // Relative value in degrees
    public float max;                     // Relative value in degrees
    private Transform thisTransform;      //current game object transform
    private Vector3 rotateAround;         //axis to rotate around
    private Quaternion minQuaternion;     //lower rotation limit
    private Quaternion maxQuaternion;     //upper rotation limit
    private float range;                  //range of motion
    private int axisMap;                  //mapping for if x,y or z axis rotation
    private bool done = false;            //flag for if rotation is complete

    public Animator penAnim;              //pen floating to mouth anim

    public Image checklistImg;            //Green box for checklist
    void Start()
    {
        _sensitivity = 0.4f;
        _rotation = Vector3.zero;
        thisTransform = transform;

        // Set the axis that we will rotate around
        switch (axis)
        {
            case ConstraintAxis.X:
                rotateAround = Vector3.right;
                axisMap = 0;
                break;

            case ConstraintAxis.Y:
                rotateAround = Vector3.up;
                axisMap = 1;
                break;

            case ConstraintAxis.Z:
                rotateAround = Vector3.forward;
                axisMap = 2;
                break;
        }

        // Set the min and max rotations in quaternion space
        var axisRotation = Quaternion.AngleAxis(thisTransform.localRotation.eulerAngles[axisMap], rotateAround);
        minQuaternion = axisRotation * Quaternion.AngleAxis(min, rotateAround);
        maxQuaternion = axisRotation * Quaternion.AngleAxis(max, rotateAround);
        range = max - min;
    }

    // We use LateUpdate to grab the rotation from the Transform after all Updates from
    // other scripts have occured
    void LateUpdate()
    {
        if (!done)
        {

            // We use quaternions here, so we don't have to adjust for euler angle range [ 0, 360 ]
            var localRotation = thisTransform.localRotation;
            var axisRotation = Quaternion.AngleAxis(localRotation.eulerAngles[axisMap], rotateAround);
            var angleFromMin = Quaternion.Angle(axisRotation, minQuaternion);
            var angleFromMax = Quaternion.Angle(axisRotation, maxQuaternion);

            if (angleFromMin == 0)
            {
                done = true;
                penAnim.enabled = true;
                checklistImg.color = Color.green;
            }
            if (angleFromMin <= range && angleFromMax <= range) // within range
            {
                if (_isRotating)
                {
                    // offset
                    _mouseOffset = Input.mousePosition - _mouseReference;

                    // apply rotation
                    _rotation.x = -(_mouseOffset.x + _mouseOffset.y) * _sensitivity;

                    transform.Rotate(_rotation);

                    // store mouse
                    _mouseReference = Input.mousePosition;
                }
                return;
            }

            else
            {
                // Let's keep the current rotations around other axes and only
                // correct the axis that has fallen out of range.
                var euler = localRotation.eulerAngles;
                if (angleFromMin > angleFromMax)
                    euler[axisMap] = maxQuaternion.eulerAngles[axisMap];
                else
                    euler[axisMap] = minQuaternion.eulerAngles[axisMap];

                thisTransform.localEulerAngles = euler;
            }
        }
    }
    void OnMouseDown()
    {
        // rotating flag
        _isRotating = true;

        // store mouse
        _mouseReference = Input.mousePosition;
    }

    void OnMouseUp()
    {
        // rotating flag
        _isRotating = false;
    }
}
