using UnityEngine;
using System.Collections;

public class HUD_MessageReceiver : MessageReceiver
{
    public override void ReceiveMessage(Message msg)
    {
        if (msg.msgType == (int)MessageTypes.MsgType.UpdateParanoia)
        {
            GetComponentInChildren<DisplayKingAttributes>().ChangeParanoia((int)msg.info);
        }
        else if (msg.msgType == (int)MessageTypes.MsgType.UpdateGreed)
        {
            GetComponentInChildren<DisplayKingAttributes>().ChangeGreed((int)msg.info);
        }
        else if (msg.msgType == (int)MessageTypes.MsgType.UpdateFear)
        {
            GetComponentInChildren<DisplayKingAttributes>().ChangeFear((int)msg.info);
        }
    }
}
