using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

// Deal damage to the current enemy
[RAINAction]
public class AttackTarget : RAINAction
{
    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
	{
		GameObject myEnemy = ai.WorkingMemory.GetItem<GameObject> ("Enemy");
		MessageDispatcher dispatch = ai.Body.GetComponent<MessageDispatcher> ();

		// Make sure there is a valid enemy to attack
		if (myEnemy==null || !myEnemy.activeSelf) 
		{
			ai.WorkingMemory.SetItem<GameObject> ("Enemy", null);
			return ActionResult.FAILURE;
		}

		/* We don't want an attack every frame, so keep a cooldown value */
		int cooldown = ai.WorkingMemory.GetItem<int> ("Cooldown");

		// If we shouldn't attack this frame, decrement our cooldown value 
		if (cooldown > 0) 
		{
			ai.WorkingMemory.SetItem<int> ("Cooldown", cooldown-1);
			return ActionResult.SUCCESS;
		}

		/* Attack the enemy and reset the cool down, so we don't attack next frame as well */
		int maxCd = ai.WorkingMemory.GetItem<int> ("MaxCooldown");
		ai.WorkingMemory.SetItem<int> ("Cooldown", maxCd);

		/* Decrease the enemy's health by the amount of damage we deal */
		int myDamage = ai.WorkingMemory.GetItem<int>("Damage");

		// Default to 10 damage if no damage amount specified
		if (myDamage == 0) 
		{
			myDamage = 10;
		} 

		// Tell the enemy how much damage we are dealing to them
		dispatch.SendMsg (0.0f,
		                  ai.Body,
		                  myEnemy,
		                  (int)MessageTypes.MsgType.DealDamage,
		                  myDamage);        

        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}