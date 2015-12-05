using UnityEngine;
using System.Collections;
using RAIN.Core;

// Spawn a goblin to keep a certain number on the field at any given time.
public class SpawnGoblin : MonoBehaviour 
{
	public Vector3 moveLocation;

	private GameObject charParent;

	// Use this for initialization
	void Start () 
	{
		charParent = GameObject.FindGameObjectWithTag ("Characters");	
	}

	// Update is called once per frame
	void Update () 
	{
	
	}

	public void spawnAGoblin()
	{
		GameObject newGoblin = (GameObject)Instantiate (Resources.Load ("Goblin"));

		newGoblin.transform.position = this.transform.position;

		newGoblin.GetComponentInChildren<AIRig> ().AI.WorkingMemory.SetItem<Vector3> ("Location", moveLocation);

		newGoblin.transform.SetParent (charParent.transform);
	}
}
