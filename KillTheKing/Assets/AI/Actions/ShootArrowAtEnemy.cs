using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class ShootArrowAtEnemy : RAINAction
{
    private AudioManager audio;

    public override void Start(RAIN.Core.AI ai)
    {
        audio = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioManager>();

        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
	{
		int cooldown = ai.WorkingMemory.GetItem<int> ("Cooldown");
		
		if (cooldown>0) {
			ai.WorkingMemory.SetItem<int> ("Cooldown", cooldown-1);
			return ActionResult.SUCCESS;
		}
		else if(cooldown==-666)
		{
			return ActionResult.SUCCESS;
		}
		
		int maxCd=ai.WorkingMemory.GetItem<int> ("MaxCooldown");
		ai.WorkingMemory.SetItem<int> ("Cooldown", maxCd);

		GameObject newArrow = (GameObject)GameObject.Instantiate (Resources.Load ("Arrow"));
		newArrow.transform.position = ai.Body.transform.position;
		//newArrow.transform.position.y = 5;

        // Play a sound indicating that we've shot the arrow
        if (audio.arrowSounds.Length > 0)
        {
            ai.Body.GetComponent<AudioSource>().clip = audio.getRandomArrowSound();
            ai.Body.GetComponent<AudioSource>().Play();
        }

		GameObject myEnemy=ai.WorkingMemory.GetItem<GameObject> ("Enemy");
		newArrow.GetComponentInChildren<AIRig> ().AI.WorkingMemory.SetItem<GameObject> ("Opponent", myEnemy);

        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}