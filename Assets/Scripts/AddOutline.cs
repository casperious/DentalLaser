using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddOutline : MonoBehaviour
{
    // Adds yellow outline to shape on gum
    public Outline script;
    void Start()
    {
        /*var outline = gameObject.AddComponent<Outline>();
        outline.renderers[0] = GetComponent<Renderer>();
        outline.OutlineMode = Outline.Mode.OutlineAll;
        outline.OutlineColor = Color.yellow;
        outline.OutlineWidth = 5f;*/
        script.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
