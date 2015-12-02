using UnityEngine;
using UnityEngine.UI;
using RAIN.Core;
using System.Collections;
using System;

public class DisplayKnightAttributes : MonoBehaviour {

	private enum DisplayMode{
		None 		= 0,
		Hunger 		= 1,
		Health 		= 2,
		Greed 		= 3
	};
	int DisplayModeSize = Enum.GetNames(typeof(DisplayMode)).Length;
	
	//The AIRig of the parent knight object
	AIRig tRig;

	//Enum for switching between modes
	DisplayMode currentMode;

	//Holds the name of the current mode
	string currentModeName = "None";

	//Holds the value of the parameter specified by
	//the current mode.
	int currentModeValue;
	

	//A reference to the slider object this is attached to.
	public Slider SliderReference;
	public Image FillReference;

	// Use this for initialization
	void Start () {
		currentMode = DisplayMode.None;
		tRig = transform.parent.parent.GetComponentInChildren<AIRig>();
	}

	void SwitchMode(){

		switch(currentMode){
		case DisplayMode.None:
			//TODO Replace this.
			//currentModeValue = 0;
			currentModeName = "None";
			FillReference.color = Color.black;
			break;
		case DisplayMode.Hunger:
			//currentModeValue = KnightHunger;
			currentModeName = "Hunger";
			FillReference.color = Color.green;
			break;
		case DisplayMode.Health:
			//currentModeValue = KnightHealth;
			currentModeName = "Health";
			FillReference.color = Color.red;
			break;
		case DisplayMode.Greed:
			//currentModeValue = KnightLoyalty;
			currentModeName = "Loyalty";
			FillReference.color = Color.blue;
			break;
		default:
			break;
		}

		//currentModeName = Enum.GetName(typeof(DisplayMode), Convert.ChangeType(currentMode, currentMode.GetTypeCode()));
		SliderReference.value = tRig.AI.WorkingMemory.GetItem<System.Int32>(currentModeName);
		Debug.Log(currentModeName);
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Q)){

			currentMode--;

			CheckCurrentModeOverflow(currentMode);

			SwitchMode();
		}

		if(Input.GetKeyDown(KeyCode.E)){

			currentMode++;

			CheckCurrentModeOverflow(currentMode);

			SwitchMode();
		}

		SliderReference.value = tRig.AI.WorkingMemory.GetItem<System.Int32>(currentModeName);
	}

	void CheckCurrentModeOverflow(DisplayMode currentMode){
		//Handles the wrap around
		//Debug.Log((int)currentMode);

		if((int)currentMode < 0){
			currentMode++;
		}
		else if((int)currentMode > 3){
			currentMode--;
		}

		Debug.Log((int)currentMode);
	}
}
