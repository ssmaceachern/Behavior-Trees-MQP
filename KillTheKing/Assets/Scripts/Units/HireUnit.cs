using UnityEngine;
using System.Collections;
using RAIN.Core;

public class HireUnit : MonoBehaviour 
{
	void OnSelect(string command)
	{
		GetComponentInChildren<AIRig> ().AI.WorkingMemory.SetItem<string> ("Command", command);
	}
}
