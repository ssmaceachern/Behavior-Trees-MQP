using UnityEngine;
using UnityEngine.UI;
using RAIN.Core;
using System.Collections;
using System;

public class DisplayKingAttributes : MonoBehaviour {

	private enum DisplayMode{
		None 		= 0,
        Health      = 1,
        Hunger 		= 2,
		Greed 		= 3
	};

    //int DisplayModeSize = Enum.GetNames(typeof(DisplayMode)).Length;
	
	//The AIRig of the parent knight object
	AIRig tRig;

	//Enum for switching between modes
	DisplayMode currentMode;

	//Holds the name of the current mode
	string currentModeName;

	//Holds the value of the parameter specified by
	//the current mode.
	int currentModeValue;
	

	//A reference to the slider object this is attached to.
	public Slider SliderReference;

	public Scrollbar HealthScrollbarReference;
	public Scrollbar LoyaltyScrollbarReference;
	public Scrollbar HungerScrollbarReference;
	public Scrollbar FearScrollbarReference;

	public Image FillReference;

	// Use this for initialization
	void Start () {
		currentMode = DisplayMode.Health;
		tRig = transform.parent.parent.GetComponentInChildren<AIRig>();
        SwitchMode();
    }

	void SwitchMode(){

		switch(currentMode){
		case DisplayMode.None:
			
			currentModeName = "None";
			FillReference.color = Color.black;
			break;
		case DisplayMode.Hunger:
			
			currentModeName = "Hunger";
			FillReference.color = Color.green;
			break;
		case DisplayMode.Health:
			
			currentModeName = "Health";
			FillReference.color = Color.red;
			break;
		case DisplayMode.Greed:
			
			currentModeName = "Loyalty";
			FillReference.color = Color.blue;
			break;
		default:
			break;
		}

		//currentModeName = Enum.GetName(typeof(DisplayMode), Convert.ChangeType(currentMode, currentMode.GetTypeCode()));
		SliderReference.value = tRig.AI.WorkingMemory.GetItem<System.Int32>(currentModeName);
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Q)){

            if((int)currentMode > 1)
            {
                currentMode--;
            }
            else
            {
                currentMode = DisplayMode.Greed;
            }

			SwitchMode();
		}

		if(Input.GetKeyDown(KeyCode.E)){

            if ((int)currentMode < 3)
            {
                currentMode++;
            }
            else
            {
                currentMode = DisplayMode.Health;
            }

            SwitchMode();
		}

		SliderReference.value = tRig.AI.WorkingMemory.GetItem<System.Int32>(currentModeName);
		HealthScrollbarReference.value = tRig.AI.WorkingMemory.GetItem<System.Int32>("Health") / (100f * 2f);
		LoyaltyScrollbarReference.value = tRig.AI.WorkingMemory.GetItem<System.Int32>("Greed") / (100f * 10f);
		HungerScrollbarReference.value = tRig.AI.WorkingMemory.GetItem<System.Int32>("Paranoia") / (100f * 10f);
		FearScrollbarReference.value = tRig.AI.WorkingMemory.GetItem<System.Int32>("Fear") / (100f * 10f);
	}

}
