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
		//Debug.Log(ai.WorkingMemory.GetItem<string> ("UnitType"));

		if (ai.WorkingMemory.GetItem<string> ("UnitType") == "") { // if you're something that just dies with no other tweaking needed

			ai.Body.SetActive (false);
			return ActionResult.SUCCESS;
			
		} else if (ai.WorkingMemory.GetItem<string> ("UnitType") == "King") { // if you're a king

			ai.WorkingMemory.SetItem<int> ("Health", -1);

			ai.Body.SetActive (false);

            if (LevelCoordinator.instance.GetLevelRegistry().ContainsKey(LevelCoordinator.instance.currentLevel))
            {
                LevelCoordinator.instance.GetLevelRegistry()[LevelCoordinator.instance.currentLevel].setComplete(true);
                Debug.Log(LevelCoordinator.instance.currentLevel + ": " + 
                    LevelCoordinator.instance.GetLevelRegistry()[LevelCoordinator.instance.currentLevel].isComplete);
            }
            else{
                Debug.LogError("Could not update level information");
            }

			Application.LoadLevel (2);

			return ActionResult.SUCCESS;

		} else if (ai.WorkingMemory.GetItem<string> ("UnitType") == "Knight") { // if you're a guard

			ai.WorkingMemory.SetItem<bool> ("Fleeing", true);

			ai.WorkingMemory.SetItem<int> ("Health", -1);

			ai.Body.SetActive (false);
			return ActionResult.SUCCESS;

		} else if (ai.WorkingMemory.GetItem<string> ("UnitType") == "Thug") { // if you're a thug
		
			ai.WorkingMemory.SetItem<int> ("Health", -1);
		
			ai.Body.SetActive (false);
			return ActionResult.SUCCESS;
		} else if (ai.WorkingMemory.GetItem<string> ("UnitType") == "Archer") { // if you're an archer
			
			ai.WorkingMemory.SetItem<int> ("Health", -1);
			
			ai.Body.SetActive (false);
			return ActionResult.SUCCESS;
		} else if (ai.WorkingMemory.GetItem<string> ("UnitType") == "Trapper") { // if you're a trapper
		
			ai.WorkingMemory.SetItem<int> ("Health", -1);
		
			ai.Body.SetActive (false);
			return ActionResult.SUCCESS;
		} else if (ai.WorkingMemory.GetItem<string> ("UnitType") == "Goblin") { // if you're a Goblin
			
			ai.WorkingMemory.SetItem<int> ("Health", -1);
			
			GameObject.Destroy (ai.Body);
			return ActionResult.SUCCESS;
		} else if (ai.WorkingMemory.GetItem<string> ("UnitType") == "Dragon") { // if you're a Dragon
			
			ai.WorkingMemory.SetItem<int> ("Health", -1);
			
			ai.Body.SetActive (false);

			//TODO: send a message to everyone around you to take x damage

			return ActionResult.SUCCESS;
		} else if (ai.WorkingMemory.GetItem<string> ("UnitType") == "Bear") { // if you're a Bear
			
			ai.WorkingMemory.SetItem<int> ("Health", -1);

			GameObject spawner = GameObject.FindGameObjectWithTag("GoblinRespawn");

			if (spawner != null)
			{
				spawner.GetComponent<SpawnGoblin>().spawnAGoblin();
			}

			ai.Body.SetActive (false);
			return ActionResult.SUCCESS;

		} else if (ai.WorkingMemory.GetItem<string> ("UnitType") == "Peasant") { // if you're a Peasant
			
			ai.WorkingMemory.SetItem<int> ("Health", -1);
			
			ai.Body.SetActive (false);
			return ActionResult.SUCCESS;

			//TODO: care more if a peasant dies that can be put here, or should that be a per-level script attached to them?

		} else if (ai.WorkingMemory.GetItem<string> ("UnitType") == "unitSpawner") { // if you're a unitSpawner
			
			ai.Body.SetActive (false);
			return ActionResult.SUCCESS;
		} else if (ai.WorkingMemory.GetItem<string> ("UnitType") == "Assassin") { // if you're an assasssin
			
			ai.Body.SetActive (false);
			return ActionResult.SUCCESS;
		} else if (ai.WorkingMemory.GetItem<string> ("UnitType") == "Ghost") { // if you're a ghost

			ai.Body.SetActive (false);

		}
		
		Debug.Log("Please make a Die() entry for unit type: " + ai.WorkingMemory.GetItem<string> ("UnitType"));

		// how did i get here?

		ai.Body.SetActive (false);
        return ActionResult.FAILURE;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}