using UnityEngine;
using System.Collections;
using RAIN.Core;

public class DeactivateOnUnitHire : MonoBehaviour
{
    public GameObject unitToCheck;     // The unit to check the hire status of.

    private AIRig unitAI;              // The AI of the given unit to check the hire status.
    private MessageDispatcher dispatch;
	// Use this for initialization
	void Start ()
    {
        unitAI = unitToCheck.GetComponentInChildren<AIRig>();
        dispatch = GetComponent<MessageDispatcher>();
    }

    // Update is called once per frame
    void Update ()
    {
        Vector3 unitLoc = unitAI.AI.WorkingMemory.GetItem<Vector3>("Location");
        Vector3 unitInitPos = unitAI.AI.WorkingMemory.GetItem<Vector3>("InitPos");

        if (unitLoc != unitInitPos)
        {
            Debug.Log("Message Sent");
            dispatch.SendMsg(0.0f,
                 this.gameObject,
                 this.gameObject,
                 (int)MessageTypes.MsgType.Deactivate,
                 null);
        }
	}
}
