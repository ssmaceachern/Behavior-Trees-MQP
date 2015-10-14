using UnityEngine;
using System.Collections;

public class SpawnPeasant : MonoBehaviour 
{
	void OnSelect(string command)
	{
		if (command == "Make Peasant")
		{
			GameObject go = (GameObject)GameObject.Instantiate (Resources.Load ("Peasant"));

			go.transform.position = new Vector3 (transform.position.x - (Random.value*4-2), transform.position.y - 2.5f, transform.position.z - (Random.value*10+10));
		}
		else
		{
			GameObject go = (GameObject)GameObject.Instantiate (Resources.Load ("Merc"));

			go.transform.position = new Vector3 (transform.position.x - (Random.value*10+10), transform.position.y - 2.5f, transform.position.z - (Random.value*4-2));
		}
	}
}
