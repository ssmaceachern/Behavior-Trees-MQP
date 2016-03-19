using UnityEngine;
using System.Collections;
using RAIN.Core;

public class InitKing : MonoBehaviour {

	// Use this for initialization
	void Start () {
		AI kingAI = GetComponentInChildren<AIRig> ().AI;
		kingAI.WorkingMemory.SetItem<Vector3> ("StartLoc", this.gameObject.transform.position);

		
		GameObject myGoal = GameObject.FindGameObjectWithTag ("Goal");
		if (myGoal != null) {
			Vector3 goalPos=new Vector3(myGoal.gameObject.transform.position.x, 0, myGoal.gameObject.transform.position.z);
			kingAI.WorkingMemory.SetItem<Vector3> ("MoveTarget", goalPos);
		}

		GameObject mySpawn = GameObject.FindGameObjectWithTag ("Spawn");
		if (mySpawn != null) {
			Vector3 spawnPos=new Vector3(mySpawn.gameObject.transform.position.x, 0, mySpawn.gameObject.transform.position.z);
			kingAI.WorkingMemory.SetItem<Vector3> ("StartLoc", spawnPos);
		}
	}
}
