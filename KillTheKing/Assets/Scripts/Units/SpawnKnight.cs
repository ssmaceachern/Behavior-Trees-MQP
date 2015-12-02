using UnityEngine;
using System.Collections;
using RAIN.Core;

// Spawn knights to keep a certain number on the field at any given time.
public class SpawnKnight : MonoBehaviour 
{
	public int numKnights = 3;

	private GameObject king;
	private GameObject charParent;

	// Use this for initialization
	void Start () 
	{
		king = GameObject.FindGameObjectWithTag ("King");
		charParent = GameObject.FindGameObjectWithTag ("Characters");
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void spawnAKnight()
	{
		GameObject newKnight = (GameObject)Instantiate (Resources.Load ("Knight"));

		newKnight.transform.position = this.transform.position;

		newKnight.GetComponentInChildren<AIRig> ().AI.WorkingMemory.SetItem<GameObject> ("Master", king);
		
		newKnight.transform.SetParent (charParent.transform);
	}
}
