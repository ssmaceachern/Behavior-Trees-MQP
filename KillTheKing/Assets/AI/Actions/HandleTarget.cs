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
    private KingAttributeManager attMan;

    public override void Start(RAIN.Core.AI ai)
    {
        attMan = ai.Body.GetComponent<KingAttributeManager>();
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

		myTrap.GetComponentInChildren<EntityRig> ().Entity.GetAspect ("Chest").IsActive = false;
		myTrap.GetComponentInChildren<AIRig> ().AI.WorkingMemory.SetItem<bool> ("Used", true);


        /************************* Knights's way of handling traps ***********************/
		if (myType=="Knight") // if this is a knight that's been ordered to check a trap by the king
		{

			if (itsType == "FoodBribe") { // Food bribe trap

				int oldHunger = ai.WorkingMemory.GetItem<int> ("Hunger");
				int oldLoyalty = ai.WorkingMemory.GetItem<int> ("Loyalty");
				ai.WorkingMemory.SetItem<int> ("Loyalty", oldLoyalty-oldHunger);
				ai.WorkingMemory.SetItem<int> ("Hunger", 0);
				ai.WorkingMemory.SetItem<int> ("Health", 100);

				GameObject myKing = ai.WorkingMemory.GetItem<GameObject> ("Master");

				if(myKing != null)
				{
					int oldGreed=myKing.GetComponentInChildren<AIRig> ().AI.WorkingMemory.GetItem<int> ("Greed");
					myKing.GetComponentInChildren<AIRig> ().AI.WorkingMemory.SetItem<int> ("Greed", oldGreed+20);

					int oldFear=myKing.GetComponentInChildren<AIRig> ().AI.WorkingMemory.GetItem<int> ("Fear");
					myKing.GetComponentInChildren<AIRig> ().AI.WorkingMemory.SetItem<int> ("Fear", oldFear-20);

					int oldPara=myKing.GetComponentInChildren<AIRig> ().AI.WorkingMemory.GetItem<int> ("Paranoia");
					myKing.GetComponentInChildren<AIRig> ().AI.WorkingMemory.SetItem<int> ("Paranoia", oldPara-20);
				

					// Now that the task is complete, let the king know he can select other guards for other tasks
					dispatch.SendMsg (0.0f,
					                  ai.Body,
					                  myKing,
					                  (int)MessageTypes.MsgType.CheckTrap,
					                  null);
				}

				// Deactivate the trap and forget about it.
				ai.WorkingMemory.SetItem<GameObject>("Target", null);



			} else if (itsType == "Food") { // Normal Food
			
				ai.WorkingMemory.SetItem<int> ("Hunger", 0);

				// Deactivate the trap and forget about it
				ai.WorkingMemory.SetItem<GameObject>("Target", null);

				GameObject myKing = ai.WorkingMemory.GetItem<GameObject> ("Master");
				// Now that the task is complete, let the king know he can select other guards for other tasks
				dispatch.SendMsg (0.0f,
				                  ai.Body,
				                  myKing,
				                  (int)MessageTypes.MsgType.CheckTrap,
				                  null);

			} else if (itsType == "GoldBribe") { // random chest full of gold
				
				int oldScared = ai.WorkingMemory.GetItem<int> ("Fear");
				ai.WorkingMemory.SetItem<int> ("Fear", oldScared-30);

				
				GameObject myKing = ai.WorkingMemory.GetItem<GameObject> ("Master");
				if(myKing != null)
				{
					int oldGreed=myKing.GetComponentInChildren<AIRig> ().AI.WorkingMemory.GetItem<int> ("Greed");
					myKing.GetComponentInChildren<AIRig> ().AI.WorkingMemory.SetItem<int> ("Greed", oldGreed+30);
					
					int oldFear=myKing.GetComponentInChildren<AIRig> ().AI.WorkingMemory.GetItem<int> ("Fear");
					myKing.GetComponentInChildren<AIRig> ().AI.WorkingMemory.SetItem<int> ("Fear", oldFear-10);
					
					int oldPara=myKing.GetComponentInChildren<AIRig> ().AI.WorkingMemory.GetItem<int> ("Paranoia");
					myKing.GetComponentInChildren<AIRig> ().AI.WorkingMemory.SetItem<int> ("Paranoia", oldPara-10);
					
					
					// Now that the task is complete, let the king know he can select other guards for other tasks
					dispatch.SendMsg (0.0f,
					                  ai.Body,
					                  myKing,
					                  (int)MessageTypes.MsgType.CheckTrap,
					                  null);
				}

				for (int i=0; i<40; i++) 
				{
					GameObject particle = (GameObject)GameObject.Instantiate (Resources.Load ("Coin"));
					particle.transform.position = new Vector3 (myTrap.transform.position.x, myTrap.transform.position.y, myTrap.transform.position.z);
					Rigidbody hisBod = particle.GetComponent<Rigidbody> ();
					Vector3 nudgeForce = new Vector3 ();
					nudgeForce.x = (Random.value*500-250);
					nudgeForce.y = 300;
					nudgeForce.z = (Random.value*500-250);
					hisBod.AddForce(nudgeForce);
					hisBod.AddRelativeTorque(nudgeForce);
				}

				// Deactivate the trap and forget about it
				ai.WorkingMemory.SetItem<GameObject>("Target", null);


				
			} else if (itsType == "JackBox") { // Chest that scares anyone near
				
				int oldScared = ai.WorkingMemory.GetItem<int> ("Fear");
				ai.WorkingMemory.SetItem<int> ("Fear", oldScared+30);
				
				GameObject myKing = ai.WorkingMemory.GetItem<GameObject> ("Master");
				if(myKing != null)
				{
					int oldGreed=myKing.GetComponentInChildren<AIRig> ().AI.WorkingMemory.GetItem<int> ("Greed");
					myKing.GetComponentInChildren<AIRig> ().AI.WorkingMemory.SetItem<int> ("Greed", oldGreed-40);
					
					int oldFear=myKing.GetComponentInChildren<AIRig> ().AI.WorkingMemory.GetItem<int> ("Fear");
					myKing.GetComponentInChildren<AIRig> ().AI.WorkingMemory.SetItem<int> ("Fear", oldFear+10);
					
					int oldPara=myKing.GetComponentInChildren<AIRig> ().AI.WorkingMemory.GetItem<int> ("Paranoia");
					myKing.GetComponentInChildren<AIRig> ().AI.WorkingMemory.SetItem<int> ("Paranoia", oldPara+10);
					
					
					// Now that the task is complete, let the king know he can select other guards for other tasks
					dispatch.SendMsg (0.0f,
					                  ai.Body,
					                  myKing,
					                  (int)MessageTypes.MsgType.CheckTrap,
					                  null);
				}

				for(var i=0;i<20;i++)
				{
					
					GameObject particle = (GameObject)GameObject.Instantiate (Resources.Load ("Bile"));
					particle.transform.position = new Vector3 (ai.Body.transform.position.x, ai.Body.transform.position.y, ai.Body.transform.position.z);
					Rigidbody hisBod = particle.GetComponent<Rigidbody> ();
					Vector3 nudgeForce = new Vector3 ();
					nudgeForce.x = (Random.value*500-250);
					nudgeForce.y = 300;
					nudgeForce.z = (Random.value*250);
					hisBod.AddForce(nudgeForce);
				}

				GameObject spooked = (GameObject)GameObject.Instantiate (Resources.Load ("FearFace"));
				spooked.GetComponent<ParticleFade>().followTarget=ai.Body;
				
				// Deactivate the trap and forget about it
				ai.WorkingMemory.SetItem<GameObject>("Target", null);


			} else if (itsType == "Vomit") { // Vomit trap
				
				ai.WorkingMemory.SetItem<bool> ("Puking", true);

				
				int oldHunger = ai.WorkingMemory.GetItem<int> ("Hunger");
				ai.WorkingMemory.SetItem<int> ("Hunger", oldHunger+15);
				
				ai.WorkingMemory.SetItem<int> ("Rooted", 7);

				// Deactivate the trap and forget about it
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

			} 
			else if(itsType == "Spooky") 
			{
				// Deactivate the trap and forget about it
				ai.WorkingMemory.SetItem<GameObject>("Target", null);
				
				GameObject myKing = ai.WorkingMemory.GetItem<GameObject> ("Master");
				// Now that the task is complete, let the king know he can select other guards for other tasks
				dispatch.SendMsg (0.0f,
				                  ai.Body,
				                  myKing,
				                  (int)MessageTypes.MsgType.CheckTrap,
				                  null);
			} 
			else if(itsType == "SuperShield")
			{
				ai.WorkingMemory.SetItem<GameObject>("Target", null);

				GameObject myKing = ai.WorkingMemory.GetItem<GameObject> ("Master");
				dispatch.SendMsg (0.0f,
				                  ai.Body,
				                  myKing,
				                  (int)MessageTypes.MsgType.CheckTrap,
				                  null);

				GameObject bubble = (GameObject)GameObject.Instantiate (Resources.Load ("Supershield"));
				bubble.GetComponent<ParticleFade>().followTarget=ai.Body;

			}
			else 
			{ 
				Debug.Log("Knight encountered an unusual trap");
			}










        /************************* King's way of handling traps ***********************/
        } else if (myType=="King") { // the king is greedy and activates the trap himself

			if (itsType == "FoodBribe") { // Food bribe trap

                int oldHealth = ai.WorkingMemory.GetItem<int>("Health");
				ai.WorkingMemory.SetItem<int> ("Health", oldHealth + 10);
				
                attMan.ChangeAttribute("Greed", 30);

			} else if (itsType == "GoldBribe") { // A random pile of gold in a chest

                attMan.ChangeAttribute("Greed", 30);
				
                attMan.ChangeAttribute("Fear", -30);

                attMan.ChangeAttribute("Paranoia", -30);

				for (int i=0; i<40; i++) 
				{
					GameObject particle = (GameObject)GameObject.Instantiate (Resources.Load ("Coin"));
					particle.transform.position = new Vector3 (ai.Body.transform.position.x, ai.Body.transform.position.y, ai.Body.transform.position.z);
					Rigidbody hisBod = particle.GetComponent<Rigidbody> ();
					Vector3 nudgeForce = new Vector3 ();
					nudgeForce.x = (Random.value*500-250);
					nudgeForce.y = 300;
					nudgeForce.z = (Random.value*500-250);
					hisBod.AddForce(nudgeForce);
					hisBod.AddRelativeTorque(nudgeForce);
				}
				
			} else if (itsType == "JackBox") { // Spike trap
				
                attMan.ChangeAttribute("Greed", -40);
				
                attMan.ChangeAttribute("Fear", 10);
				
                attMan.ChangeAttribute("Paranoia", 10);

				for(var i=0;i<20;i++)
				{
					GameObject particle = (GameObject)GameObject.Instantiate (Resources.Load ("Bile"));
					particle.transform.position = new Vector3 (ai.Body.transform.position.x, ai.Body.transform.position.y, ai.Body.transform.position.z);
					Rigidbody hisBod = particle.GetComponent<Rigidbody> ();
					Vector3 nudgeForce = new Vector3 ();
					nudgeForce.x = (Random.value*500-250);
					nudgeForce.y = 300;
					nudgeForce.z = (Random.value*250);
					hisBod.AddForce(nudgeForce);
				}

			} else if (itsType == "Vomit") { // Vomit trap
				
				int oldHealth = ai.WorkingMemory.GetItem<int> ("Health");
				ai.WorkingMemory.SetItem<int> ("Health", oldHealth - 20);
				
                attMan.ChangeAttribute("Greed", -30);
				
				ai.WorkingMemory.SetItem<int> ("Rooted", 7);

				for(var i=0;i<5;i++)
				{
					GameObject particle = (GameObject)GameObject.Instantiate (Resources.Load ("Blood"));
					particle.transform.position = new Vector3 (ai.Body.transform.position.x, ai.Body.transform.position.y, ai.Body.transform.position.z);
					Rigidbody hisBod = particle.GetComponent<Rigidbody> ();
					Vector3 nudgeForce = new Vector3 ();
					nudgeForce.x = (Random.value*500-250);
					nudgeForce.y = 300;
					nudgeForce.z = (Random.value*250);
					hisBod.AddForce(nudgeForce);
				}

				for(var i=0;i<5;i++)
				{
					GameObject particle = (GameObject)GameObject.Instantiate (Resources.Load ("Puke"));
					particle.transform.position = new Vector3 (ai.Body.transform.position.x, ai.Body.transform.position.y, ai.Body.transform.position.z);
					Rigidbody hisBod = particle.GetComponent<Rigidbody> ();
					Vector3 nudgeForce = new Vector3 ();
					nudgeForce.x = (Random.value*500-250);
					nudgeForce.y = 300;
					nudgeForce.z = (Random.value*250);
					hisBod.AddForce(nudgeForce);
				}

			} else if (itsType == "Food") { // Normal Food
				
				int oldHealth = ai.WorkingMemory.GetItem<int> ("Health");
				ai.WorkingMemory.SetItem<int> ("Health", oldHealth + 30);

			} else if (itsType == "Spooky") { // Spooky trap

                attMan.ChangeAttribute("Greed", -ai.WorkingMemory.GetItem<int>("Greed"));

			} else { 
				Debug.Log("King encountered an unusual trap");
			}

			ai.WorkingMemory.SetItem<GameObject>("Target", null);

        /************************* Bear's way of handling traps ***********************/
		} else if(myType=="Bear") { // a bear comes over and activates the trap
			
			if (itsType == "FoodBribe") { // Food bribe trap
				
				int oldHealth = ai.WorkingMemory.GetItem<int> ("Health");
				ai.WorkingMemory.SetItem<int> ("Health", oldHealth + 10);

			} else if (itsType == "Vomit") { // Vomit trap
				
				int oldHealth = ai.WorkingMemory.GetItem<int> ("Health");
				ai.WorkingMemory.SetItem<int> ("Health", oldHealth - 10);
				
			} else if (itsType == "Food") { // Normal Food
				
				int oldHealth = ai.WorkingMemory.GetItem<int> ("Health");
				ai.WorkingMemory.SetItem<int> ("Health", oldHealth + 30);

			} else { 
				Debug.Log("Bear encountered an unusual trap");
			}
			
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
			
			ai.WorkingMemory.SetItem<GameObject>("Target", null);
		}

        return ActionResult.SUCCESS;
    }
}