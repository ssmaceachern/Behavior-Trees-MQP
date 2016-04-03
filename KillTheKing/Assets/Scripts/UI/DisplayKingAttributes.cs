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

	// A reference to the Parent Game Objects associated with each attribute
	public GameObject ParanoiaHUD;
	public GameObject FearHUD;
	public GameObject GreedHUD;

	public Scrollbar HealthScrollbarReference;
	private Scrollbar ParanoiaScrollbarReference;
	private Scrollbar GreedScrollbarReference;
	private Scrollbar FearScrollbarReference;

	public Image FillReference;
	
	private Image paranoiaEmoji;
	private Image fearEmoji;
	private Image greedEmoji;

	// Use this for initialization
	void Start () {
		tRig = GameObject.Find("King").transform.GetComponentInChildren<AIRig>();

		ParanoiaScrollbarReference = ParanoiaHUD.GetComponentInChildren<Scrollbar> ();
		GreedScrollbarReference = GreedHUD.GetComponentInChildren<Scrollbar> ();
        FearScrollbarReference = FearHUD.GetComponentInChildren<Scrollbar> ();

        paranoiaEmoji = ParanoiaHUD.transform.FindChild ("Emoji").gameObject.GetComponent<Image> ();
		greedEmoji = GreedHUD.transform.FindChild ("Emoji").gameObject.GetComponent<Image> ();
		fearEmoji = FearHUD.transform.FindChild ("Emoji").gameObject.GetComponent<Image> ();

		ChangeParanoia (tRig.AI.WorkingMemory.GetItem<int> ("Paranoia"));
		ChangeGreed (tRig.AI.WorkingMemory.GetItem<int> ("Greed"));
		ChangeFear (tRig.AI.WorkingMemory.GetItem<int> ("Fear"));
    }

	// Update is called once per frame
	void LateUpdate () {

		//SliderReference.value = tRig.AI.WorkingMemory.GetItem<System.Int32>(currentModeName);
        HealthScrollbarReference.value = tRig.AI.WorkingMemory.GetItem<System.Int32>("Health") / (100f * 1f);
        ParanoiaScrollbarReference.value = tRig.AI.WorkingMemory.GetItem<System.Int32>("Paranoia") / (100f * 4f);
        GreedScrollbarReference.value = tRig.AI.WorkingMemory.GetItem<System.Int32>("Greed") / (100f * 4f);
        FearScrollbarReference.value = tRig.AI.WorkingMemory.GetItem<System.Int32>("Fear") / (100f * 4f);
	}

    public void ChangeParanoia(int value)
    {
		ParanoiaScrollbarReference.value += (value / (100f * 4f));

		if (value > 0) {
			paranoiaEmoji.GetComponent<FlashColor> ().GoodFlash ();
		} else
			paranoiaEmoji.GetComponent<FlashColor> ().Flash ();;
    }

	public void ChangeFear(int value)
	{
		FearScrollbarReference.value += (value / (100f * 4f));

		if (value > 0) {
			fearEmoji.GetComponent<FlashColor> ().GoodFlash ();
		} else
			fearEmoji.GetComponent<FlashColor> ().Flash ();
	}

	public void ChangeGreed(int value)
	{
        if (GreedScrollbarReference == null)
        {
            Debug.Log("Null reference for greed scroll bar");
            return;
        }
		GreedScrollbarReference.value += (value / (100f * 4f));

		if (value > 0) {
			greedEmoji.GetComponent<FlashColor> ().GoodFlash ();
		} else
			greedEmoji.GetComponent<FlashColor> ().Flash ();;
	}
}
