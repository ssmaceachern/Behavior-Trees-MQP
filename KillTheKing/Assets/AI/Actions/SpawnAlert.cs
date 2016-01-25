using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class SpawnAlert : RAINAction
{
    public override ActionResult Execute(RAIN.Core.AI ai)
    {
		GameObject particle = (GameObject)GameObject.Instantiate (Resources.Load ("Alert"));
		particle.transform.position = new Vector3 (ai.Body.transform.position.x, ai.Body.transform.position.y+5.0f, ai.Body.transform.position.z);
		particle.GetComponent<ParticleFade> ().followTarget = ai.Body;

        return ActionResult.SUCCESS;
    }

}