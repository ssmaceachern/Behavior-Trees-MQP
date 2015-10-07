using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

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
		GameObject ANull = ai.WorkingMemory.GetItem<GameObject> ("ANull");

		if (myTrap == ANull) {
			return ActionResult.SUCCESS;
		}

		int itsType = myTrap.GetComponentInChildren<AIRig> ().AI.WorkingMemory.GetItem<int> ("Type");

		if (!ai.WorkingMemory.GetItem<bool> ("IsKing")) { // if this is a knight

			if (itsType == 1) { // Food trap

				int oldHunger = ai.WorkingMemory.GetItem<int> ("Hunger");
				int oldLoyalty = ai.WorkingMemory.GetItem<int> ("Loyalty");
				ai.WorkingMemory.SetItem<int> ("Loyalty", oldLoyalty-oldHunger);
				ai.WorkingMemory.SetItem<int> ("Hunger", 0);

				GameObject myKingg = ai.WorkingMemory.GetItem<GameObject> ("Master");
				if(myKingg!=ANull)
				{
					int oldGreed=myKingg.GetComponentInChildren<AIRig> ().AI.WorkingMemory.GetItem<int> ("Greed");
					myKingg.GetComponentInChildren<AIRig> ().AI.WorkingMemory.SetItem<int> ("Greed", oldGreed+30);
				}

			} else if (itsType == 2) { // Spike trap
	
				int oldHealth = ai.WorkingMemory.GetItem<int> ("Health");
				ai.WorkingMemory.SetItem<int> ("Health", oldHealth - 30);
				
				GameObject myKingg = ai.WorkingMemory.GetItem<GameObject> ("Master");
				if(myKingg!=ANull)
				{
					int oldGreed=myKingg.GetComponentInChildren<AIRig> ().AI.WorkingMemory.GetItem<int> ("Greed");
					if(oldGreed>=10)
					{
						myKingg.GetComponentInChildren<AIRig> ().AI.WorkingMemory.SetItem<int> ("Greed", oldGreed-10);
					}
				}

			} else if (itsType == 3) { // Normal Food
			
				ai.WorkingMemory.SetItem<int> ("Hunger", 0);
			}

			
			myTrap.SetActive(false);
			
			ai.WorkingMemory.SetItem<GameObject>("Target", ANull);
			GameObject myKing = ai.WorkingMemory.GetItem<GameObject> ("Master");
			myKing.GetComponentInChildren<AIRig> ().AI.WorkingMemory.SetItem<GameObject> ("Target", ANull);

		} else { // the king is greedy and activates the trap himself

			if (itsType == 1) { // Food trap
				
				
				int oldHealth = ai.WorkingMemory.GetItem<int> ("Health");
				ai.WorkingMemory.SetItem<int> ("Health", oldHealth + 30);
				
				int oldGreed=ai.WorkingMemory.GetItem<int> ("Greed");
				ai.WorkingMemory.SetItem<int> ("Greed", oldGreed+30);
				
			} else if (itsType == 2) { // Spike trap
				
				int oldHealth = ai.WorkingMemory.GetItem<int> ("Health");
				ai.WorkingMemory.SetItem<int> ("Health", oldHealth - 30);
				
				int oldGreed=ai.WorkingMemory.GetItem<int> ("Greed");
				ai.WorkingMemory.SetItem<int> ("Greed", oldGreed-10);
				
			} else if (itsType == 3) { // Normal Food
				
				ai.WorkingMemory.SetItem<GameObject>("Target", ANull);
			}

			myTrap.SetActive(false);

			ai.WorkingMemory.SetItem<GameObject>("Target", ANull);
		}

        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}