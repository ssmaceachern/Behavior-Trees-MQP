using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class BardSong : RAINAction
{
 
    public override ActionResult Execute(RAIN.Core.AI ai)
	{
		MessageDispatcher dispatch = ai.Body.GetComponent<MessageDispatcher> ();

		string mySong = ai.WorkingMemory.GetItem<string> ("Command");

		if(mySong=="BlueSong")
		{	
			GameObject particle = (GameObject)GameObject.Instantiate (Resources.Load ("BlueSong"));
			particle.GetComponent<ParticleFade> ().followTarget = ai.Body;

			dispatch.BroadcastMsg (0.0f,
			                       ai.Body,
			                       ai.Body.transform.position,
			                       20,
			                       (int)MessageTypes.MsgType.BlueSong,
			                       10);
		}
		else if(mySong=="GreenSong")
		{	
			GameObject particle = (GameObject)GameObject.Instantiate (Resources.Load ("GreenSong"));
			particle.GetComponent<ParticleFade> ().followTarget = ai.Body;
			
			dispatch.BroadcastMsg (0.0f,
			                       ai.Body,
			                       ai.Body.transform.position,
			                       20,
			                       (int)MessageTypes.MsgType.GreenSong,
			                       10);
		}
		
		return ActionResult.SUCCESS;
    }

}