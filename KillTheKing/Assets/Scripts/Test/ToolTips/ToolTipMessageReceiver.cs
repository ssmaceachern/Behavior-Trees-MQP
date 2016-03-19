using UnityEngine;
using System.Collections;

public class ToolTipMessageReceiver : MessageReceiver
{
    public override void ReceiveMessage(Message msg)
    {
        if (msg.msgType == (int)MessageTypes.MsgType.ActivateEntity)
        {
            GetComponent<ActivateToolTips>().Activate();
        }
        else if (msg.msgType == (int)MessageTypes.MsgType.Deactivate)
        {
            GetComponent<ActivateToolTips>().Deactivate();
        }
    }
}
