using UnityEngine;
using System.Collections;
using System;

public class TutorialTextMessageReceiver : MessageReceiver
{
    public override void ReceiveMessage(Message msg)
    {
        if (msg.msgType == (int) MessageTypes.MsgType.Deactivate)
        {
            this.gameObject.SetActive(false);
        }
        else if (msg.msgType == (int) MessageTypes.MsgType.ActivateEntity)
        {
            // Set active and focus the camera
            this.gameObject.SetActive(true);
            
            // Focus the camera on our position
            Vector3 newPos = new Vector3(transform.position.x,
                                         Camera.main.transform.position.y,
                                         transform.position.z);
            // Unless otherwise specified
            if (msg.info != null)
            {
                newPos = (Vector3)msg.info;
            }

            Camera.main.transform.position = newPos;
        }
    }

}
