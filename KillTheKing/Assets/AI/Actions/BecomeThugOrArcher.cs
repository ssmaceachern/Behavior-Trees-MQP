using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class BecomeThugOrArcher : RAINAction
{
	public override ActionResult Execute(RAIN.Core.AI ai)
	{
		GameObject newMerc;

		if(Random.value <= 0.5) 
		{
			
			newMerc = (GameObject)GameObject.Instantiate (Resources.Load ("Thug"));
		} 
		else 
		{
			
			newMerc = (GameObject)GameObject.Instantiate (Resources.Load ("Archer"));
		}
		
		newMerc.transform.position = ai.Body.transform.position;
		



		ai.Body.SetActive (false);
		
		
		return ActionResult.SUCCESS;
	}
}