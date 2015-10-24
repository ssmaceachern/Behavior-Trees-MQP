using UnityEngine;
using System.Collections;
using RAIN.Core;

public class HireMerc : MonoBehaviour 
{
	void OnSelect(string command)
	{
		GetComponentInChildren<AIRig> ().AI.WorkingMemory.SetItem<string> ("trapToLay", command);
	}
}
