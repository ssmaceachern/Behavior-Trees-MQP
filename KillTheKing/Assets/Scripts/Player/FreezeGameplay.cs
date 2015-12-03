using UnityEngine;
using System.Collections;
using RAIN.Core;
using RAIN.Minds;
using RAIN.BehaviorTrees;

// Turn off all behavior trees when the player presses spacebar
public class FreezeGameplay : MonoBehaviour 
{
	private bool frozen = false;	// Whether the game s currently frozen
	private GameObject freezeIMG;

    void Start()
    {
        freezeIMG = GameObject.Find("FreezeIMG");
        freezeIMG.SetActive(false);
    }

	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown ("space") && !frozen)
		{
			frozen = true;

			AIRig[] ais = GetComponentsInChildren<AIRig>();
			for (int i = 0; i < ais.Length; i++)
			{
				ais[i].AI.IsActive = false;
			}

            if (freezeIMG)
            {
                freezeIMG.SetActive(true);
            }
		}
		else if (Input.GetKeyDown ("space") && frozen)
		{
			frozen = false;
			AIRig[] ais = GetComponentsInChildren<AIRig>();
			for (int i = 0; i < ais.Length; i++)
			{
				ais[i].AI.IsActive = true;
			}

            if (freezeIMG)
            {
                freezeIMG.SetActive(false);
            }
        }
	}

	public bool IsFrozen()
	{
		return frozen;
	}
}
