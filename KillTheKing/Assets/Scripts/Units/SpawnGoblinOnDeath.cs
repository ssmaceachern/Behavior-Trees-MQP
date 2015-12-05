using UnityEngine;
using System.Collections;

public class SpawnGoblinOnDeath : MonoBehaviour {

	private GameObject spawner;
	
	// Use this for initialization
	void Start () 
	{
		spawner = GameObject.FindGameObjectWithTag ("GoblinRespawn");
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnDestroy()
	{
		if (spawner != null)
		{
			spawner.GetComponent<SpawnGoblin> ().spawnAGoblin ();
		}
	}
}
