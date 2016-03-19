using UnityEngine;
using System.Collections;
using RAIN.Core;

// Move a wave and handle collisions
public class MoveWave : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
	{
		AIRig wAI = GetComponentInChildren<AIRig> ();
		wAI.AI.WorkingMemory.SetItem<bool> ("ShouldMove", true);
		Vector3 moveTo = new Vector3 (-150.0f, transform.position.y, transform.position.z);
		wAI.AI.WorkingMemory.SetItem<Vector3> ("MovePos", moveTo);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (transform.position.x <= -150)
		{
			Destroy (this.gameObject);
		}
	}

	// Destroy a game object hit by a wave
	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "King" || col.gameObject.tag == "Guard" || col.gameObject.tag == "Merc")
		{
			col.gameObject.GetComponentInChildren<AIRig>().AI.WorkingMemory.SetItem<int>("Health", 0);
		}
	}
}
