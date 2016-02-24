using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class SendKnightAhead : RAINAction
{
    public override ActionResult Execute(RAIN.Core.AI ai)
    {
        GameObject mySlave = ai.WorkingMemory.GetItem<GameObject>("PossibleSlave");

        if (mySlave == null)
            return ActionResult.SUCCESS;

        bool Fleeing = mySlave.GetComponentInChildren<AIRig>().AI.WorkingMemory.GetItem<bool>("Fleeing");
        int Hp = mySlave.GetComponentInChildren<AIRig>().AI.WorkingMemory.GetItem<int>("Health");

        if (Fleeing || Hp <= 0 || !mySlave.activeSelf)
        {
            ai.WorkingMemory.SetItem<GameObject>("PossibleSlave", null);
            return ActionResult.SUCCESS;
        }

        mySlave.GetComponentInChildren<AIRig>().AI.WorkingMemory.SetItem<bool>("PathWalker", true);

        int oldFear = ai.WorkingMemory.GetItem<int>("Fear");
        ai.WorkingMemory.SetItem<int>("Fear", oldFear - 30);

        
        ai.WorkingMemory.SetItem<int>("Rooted", 30);

        return ActionResult.SUCCESS;
    }
}