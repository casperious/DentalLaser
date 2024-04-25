using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine.UI;
using System;
public class GumSwitcherUI : MonoBehaviour
{
    public GameObject[] jaws;


    // Start is called before the first frame update
    void Start()
    {
        var dropdown = transform.GetComponent<TMP_Dropdown>();
        DropdownItemSelected(dropdown);
        dropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(dropdown); });
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void DropdownItemSelected(TMP_Dropdown dropdown)
    {
        int idx = dropdown.value;
        //Debug.Log(idx);
        for (int i = 0; i < jaws.Length; i++)
        {
            if (i != idx)
            {
                jaws[i].SetActive(false);
            }
        }
        jaws[idx].SetActive(true);
    }
}
