using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;
public class MouseTrace : MonoBehaviour
{
    public List<bool> visited;                                          //list of visited colliders
    public List<GameObject> markers;                                    //list of game objects mouse has to visit
    private bool tracing = false;                                       //flag for if mouse is within the shape to be traced
    private int countTrack = 0;                                         //number of colliders visited    
    private bool complete = false;                                      //flag for if tracing is complete
    public Animator gumAnim;                                            //gum moving out animation
    public Animator scalpelAnim;                                        //scalpel coming in animation
    public MeshRenderer torus;                                          //disable shape after trace

    public Image checklistImg;                                          //green box on checklist

    public Image lastChecklistImg;                                      //last stop in checklist

    void Start()
    {
        for (int i = 0; i < markers.Count; i++)                         //set all nodes visited to false
        {
            visited[i] = false;
        }
    }


    void Update()
    {
        if (tracing && !complete)                                   //while tracing is not complete
        {
            if (countTrack == 4)                                    //if all 4 sides have been traced
            {
                complete = true;                                    //set complete to true
                Debug.Log("Completed trace");
                gumAnim.enabled = true;                             //start animations
                scalpelAnim.enabled = true;
                torus.enabled = false;
                checklistImg.color = Color.green;
            }
            for (int i = 0; i < markers.Count; i++)                 //loop through all targets and see if they have been visited by the cursor. if so, update list and count
            {
                if (markers[i].GetComponent<DetectMouseCube>().isHit && !visited[i])
                {
                    visited[i] = true;
                    countTrack++;
                }
            }
        }
        if (scalpelAnim.GetCurrentAnimatorStateInfo(0).IsName("New State"))     //if scalpel animator changes state to "New state" the animation is complete
        {
            gumAnim.SetBool("Done", true);                                      //set bool param for gum anim transition to true, so gum returns to initial spot
            torus.enabled = false;                                              //set shape mesh renderer off
            new WaitForSeconds(3f);
            lastChecklistImg.color = Color.green;

        }
    }
    void Reset()                                                    //function to reset all visited (intended for if mouse leaves shape, not in use)
    {
        tracing = false;
        for (int i = 0; i < markers.Count; i++)
        {
            visited[i] = false;
        }
    }
    void OnMouseEnter()                                             //detect mouse entry and start tracing
    {
        if (!complete)
        {
            Debug.Log("starting trace");
            tracing = true;
        }
    }
    void OnMouseExit()                                              //can be used to reset tracing progress if mouse leaves the shape without hitting all targets
    {
        /*if (!complete)
        {
            Reset();
            Debug.Log("Resetting");
        }*/
    }
}
