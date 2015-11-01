using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

// The guard will wait until he gets a response from the player
[RAINAction]
public class WaitForResponse : RAINAction
{
	private int phraseSaid;		// what was said
	private GameObject speaker;	// who said it
	private TextMesh speech;

    public override void Start(RAIN.Core.AI ai)
    {
		speech = ai.Body.GetComponentInChildren<TextMesh> ();
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
		phraseSaid = ai.WorkingMemory.GetItem<int> ("receivedSpeech");
		speaker = ai.WorkingMemory.GetItem<GameObject> ("speaker");

		switch(phraseSaid)
		{
		case -1:
			return ActionResult.RUNNING;
		case 1:
			speech.text = "I can look the other way";
			break;
		case 2:
			if(speaker.tag == "King")
				speech.text = "Yes sir!";
			else
				speech.text = "$^%& you!";
			break;
		case 3:
			speech.text = "Who am I? Who are you!?";
			break;
		default:
			speech.text = "I don't know";
			break;
		}

        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}