using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;
using RAIN.Representation;

[RAINAction]
public class AttackUnit : RAINAction
{
	public Expression enemyToAttack = new Expression ();


    public override ActionResult Execute(RAIN.Core.AI ai)
    {

		GameObject myEnemy = enemyToAttack.Evaluate<GameObject> (ai.DeltaTime, ai.WorkingMemory);

		
		if (myEnemy == null) 
		{
			return ActionResult.FAILURE;
		}


		MessageDispatcher dispatch = ai.Body.GetComponent<MessageDispatcher> ();

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

        return ActionResult.SUCCESS;
    }

}