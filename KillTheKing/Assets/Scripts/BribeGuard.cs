using UnityEngine;
using System.Collections;
using RAIN.Core;

// Bribe a guard when space is pressed
public class BribeGuard : MonoBehaviour 
{
	public GameObject guard;	

	private AIRig guardRig;		// The ai of the guard

	// Use this for initialization
	void Start () 
	{
		guardRig = guard.GetComponentInChildren<AIRig> ();	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown ("space"))
		{
			guardRig.AI.WorkingMemory.SetItem<int>("receivedSpeech", SpeechOptions.bribe);
			guardRig.AI.WorkingMemory.SetItem<GameObject>("speaker", this.gameObject);
		}
	}
}
