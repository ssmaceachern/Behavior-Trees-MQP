using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class SpawnVomit : RAINAction
{
    public override ActionResult Execute(RAIN.Core.AI ai)
    {
		GameObject particle = (GameObject)GameObject.Instantiate (Resources.Load ("Puke"));
		particle.transform.position = new Vector3 (ai.Body.transform.position.x, ai.Body.transform.position.y, ai.Body.transform.position.z);
		Rigidbody hisBod = particle.GetComponent<Rigidbody> ();
		Vector3 nudgeForce = new Vector3 ();
		nudgeForce.x = (Random.value*300-150);
		nudgeForce.y = 400;
		nudgeForce.z = (Random.value*300-150);
		hisBod.AddForce(nudgeForce);


        return ActionResult.SUCCESS;
    }
}