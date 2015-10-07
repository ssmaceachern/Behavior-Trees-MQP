using UnityEngine;
using System.Collections;

public class SpawnPeasant : MonoBehaviour 
{
	void OnSelect(string command)
	{
		if (command == "Make Peasant")
		{
			GameObject go = (GameObject)GameObject.Instantiate (Resources.Load ("Peasant"));

			go.transform.position = new Vector3 (transform.position.x - 5.0f, transform.position.y - 2.5f, transform.position.z);
		}
		else
		{
			GameObject go = (GameObject)GameObject.Instantiate (Resources.Load ("Merc"));
			
			go.transform.position = new Vector3 (transform.position.x - 5.0f, transform.position.y - 2.5f, transform.position.z);
		}
	}
}
