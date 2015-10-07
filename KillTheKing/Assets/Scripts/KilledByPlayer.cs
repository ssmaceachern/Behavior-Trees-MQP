using UnityEngine;
using System.Collections;

// Die when the player enters your trigger 
public class KilledByPlayer : MonoBehaviour 
{
	void Start()
	{

	}
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			Destroy (this.gameObject);
		}
	}
}
