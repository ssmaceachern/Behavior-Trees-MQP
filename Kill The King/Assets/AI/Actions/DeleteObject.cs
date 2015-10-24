using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;
using RAIN.Representation;

[RAINAction]
public class DeleteObject : RAINAction
{
	public Expression objectToDelete = new Expression ();

	private GameObject go;	// The object to delete

    public override void Start(RAIN.Core.AI ai)
    {
		go = objectToDelete.Evaluate<GameObject> (ai.DeltaTime, ai.WorkingMemory);

        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
		if (go == null)
			return ActionResult.FAILURE;

		GameObject.Destroy (go);

        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}