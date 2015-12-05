using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;
using RAIN.Representation;

[RAINAction]
public class ActivateSpawner : RAINAction
{
	public Expression spawnerNameInMind;

	private string spawnerName;

    public override void Start(RAIN.Core.AI ai)
	{
		spawnerName = spawnerNameInMind.Evaluate<string> (ai.DeltaTime, ai.WorkingMemory);
		base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
		GameObject spawner = ai.WorkingMemory.GetItem<GameObject> (spawnerName);

		if (spawner == null)
		{
			Debug.LogError("No spawner assigned to action");
			return ActionResult.FAILURE;
		}

		spawner.GetComponent<SpawnWave> ().spawnWave = true;

        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}