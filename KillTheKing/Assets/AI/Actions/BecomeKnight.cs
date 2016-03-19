using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class BecomeKnight : RAINAction
{

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
        GameObject charPar = GameObject.FindGameObjectWithTag("Characters");

        GameObject newKnight = (GameObject)GameObject.Instantiate(Resources.Load("Knight"));

        newKnight.transform.position = ai.Body.transform.position;
        newKnight.transform.parent = charPar.transform;

        GameObject particle = (GameObject)GameObject.Instantiate(Resources.Load("EvilAura"));
        particle.GetComponent<ParticleFade>().followTarget = newKnight;

        if (charPar.GetComponent<FreezeGameplay>().IsFrozen())
        {
            newKnight.GetComponentInChildren<AIRig>().AI.IsActive = false;
        }

        ai.Body.SetActive(false);

        return ActionResult.SUCCESS;
    }
 
}