using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

// Deal damage to the current enemy
[RAINAction]
public class AttackEnemy : RAINAction
{
    private AudioManager audio;

    public override void Start(RAIN.Core.AI ai)
    {
        audio = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioManager>();

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
		bool canAttack = ai.WorkingMemory.GetItem<bool> ("CanAttack");

		// If we shouldn't attack this frame, decrement our cooldown value 
		if (!canAttack) 
		{
			return ActionResult.SUCCESS;
		}

		/* Attack the enemy and reset the cool down, so we don't attack next frame as well */
		ai.WorkingMemory.SetItem<bool> ("CanAttack", false);

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

        // Play a sound, so the player knows we are dealing damage
        if ((audio != null) && (audio.swordSounds.Length > 0))
        {
            ai.Body.GetComponent<AudioSource>().clip = audio.getRandomSwordSound();
            ai.Body.GetComponent<AudioSource>().Play();
        }

        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}