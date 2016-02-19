using UnityEngine;
using System.Collections;
using System;

public class TutorialTextMessageReceiver : MessageReceiver
{
    public override void ReceiveMessage(Message msg)
    {
        if (msg.msgType == (int) MessageTypes.MsgType.Deactivate)
        {
			Camera.main.gameObject.GetComponent<CenterCameraOnPoint>().FreeCamera();
            this.gameObject.SetActive(false);
        }
        else if (msg.msgType == (int) MessageTypes.MsgType.ActivateEntity)
        {
            // Set active and focus the camera
            this.gameObject.SetActive(true);
            
			Camera.main.gameObject.GetComponent<CenterCameraOnPoint>().SetPoint (this.gameObject.transform.position);
        }
    }

}
