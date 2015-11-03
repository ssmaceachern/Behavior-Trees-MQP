using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;
using RAIN.Representation;

// Change the text in the speech bubble above the guard's head.
[RAINAction]
public class SaySomething : RAINAction
{
	public Expression sentence = new Expression();

    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
		TextMesh speech = ai.Body.GetComponentInChildren<TextMesh> ();

		speech.text = sentence.Evaluate<string> (ai.DeltaTime, ai.WorkingMemory);

        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}