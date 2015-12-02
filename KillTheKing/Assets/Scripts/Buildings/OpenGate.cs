using UnityEngine;
using System.Collections;

// Open the gate when the king gets close
public class OpenGate : MonoBehaviour 
{
	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "King")
		{
			Destroy (this.gameObject);
		}
	}
}
