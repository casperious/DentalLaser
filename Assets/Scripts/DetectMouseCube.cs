using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectMouseCube : MonoBehaviour
{
    public bool isHit = false;                                //flag set to true when mouse enters collider region
    public ParticleSystemRenderer particle;
    public ParticleSystem smoke;
    public ParticleSystemRenderer bloodRenderer;
    public ParticleSystem blood;

    void Start()                                             // On start and awake, set isHit to false and disable particle system
    {
        isHit = false;
        particle.enabled = false;
    }
    void Awake()
    {
        isHit = false;
        particle.enabled = false;
    }
    void OnMouseEnter()
    {
        particle.enabled = true;                            //start smoke and blood anim on entering collider region
        particle.trailMaterial = null;
        bloodRenderer.enabled = true;
        blood.Play();
        smoke.Play();
    }
    void OnMouseExit()
    {
        if (!isHit)
        {
            Debug.Log("Mouse exited");
            isHit = true;                                   //Only when mouse leaves the collider region, set isHit to true. Avoids early completion.
        }
    }
    void Reset()
    {
        isHit = false;
    }
}
