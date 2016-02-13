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
		if (msg.msgType == (int) MessageTypes.MsgType.DealDamage  || msg.msgType == (int)MessageTypes.MsgType.GhoulBomb)
		{
			AIRig mercAI = GetComponentInChildren<AIRig>();
			int currentHealth = mercAI.AI.WorkingMemory.GetItem<int>("Health");

			// Decrement our health by the amount of damage contained in the message
			mercAI.AI.WorkingMemory.SetItem<int>("Health", (currentHealth - (int)msg.info));

			GameObject particle = (GameObject)GameObject.Instantiate (Resources.Load ("Blood"));
			particle.transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
			Rigidbody hisBod = particle.GetComponent<Rigidbody> ();
			Vector3 nudgeForce = new Vector3 ();
			nudgeForce=(transform.position-msg.sender.transform.position)*50;
			nudgeForce.x += (Random.value*100-50);
			nudgeForce.y = 300;
			nudgeForce.z += (Random.value*100-50);
			hisBod.AddForce(nudgeForce);
		}
		else if (msg.msgType == (int) MessageTypes.MsgType.MoveTo)
		{
			if (msg.sender.GetComponent<SelectionBox>() != null)
			{
				if (!selected)
					return;
			}

			AIRig mercAI = GetComponentInChildren<AIRig>();

			mercAI.AI.WorkingMemory.SetItem<Vector3>("Location", (Vector3)msg.info);
            mercAI.AI.WorkingMemory.SetItem<bool>("Moving", true);

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
		else if (msg.msgType == (int)MessageTypes.MsgType.PriestHeal)
		{
			AIRig mercAi = GetComponentInChildren<AIRig>();
			
			int oldHealth = mercAi.AI.WorkingMemory.GetItem<int>("Health");
			
			if(oldHealth<100)
			{
				mercAi.AI.WorkingMemory.SetItem<int>("Health", oldHealth + 10);
				
				GameObject particle = (GameObject)GameObject.Instantiate (Resources.Load ("Heart"));
				particle.transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
				Rigidbody hisBod = particle.GetComponent<Rigidbody> ();
				Vector3 nudgeForce = new Vector3 ();
				nudgeForce.x = (Random.value*100-50);
				nudgeForce.y = 300;
				nudgeForce.z = (Random.value*100-50);
				hisBod.AddForce(nudgeForce);
			}
		}
		else if (msg.msgType == (int)MessageTypes.MsgType.GreenSong)
		{
			AIRig mercAi = GetComponentInChildren<AIRig>();
			
			int oldHealth = mercAi.AI.WorkingMemory.GetItem<int>("Health");
			
			if(oldHealth<100)
			{
				mercAi.AI.WorkingMemory.SetItem<int>("Health", oldHealth + 10);
				
				GameObject particle = (GameObject)GameObject.Instantiate (Resources.Load ("Heart"));
				particle.transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
				Rigidbody hisBod = particle.GetComponent<Rigidbody> ();
				Vector3 nudgeForce = new Vector3 ();
				nudgeForce.x = (Random.value*100-50);
				nudgeForce.y = 300;
				nudgeForce.z = (Random.value*100-50);
				hisBod.AddForce(nudgeForce);
			}
		}
	}
}
