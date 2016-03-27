using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;
using RAIN.Entities;
using RAIN.Entities.Aspects;

[RAINAction]
public class BecomeMerc : RAINAction
{

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
        GameObject newMerc;

        GameObject charPar = GameObject.FindGameObjectWithTag("Characters");

        if (Random.value <= 0.5)
        {
            newMerc = (GameObject)GameObject.Instantiate(Resources.Load("Thug"));
        }
        else
        {
            newMerc = (GameObject)GameObject.Instantiate(Resources.Load("Archer"));
        }

        newMerc.transform.position = ai.Body.transform.position;
		newMerc.transform.parent = charPar.transform;
		newMerc.GetComponentInChildren<EntityRig> ().Entity.GetAspect("Rebel").IsActive = false;



        GameObject particle = (GameObject)GameObject.Instantiate(Resources.Load("Halo"));
        particle.GetComponent<ParticleFade>().followTarget = newMerc;
        particle.GetComponent<ParticleFade>().timeTillFade=5;


        if (charPar.GetComponent<FreezeGameplay>().IsFrozen())
        {
            newMerc.GetComponentInChildren<AIRig>().AI.IsActive = false;
        }


        ai.Body.SetActive(false);

        return ActionResult.SUCCESS;
    }
    
}