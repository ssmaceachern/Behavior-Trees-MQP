using UnityEngine;
using System.Collections;

public class DeactivateTextOnButton : MonoBehaviour
{
    private MessageDispatcher dispatch;
	// Use this for initialization
	void Start ()
    {
        dispatch = GetComponent<MessageDispatcher>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown("space"))
        {
            dispatch.SendMsg(0.0f,
                             this.gameObject,
                             this.gameObject,
                             (int)MessageTypes.MsgType.Deactivate,
                             null);
        }
	}

}
