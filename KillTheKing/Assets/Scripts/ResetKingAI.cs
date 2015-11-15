using UnityEngine;
using System.Collections;
using RAIN.Core;

public class ResetKingAI : MonoBehaviour {
	public GameObject king;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("q"))
		{
			king.GetComponentInChildren<AIRig>().AI.Mind.AIInit ();
		}
	}
}
