using System.Collections;
using System.Collections.Generic;
using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private LineRenderer lr;
    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    //check if laser is hitting something and don't render the laser beyond that point
    void Update()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        lr.SetPosition(0, pos);
        RaycastHit hit;
        if (Physics.Raycast(pos, transform.forward * -1, out hit))
        {
            if (hit.collider)
            {
                lr.SetPosition(1, hit.point);
            }
        }
        else
        {
            lr.SetPosition(1, transform.forward * -5);
        }
    }
}
