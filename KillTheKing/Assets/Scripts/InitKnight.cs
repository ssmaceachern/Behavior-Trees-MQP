using UnityEngine;
using System.Collections;
using RAIN.Core;

public class InitKnight : MonoBehaviour {

	// Use this for initialization
	void Start () {
        AI knightAI = GetComponentInChildren<AIRig>().AI;
        knightAI.WorkingMemory.SetItem<Vector3>("ReturningTo", this.gameObject.transform.position);


        GameObject myGoal = GameObject.FindGameObjectWithTag("Goal");
        if (myGoal != null)
        {
            Vector3 goalPos = new Vector3(myGoal.gameObject.transform.position.x, 0, myGoal.gameObject.transform.position.z);
            knightAI.WorkingMemory.SetItem<Vector3>("MovingTo", goalPos);
        }

        GameObject mySpawn = GameObject.FindGameObjectWithTag("Spawn");
        if (mySpawn != null)
        {
            Vector3 spawnPos = new Vector3(mySpawn.gameObject.transform.position.x, 0, mySpawn.gameObject.transform.position.z);
            knightAI.WorkingMemory.SetItem<Vector3>("ReturningTo", spawnPos);
        }
    }
}
