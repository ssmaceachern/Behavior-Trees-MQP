using UnityEngine;
using System.Collections;

public class SpawnKnightOnDeath : MonoBehaviour 
{
	private GameObject spawner;
	
	// Use this for initialization
	void Start () 
	{
		spawner = GameObject.FindGameObjectWithTag ("KnightRespawn");
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	
	void OnDestroy()
	{
		if (spawner != null)
		{
			spawner.GetComponent<SpawnKnight> ().spawnAKnight ();
		}
	}
}
