using UnityEngine;
using System.Collections;
using RAIN.Core;

public class InitKing : MonoBehaviour {

	// Use this for initialization
	void Start () {
		AI kingAI = GetComponentInChildren<AIRig> ().AI;
		kingAI.WorkingMemory.SetItem<Vector3> ("StartLoc", this.gameObject.transform.position);


	}
}
