using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;
using RAIN.Representation;
[RAINAction]
public class SpeakTo : RAINAction
{
	public Expression speechRecipient = new Expression();
	public Expression whatToSay = new Expression ();
	private int speechOption;
	private GameObject whoToTalkTo;

    public override void Start(RAIN.Core.AI ai)
    {
		whoToTalkTo = speechRecipient.Evaluate <GameObject> (ai.DeltaTime, ai.WorkingMemory);
		speechOption = whatToSay.Evaluate<int> (ai.DeltaTime, ai.WorkingMemory);

        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
		whoToTalkTo.GetComponentInChildren<AIRig> ().AI.WorkingMemory.SetItem<int>("receivedSpeech", speechOption);
		whoToTalkTo.GetComponentInChildren<AIRig> ().AI.WorkingMemory.SetItem<GameObject> ("speaker", ai.Body);

        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}