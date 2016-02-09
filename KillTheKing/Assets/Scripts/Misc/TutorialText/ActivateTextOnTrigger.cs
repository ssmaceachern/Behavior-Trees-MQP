using UnityEngine;
using System.Collections;

public class ActivateTextOnTrigger : MonoBehaviour
{
    public GameObject textToActivate;   // The tutorial text we are activating.

    void OnTriggerEnter(Collider other)
    {
        /* TODO: Check that the gameObject is the right one. i.e. The King */

        MessageDispatcher dispatch = GetComponent<MessageDispatcher>();

        dispatch.SendMsg(0.0f,
                         this.gameObject,
                         textToActivate,
                         (int)MessageTypes.MsgType.ActivateEntity,
                         null);

		// Freeze the gameplay so the player can read the message.
		GameObject.FindGameObjectWithTag ("Characters").GetComponent<FreezeGameplay> ().Freeze ();

        // Lastly, delete ourselves so we don't trigger again.
        Destroy(this.gameObject);
    }
}
