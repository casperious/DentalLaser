using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLine : MonoBehaviour
{

    public float width = 0.05f;
    public Color color = Color.red;
    public Camera penCam;
    private LineRenderer lr;
    private Vector3[] linePoints = new Vector3[2];

    void Start()
    {
        lr = GetComponent<LineRenderer>();
        if (!lr) lr = gameObject.AddComponent<LineRenderer>();
        lr.material.color = color;
        lr.widthMultiplier = width;
        linePoints[0] = Vector3.zero;
        lr.positionCount = linePoints.Length;
    }

    void Update()
    {

        linePoints[0] = transform.position;
        if (lr.enabled == false) lr.enabled = true;
        Vector3 p = penCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, penCam.nearClipPlane));
        linePoints[1] = p;
        lr.SetPositions(linePoints);

    }
}