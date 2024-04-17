using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGumMovement : MonoBehaviour
{
    public GameObject GumBone;
    public Transform gumboneTransform;
    int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (count < 1000)
        {
            gumboneTransform.Translate(0, 0, 2);
        }
    }
}
