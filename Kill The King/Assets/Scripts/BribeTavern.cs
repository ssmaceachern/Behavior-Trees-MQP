﻿using UnityEngine;
using System.Collections;
using RAIN.Core;

public class BribeTavern : MonoBehaviour 
{
	void OnSelect(string command)
	{
		if (command == "Drunk") {

			GetComponentInChildren<AIRig> ().AI.WorkingMemory.SetItem<int> ("Type", 5);

		} else if (command == "Poison") {
			
			GetComponentInChildren<AIRig> ().AI.WorkingMemory.SetItem<int> ("Type", 6);

		} else if (command == "Disloyal") {
			
			GetComponentInChildren<AIRig> ().AI.WorkingMemory.SetItem<int> ("Type", 7);

		}
	}
}
