using UnityEngine;
using System.Collections;

public class ToolTipManager : MonoBehaviour
{
    // The game object whose tool tips we are currently displaying
    private GameObject toolTipEntity;
    private MessageDispatcher dispatch;

    void Start()
    {
        dispatch = GetComponent<MessageDispatcher>();
        toolTipEntity = null;
    }

	// Update is called once per frame
	void Update ()
    {
        // Ray cast down and see if we hit an entity
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Check to see if we hit anything
        if (Physics.Raycast(ray, out hit, 200.0f) != false)
        {
            // Make sure the thing we hit has a tool tip to display
            if (hit.collider.gameObject.GetComponentInChildren<ToolTipMessageReceiver>() != null)
            {
                // If we are already looking at a tool tip, don't display another one
                if (toolTipEntity != null)
                    return;

                // If so, keep a reference to the tool tip so we don't activate two at once.
                toolTipEntity = hit.collider.gameObject.GetComponentInChildren<ToolTipMessageReceiver>().gameObject;

                // Send a message to activate the tool tip
                if (dispatch != null)
                {
                    dispatch.SendMsg(0.0f,
                                     this.gameObject,
                                     toolTipEntity,
                                     (int)MessageTypes.MsgType.ActivateEntity,
                                     null);
                }
                else
                {
                    Debug.Log("No dispatcher for ToolTipManager");
                }
            }
            else
            {
                // If we hit something without a tool tip, send a message to deactivate the last tool tip
                // we hit.
                if (dispatch != null && toolTipEntity != null)
                {
                    dispatch.SendMsg(0.0f,
                                     this.gameObject,
                                     toolTipEntity,
                                     (int)MessageTypes.MsgType.Deactivate,
                                     null);

                    // Stop keeping track of the stored tool tip, so we can activate another one
                    toolTipEntity = null;
                }
            }
        }
    }
}
