using UnityEngine;
using System.Collections;

public class LoseLevel : MonoBehaviour
{
	// If the king reaches the end, the player loses
	void OnTriggerEnter(Collider col)
	{
		Debug.Log ("Collider hit");
		if (col.isTrigger)
		{
			Debug.Log ("fake hit");
			return;
		}
		else if (col.gameObject.tag == "King")
		{
			Application.LoadLevel (1);
		}
		Debug.Log ("wut hit");
	}
}
