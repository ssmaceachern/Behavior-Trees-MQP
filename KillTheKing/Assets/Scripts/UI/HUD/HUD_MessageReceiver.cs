using UnityEngine;
using System.Collections;

public class HUD_MessageReceiver : MessageReceiver
{
    public override void ReceiveMessage(Message msg)
    {
        if (msg.msgType == (int)MessageTypes.MsgType.UpdateParanoia)
        {
            GetComponent<TurnOnBars>().ChangeParanoia((int)msg.info);
        }
        else if (msg.msgType == (int)MessageTypes.MsgType.UpdateGreed)
        {
            GetComponent<TurnOnBars>().ChangeGreed((int)msg.info);
        }
        else if (msg.msgType == (int)MessageTypes.MsgType.UpdateFear)
        {
            GetComponent<TurnOnBars>().ChangeFear((int)msg.info);
        }
    }
}
