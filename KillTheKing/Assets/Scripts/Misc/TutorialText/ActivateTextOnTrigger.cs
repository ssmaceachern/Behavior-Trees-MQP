using UnityEngine;
using System.Collections;

public class ActivateTextOnTrigger : MonoBehaviour
{
    public GameObject textToActivate;   // The tutorial text we are activating.

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Entered");
        /* TODO: Check that the gameObject is the right one. */

        MessageDispatcher dispatch = GetComponent<MessageDispatcher>();

        dispatch.SendMsg(0.0f,
                         this.gameObject,
                         textToActivate,
                         (int)MessageTypes.MsgType.ActivateEntity,
                         null);
    }
}
