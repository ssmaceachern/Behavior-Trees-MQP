using UnityEngine;
using System.Collections;
using RAIN.Core;

public class BribeTavern : MonoBehaviour 
{
	void OnSelect(string command)
	{
		if (command == "Drunk") {
			GetComponentInChildren<AIRig> ().AI.WorkingMemory.SetItem<string> ("TrapType", "DrunkTavern");
		} else if (command == "Poison") {
			
			GetComponentInChildren<AIRig> ().AI.WorkingMemory.SetItem<string> ("TrapType", "PoisonTavern");

		} else if (command == "Disloyal") {
			
			GetComponentInChildren<AIRig> ().AI.WorkingMemory.SetItem<string> ("TrapType", "DisloyalTavern");

		}
	}
}
