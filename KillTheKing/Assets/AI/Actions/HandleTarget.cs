using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;
using RAIN.Entities;
using RAIN.Entities.Aspects;

[RAINAction]
public class HandleTarget : RAINAction
{
    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
		GameObject myTrap = ai.WorkingMemory.GetItem<GameObject> ("Target");
		MessageDispatcher dispatch = ai.Body.GetComponent<MessageDispatcher> ();

		if (myTrap == null) {
			return ActionResult.SUCCESS;
		}

		string myType = ai.WorkingMemory.GetItem<string> ("UnitType");
		string itsType = myTrap.GetComponentInChildren<AIRig> ().AI.WorkingMemory.GetItem<string> ("TrapType");

		if (myType=="Knight") { // if this is a knight that's been ordered to check a trap by the king

			if (itsType == "FoodBribe") { // Food bribe trap

				int oldHunger = ai.WorkingMemory.GetItem<int> ("Hunger");
				int oldLoyalty = ai.WorkingMemory.GetItem<int> ("Loyalty");
				ai.WorkingMemory.SetItem<int> ("Loyalty", oldLoyalty-oldHunger);
				ai.WorkingMemory.SetItem<int> ("Hunger", 0);

				GameObject myKing = ai.WorkingMemory.GetItem<GameObject> ("Master");

				if(myKing != null)
				{
					int oldGreed=myKing.GetComponentInChildren<AIRig> ().AI.WorkingMemory.GetItem<int> ("Greed");
					myKing.GetComponentInChildren<AIRig> ().AI.WorkingMemory.SetItem<int> ("Greed", oldGreed+30);

					// Increase the King's greed by a set amount
					dispatch.SendMsg (0.0f,
					                  ai.Body,
					                  myKing,
					                  (int)MessageTypes.MsgType.MakeGreedy,
					                  30);

					// Now that the task is complete, let the king know he can select other guards for other tasks
					dispatch.SendMsg (0.0f,
					                  ai.Body,
					                  myKing,
					                  (int)MessageTypes.MsgType.CheckTrap,
					                  null);
				}
				// Deactivate the trap and forget about it.
				myTrap.SetActive(false);				
				ai.WorkingMemory.SetItem<GameObject>("Target", null);

			} else if (itsType == "Spike") { // Spike trap
	
				int oldHealth = ai.WorkingMemory.GetItem<int> ("Health");
				ai.WorkingMemory.SetItem<int> ("Health", oldHealth - 30);
				
				GameObject myKing = ai.WorkingMemory.GetItem<GameObject> ("Master");

				// Ensure we have a master
				if(myKing != null)
				{
					// Make the king less greedy since he just saw a guard get hurt
					dispatch.SendMsg (0.0f,
					                  ai.Body,
					                  myKing,
					                  (int)MessageTypes.MsgType.MakeGreedy,
					                  -10);

					// Now that the task is complete, let the king know he can select other guards for other tasks
					dispatch.SendMsg (0.0f,
					                  ai.Body,
					                  myKing,
					                  (int)MessageTypes.MsgType.CheckTrap,
					                  null);
				}

				// Deactivate the trap and forget about it
				myTrap.SetActive(false);				
				ai.WorkingMemory.SetItem<GameObject>("Target", null);

			} else if (itsType == "Food") { // Normal Food
			
				ai.WorkingMemory.SetItem<int> ("Hunger", 0);

				// Deactivate the trap and forget about it
				myTrap.SetActive(false);	
				ai.WorkingMemory.SetItem<GameObject>("Target", null);

				GameObject myKing = ai.WorkingMemory.GetItem<GameObject> ("Master");
				// Now that the task is complete, let the king know he can select other guards for other tasks
				dispatch.SendMsg (0.0f,
				                  ai.Body,
				                  myKing,
				                  (int)MessageTypes.MsgType.CheckTrap,
				                  null);
				
			} else if (itsType == "Vomit") { // Vomit trap
				
				ai.WorkingMemory.SetItem<bool> ("Puking", true);

				// Deactivate the trap and forget about it
				myTrap.SetActive(false);			
				ai.WorkingMemory.SetItem<GameObject>("Target", null);

				GameObject myKing = ai.WorkingMemory.GetItem<GameObject> ("Master");
				dispatch.SendMsg (0.0f,
				                  ai.Body,
				                  myKing,
				                  (int)MessageTypes.MsgType.CheckTrap,
				                  null);

			} else if (itsType == "Tavern") { // Normal Tavern
				
				ai.WorkingMemory.SetItem<int> ("Hunger", 0);
				ai.WorkingMemory.SetItem<int> ("Health", 101);

				// Should this really be how we want to do it?  Shouldn't they have the ability to go to the tavern
				// multiple times?
				EntityRig pEnt = myTrap.GetComponentInChildren<AIRig> ().AI.Body.GetComponentInChildren<EntityRig> ();
				pEnt.Entity.DeactivateEntity();
				
				ai.WorkingMemory.SetItem<GameObject>("Target", null);

			} else if (itsType == "DrunkTavern") { // bribed "Drunk" Tavern
				
				ai.WorkingMemory.SetItem<int> ("Hunger", 0);
				ai.WorkingMemory.SetItem<int> ("Health", 101);
				ai.WorkingMemory.SetItem<int> ("WalkSpeed", 2);

				EntityRig pEnt = myTrap.GetComponentInChildren<AIRig> ().AI.Body.GetComponentInChildren<EntityRig> ();
				
				pEnt.Entity.DeactivateEntity();

				ai.WorkingMemory.SetItem<GameObject>("Target", null);

			} else if (itsType == "PoisonTavern") { // bribed "poisoned beer" Tavern
			
				ai.WorkingMemory.SetItem<int> ("Hunger", 0);
				int oldHealth = ai.WorkingMemory.GetItem<int> ("Health");
				ai.WorkingMemory.SetItem<int> ("Health", oldHealth - 40);

				EntityRig pEnt = myTrap.GetComponentInChildren<AIRig> ().AI.Body.GetComponentInChildren<EntityRig> ();
				
				pEnt.Entity.DeactivateEntity();

				ai.WorkingMemory.SetItem<GameObject>("Target", null);
			
			} else if (itsType == "DisloyalTavern") { // bribed "Disloyal gossip" Tavern
				
				ai.WorkingMemory.SetItem<int> ("Hunger", 0);
				int oldHealth = ai.WorkingMemory.GetItem<int> ("Health");
				ai.WorkingMemory.SetItem<int> ("Health", oldHealth + 20);
				int oldLoyal = ai.WorkingMemory.GetItem<int> ("Loyalty");
				ai.WorkingMemory.SetItem<int> ("Loyalty", oldLoyal - 40);

				EntityRig pEnt = myTrap.GetComponentInChildren<AIRig> ().AI.Body.GetComponentInChildren<EntityRig> ();
				
				pEnt.Entity.DeactivateEntity();

				ai.WorkingMemory.SetItem<GameObject>("Target", null);
				
			}

		} else if(myType=="King") { // the king is greedy and activates the trap himself

			if (itsType == "FoodBribe") { // Food bribe trap

				int oldHealth = ai.WorkingMemory.GetItem<int> ("Health");
				ai.WorkingMemory.SetItem<int> ("Health", oldHealth + 10);
				
				int oldGreed=ai.WorkingMemory.GetItem<int> ("Greed");
				ai.WorkingMemory.SetItem<int> ("Greed", oldGreed+30);
				
			} else if (itsType == "Spike") { // Spike trap
				
				int oldHealth = ai.WorkingMemory.GetItem<int> ("Health");
				ai.WorkingMemory.SetItem<int> ("Health", oldHealth - 30);
				
				int oldGreed=ai.WorkingMemory.GetItem<int> ("Greed");
				ai.WorkingMemory.SetItem<int> ("Greed", oldGreed-10);
				
			} else if (itsType == "Vomit") { // Vomit trap
				
				int oldHealth = ai.WorkingMemory.GetItem<int> ("Health");
				ai.WorkingMemory.SetItem<int> ("Health", oldHealth - 10);
				
				int oldGreed=ai.WorkingMemory.GetItem<int> ("Greed");
				ai.WorkingMemory.SetItem<int> ("Greed", oldGreed-20);

			} else if (itsType == "Food") { // Normal Food
				
				int oldHealth = ai.WorkingMemory.GetItem<int> ("Health");
				ai.WorkingMemory.SetItem<int> ("Health", oldHealth + 30);
			}

			myTrap.SetActive(false);

			ai.WorkingMemory.SetItem<GameObject>("Target", null);
		}

        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}