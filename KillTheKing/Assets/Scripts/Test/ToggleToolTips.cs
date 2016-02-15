﻿using UnityEngine;
using System.Collections;

public class ToggleToolTips : MonoBehaviour
{
    private GameObject toolTips;

	// Use this for initialization
	void Start ()
    {
        toolTips = transform.FindChild("ToolTips").gameObject;
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Ray cast down and see if we hit an entity
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 200.0f) != false)
        {
            if (hit.collider.gameObject == this.gameObject)
            {
                toolTips.SetActive(true);
                return;
            }
        }

        toolTips.SetActive(false);
    }
}
