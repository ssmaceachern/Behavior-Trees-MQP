using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class Die : RAINAction
{
    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
	{
		GameObject ANull = ai.WorkingMemory.GetItem<GameObject> ("ANull");

		if (ai.WorkingMemory.GetItem<int> ("unitType") == 0) { // if you're something that just dies with no other tweaking needed
			
			ai.Body.SetActive (false);
			return ActionResult.SUCCESS;

		}
		else if (ai.WorkingMemory.GetItem<int> ("unitType") == 1) { // if you're a king

			GameObject slaveOne = ai.WorkingMemory.GetItem<GameObject> ("Slave1");
			if (slaveOne != ANull) {
				slaveOne.GetComponentInChildren<AIRig> ().AI.WorkingMemory.SetItem<GameObject> ("Master", ANull);
			}

			GameObject slaveTwo = ai.WorkingMemory.GetItem<GameObject> ("Slave2");
			if (slaveTwo != ANull) {
				slaveTwo.GetComponentInChildren<AIRig> ().AI.WorkingMemory.SetItem<GameObject> ("Master", ANull);
			}

			GameObject slaveThree = ai.WorkingMemory.GetItem<GameObject> ("Slave3");
			if (slaveThree != ANull) {
				slaveThree.GetComponentInChildren<AIRig> ().AI.WorkingMemory.SetItem<GameObject> ("Master", ANull);
			}

			ai.Body.SetActive (false);
			return ActionResult.SUCCESS;

		} else if (ai.WorkingMemory.GetItem<int> ("unitType") == 3) { // if you're a thug

			ai.Body.SetActive (false);
			return ActionResult.SUCCESS;

		} else if (ai.WorkingMemory.GetItem<int> ("unitType") == 2) { // if you're a guard

			ai.WorkingMemory.SetItem<bool> ("Fleeing", true);

			GameObject myKing = ai.WorkingMemory.GetItem<GameObject> ("Master");


			GameObject slave1 = myKing.GetComponentInChildren<AIRig> ().AI.WorkingMemory.GetItem<GameObject> ("Slave1");
			string slave1name = slave1.GetComponentInChildren<AIRig> ().AI.WorkingMemory.GetItem<string> ("Name");

			if (slave1name == ai.WorkingMemory.GetItem<string> ("Name")) {
				myKing.GetComponentInChildren<AIRig> ().AI.WorkingMemory.SetItem<GameObject> ("Slave1", ANull);
				ai.Body.SetActive (false);
				return ActionResult.SUCCESS;
			}

		
			GameObject slave2 = myKing.GetComponentInChildren<AIRig> ().AI.WorkingMemory.GetItem<GameObject> ("Slave2");
			string slave2name = slave2.GetComponentInChildren<AIRig> ().AI.WorkingMemory.GetItem<string> ("Name");

			if (slave2name == ai.WorkingMemory.GetItem<string> ("Name")) {
				myKing.GetComponentInChildren<AIRig> ().AI.WorkingMemory.SetItem<GameObject> ("Slave2", ANull);
				ai.Body.SetActive (false);
				return ActionResult.SUCCESS;
			}

			GameObject slave3 = myKing.GetComponentInChildren<AIRig> ().AI.WorkingMemory.GetItem<GameObject> ("Slave3");
			string slave3name = slave3.GetComponentInChildren<AIRig> ().AI.WorkingMemory.GetItem<string> ("Name");
		
			if (slave3name == ai.WorkingMemory.GetItem<string> ("Name")) {
				myKing.GetComponentInChildren<AIRig> ().AI.WorkingMemory.SetItem<GameObject> ("Slave3", ANull);
				ai.Body.SetActive (false);
				return ActionResult.SUCCESS;
			}

			// how did i get here?

			ai.Body.SetActive (false);
		}

		// how did i get here?

        return ActionResult.FAILURE;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}