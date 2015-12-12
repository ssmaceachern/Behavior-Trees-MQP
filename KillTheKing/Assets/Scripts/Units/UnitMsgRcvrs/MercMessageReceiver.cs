using UnityEngine;
using System.Collections;
using RAIN.Core;
using RAIN.Entities;

// Define how mercenaries will handle various messages
public class MercMessageReceiver : MessageReceiver 
{
	private bool selected = false;

	public override void ReceiveMessage (Message msg)
	{
		if (msg.msgType == (int) MessageTypes.MsgType.DealDamage)
		{
			AIRig mercAI = GetComponentInChildren<AIRig>();
			int currentHealth = mercAI.AI.WorkingMemory.GetItem<int>("Health");

			// Decrement our health by the amount of damage contained in the message
			mercAI.AI.WorkingMemory.SetItem<int>("Health", (currentHealth - (int)msg.info));
		}
		else if (msg.msgType == (int) MessageTypes.MsgType.MoveTo)
		{
			if (msg.sender.GetComponent<SelectionBox>() != null)
			{
				if (!selected)
					return;

				EntityRig pEnt = GetComponentInChildren<AIRig>().AI.Body.GetComponentInChildren<EntityRig> ();
				
				if(pEnt!=null)
				{
					pEnt.Entity.GetAspect("Good").IsActive=true;
					//pEnt.Entity.ActivateEntity();
				}
			}

			AIRig mercAI = GetComponentInChildren<AIRig>();

			mercAI.AI.WorkingMemory.SetItem<Vector3>("Location", (Vector3)msg.info);
		}
		else if (msg.msgType == (int) MessageTypes.MsgType.GiveCommand)
		{
			AIRig mercAI = GetComponentInChildren<AIRig>();

			mercAI.AI.WorkingMemory.SetItem<string>("Command", (string)msg.info);
		}
		else if (msg.msgType == (int) MessageTypes.MsgType.ActivateEntity)
		{
			EntityRig mercEnt = GetComponentInChildren<EntityRig>();

			mercEnt.Entity.GetAspect ("Good").IsActive = true;
		}
		else if (msg.msgType == (int) MessageTypes.MsgType.SelectUnit)
		{
			SelectionBox selectBox = msg.sender.GetComponent<SelectionBox>();
			Transform indicatorTransform = transform.FindChild ("SelectionIndicator");
			MeshRenderer selectMesh = null;

			if (indicatorTransform != null)
			{
				selectMesh = indicatorTransform.GetComponent<MeshRenderer>();
			}

			// Perform whatever tasks we need to as a selected unit
			if (selectBox.IsSelected (this.gameObject))
			{
				if (selectMesh != null)
				{
					selectMesh.enabled = true;
					selected = true;
				}
			}
		}
		else if (msg.msgType == (int) MessageTypes.MsgType.DeselectUnit)
		{
			Transform indicatorTransform = transform.FindChild ("SelectionIndicator");
			MeshRenderer selectMesh = null;
			
			if (indicatorTransform != null)
			{
				selectMesh = indicatorTransform.GetComponent<MeshRenderer>();
			}

			if (selectMesh != null)
			{
				selectMesh.enabled = false;
				selected = false;
			}
		}
	}
}
