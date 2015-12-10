using UnityEngine;
using System.Collections;

public class SelectUnit : MonoBehaviour 
{
	private MeshRenderer selectMesh;	// The mesh that is displayed when a unit is selected
	private bool selected;

	void Start()
	{
		selectMesh = transform.FindChild ("SelectionIndicator").GetComponent<MeshRenderer>();
		selectMesh.enabled = false;
		selected = false;
	}

	// Update is called once per frame
	void Update () 
	{
		
	}

	public void MakeSelected()
	{
		// Show the player the unit is selected
		selected = true;

		selectMesh.enabled = true;

		// Do other tasks for a selected unit
	}

	public void Deselect()
	{
		// Hide the selection indicator
		selected = false;

		selectMesh.enabled = false;
	}
}
