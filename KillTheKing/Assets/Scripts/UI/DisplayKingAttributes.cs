using UnityEngine;
using UnityEngine.UI;
using RAIN.Core;
using System.Collections;
using System;

public class DisplayKingAttributes : MonoBehaviour {

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
	public Scrollbar ParanoiaScrollbarReference;
	public Scrollbar GreedScrollbarReference;
	public Scrollbar FearScrollbarReference;

	public Image FillReference;

	// Use this for initialization
	void Start () {
		tRig = GameObject.Find("King").transform.GetComponentInChildren<AIRig>();
    }

	// Update is called once per frame
	void LateUpdate () {

		//SliderReference.value = tRig.AI.WorkingMemory.GetItem<System.Int32>(currentModeName);
        HealthScrollbarReference.value = tRig.AI.WorkingMemory.GetItem<System.Int32>("Health") / (100f * 1f);
        ParanoiaScrollbarReference.value = tRig.AI.WorkingMemory.GetItem<System.Int32>("Paranoia") / (100f * 4f);
        GreedScrollbarReference.value = tRig.AI.WorkingMemory.GetItem<System.Int32>("Greed") / (100f * 4f);
        FearScrollbarReference.value = tRig.AI.WorkingMemory.GetItem<System.Int32>("Fear") / (100f * 4f);
	}

    void ChangeParanoia(int value)
    {
        
    }

}
