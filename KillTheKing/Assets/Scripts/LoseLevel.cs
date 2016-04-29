using UnityEngine;
using System.Collections;

public class LoseLevel : MonoBehaviour
{
	// If the king reaches the end, the player loses
	void OnTriggerEnter(Collider col)
	{
		Debug.Log ("loseLevel hit");
		if (col.isTrigger)
		{
			Debug.Log ("fake hit");
			return;
		}
		else if (col.gameObject.tag == "King")
		{
			PlayerPrefs.SetInt ("LastLevel", Application.loadedLevel);
			Debug.Log (Application.loadedLevel);
			LevelLoad.StaticLoadLevel ("GameOver");
		}
		Debug.Log ("wut hit");
	}
}
