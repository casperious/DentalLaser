using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour
{
    public int speed;
    public Transform tf;
    void Start()
    {

    }
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            tf.eulerAngles = tf.eulerAngles + new Vector3(Input.GetAxis("Mouse Y") * speed * -1, Input.GetAxis("Mouse X") * speed, 0);      //controls rotation of camera
        }

    }
}
