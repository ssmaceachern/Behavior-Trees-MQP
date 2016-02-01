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
		if (ai.WorkingMemory.GetItem<string> ("UnitType") == "King")
		{
			GameObject thoughtBubble = ai.Body.transform.FindChild ("ThoughtBubble").gameObject;
			thoughtBubble.GetComponent<DisplayThoughts>().TurnOn (0, 0, 0);
		}
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
					//int oldGreed=myKing.GetComponentInChildren<AIRig> ().AI.WorkingMemory.GetItem<int> ("Greed");
					//myKing.GetComponentInChildren<AIRig> ().AI.WorkingMemory.SetItem<int> ("Greed", oldGreed+30);

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

				for (int i=0; i<3; i++) {
					GameObject particle = (GameObject)GameObject.Instantiate (Resources.Load ("Blood"));
					particle.transform.position = new Vector3 (ai.Body.transform.position.x, ai.Body.transform.position.y, ai.Body.transform.position.z);
					Rigidbody hisBod = particle.GetComponent<Rigidbody> ();
					Vector3 nudgeForce = new Vector3 ();
					nudgeForce.x = (Random.value*300-150);
					nudgeForce.y = 400;
					nudgeForce.z = (Random.value*300-150);
					hisBod.AddForce(nudgeForce);
				}

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

				
				int oldHunger = ai.WorkingMemory.GetItem<int> ("Hunger");
				ai.WorkingMemory.SetItem<int> ("Hunger", oldHunger+15);


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
				
			} else if(itsType == "Spooky") {
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
			} else { 
				Debug.Log("Knight encountered an unusual trap");
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

				for (int i=0; i<3; i++) {
					GameObject particle = (GameObject)GameObject.Instantiate (Resources.Load ("Blood"));
					particle.transform.position = new Vector3 (ai.Body.transform.position.x, ai.Body.transform.position.y, ai.Body.transform.position.z);
					Rigidbody hisBod = particle.GetComponent<Rigidbody> ();
					Vector3 nudgeForce = new Vector3 ();
					nudgeForce.x = (Random.value*300-150);
					nudgeForce.y = 400;
					nudgeForce.z = (Random.value*300-150);
					hisBod.AddForce(nudgeForce);
				}
				
			} else if (itsType == "Vomit") { // Vomit trap
				
				int oldHealth = ai.WorkingMemory.GetItem<int> ("Health");
				ai.WorkingMemory.SetItem<int> ("Health", oldHealth - 10);
				
				int oldGreed=ai.WorkingMemory.GetItem<int> ("Greed");
				ai.WorkingMemory.SetItem<int> ("Greed", oldGreed-20);

			} else if (itsType == "Food") { // Normal Food
				
				int oldHealth = ai.WorkingMemory.GetItem<int> ("Health");
				ai.WorkingMemory.SetItem<int> ("Health", oldHealth + 30);
				
			} else if (itsType == "Spooky") { // Spooky trap

				ai.WorkingMemory.SetItem<int> ("Greed", 0);

			} else { 
				Debug.Log("King encountered an unusual trap");
			}


			myTrap.SetActive(false);

			ai.WorkingMemory.SetItem<GameObject>("Target", null);

			GameObject thoughtBubble = ai.Body.transform.FindChild ("ThoughtBubble").gameObject;
			thoughtBubble.GetComponent<DisplayThoughts>().TurnOff();

		} else if(myType=="Bear") { // a bear comes over and activates the trap
			
			if (itsType == "FoodBribe") { // Food bribe trap
				
				int oldHealth = ai.WorkingMemory.GetItem<int> ("Health");
				ai.WorkingMemory.SetItem<int> ("Health", oldHealth + 10);
			} else if (itsType == "Spike") { // Spike trap
				
				int oldHealth = ai.WorkingMemory.GetItem<int> ("Health");
				ai.WorkingMemory.SetItem<int> ("Health", oldHealth - 90);
			} else if (itsType == "Vomit") { // Vomit trap
				
				int oldHealth = ai.WorkingMemory.GetItem<int> ("Health");
				ai.WorkingMemory.SetItem<int> ("Health", oldHealth - 10);
				
			} else if (itsType == "Food") { // Normal Food
				
				int oldHealth = ai.WorkingMemory.GetItem<int> ("Health");
				ai.WorkingMemory.SetItem<int> ("Health", oldHealth + 30);
			} else { 
				Debug.Log("Bear encountered an unusual trap");
			}
			
			myTrap.SetActive(false);
			
			ai.WorkingMemory.SetItem<GameObject>("Target", null);

		} else if(myType=="Peasant") { // a peasant sees a trap and rushes over to activate it
			
			if (itsType == "FoodBribe") { // Food bribe trap
				
				int oldHealth = ai.WorkingMemory.GetItem<int> ("Health");
				ai.WorkingMemory.SetItem<int> ("Health", oldHealth + 10);
			} else if (itsType == "Spike") { // Spike trap
				
				int oldHealth = ai.WorkingMemory.GetItem<int> ("Health");
				ai.WorkingMemory.SetItem<int> ("Health", oldHealth - 90);
			} else if (itsType == "Vomit") { // Vomit trap
				
				int oldHealth = ai.WorkingMemory.GetItem<int> ("Health");
				ai.WorkingMemory.SetItem<int> ("Health", oldHealth - 10);
				
			} else if (itsType == "Food") { // Normal Food
				
				int oldHealth = ai.WorkingMemory.GetItem<int> ("Health");
				ai.WorkingMemory.SetItem<int> ("Health", oldHealth + 20);
			} else { 
				Debug.Log("Peasant encountered an unusual trap");
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