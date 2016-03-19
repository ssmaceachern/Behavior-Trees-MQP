using UnityEngine;
using UnityEngine.UI;
using RAIN.Core;
using System.Collections;
using System;

public class DisplayKnightAttributes : MonoBehaviour {

    //int DisplayModeSize = Enum.GetNames(typeof(DisplayMode)).Length;
	
	//The AIRig of the parent knight object
	AIRig tRig;

	//Holds the name of the current mode
	string currentModeName;

	//Holds the value of the parameter specified by
	//the current mode.
	int currentModeValue;
	

	//A reference to the slider object this is attached to.
	public Slider SliderReference;

	public Scrollbar HealthScrollbarReference;

	// Use this for initialization
	void Start () {
		tRig = transform.parent.parent.GetComponentInChildren<AIRig>(); 
    }	

	// Update is called once per frame
	void Update () {
		HealthScrollbarReference.value = tRig.AI.WorkingMemory.GetItem<System.Int32>("Health") / (100f);
	}

}
