using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// Script for drawing a rectangle and selecting all units that lie within it
public class SelectionBox : MonoBehaviour 
{
	public float unitSpacing = 5f;

	private RectTransform selectRect = new RectTransform();	// The rectangle we will use to select our units
	private Vector2 initClickPos = new Vector2();		// The positon of the initial click
	private Vector2 currentMousePos = new Vector2();	// The current position of the mouse
	private Vector2 difference = new Vector2();			// How far the mouse has moved in one frame
	private Vector2 currentAnchor = new Vector2();		// The anchor point at any given frame
	private Image selectionImg;							// The image used for the selection box. Should be 1 pixel big
	private GameObject charPar;							// The parent class of the characters we can select
	private MessageReceiver[] rcvrs;					// The list of message receivers we can use to select units
	private MessageDispatcher dispatch;					// A reference to our own message dispatcher, so we can select units

	void Start()
	{
		// Set up our references
		selectRect = GetComponent<RectTransform> ();
		dispatch = GetComponent<MessageDispatcher> ();
		selectionImg = GetComponent<Image> ();
		charPar = GameObject.FindGameObjectWithTag("Characters");
		selectionImg.enabled = false;					// Only enable the image while the player is clicking.

		/* 
		 * If we don't have a message dispatcher then we can't select units, so print an error message
		 * and deactivate this component.
		 */
		if (dispatch == null)
		{
			Debug.LogError (this.gameObject.name + " needs a messageDispatcher");
			enabled = false;
		}
	}

	// Update is called once per frame
	void Update () 
	{
		// Send a move command to all selected units when user presses RMB
		if (Input.GetMouseButtonDown(1))
		{
			// Get the list of receivers to send to
			rcvrs = charPar.GetComponentsInChildren<MessageReceiver>();

			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(ray, out hit, 1000.0f) != false)
			{
				Vector3 movePoint = hit.point;

				// Send the desired location to all units
				foreach (MessageReceiver rcvr in rcvrs)
				{
					dispatch.SendMsg (0.0f,
					                  this.gameObject,
					                  rcvr.gameObject,
					                  (int) MessageTypes.MsgType.MoveTo,
					                  RandomizePoint (movePoint));
				}
			}

			// Deselect all units
			foreach (MessageReceiver rcvr in rcvrs)
			{
				dispatch.SendMsg(0.0f,
				                 this.gameObject,
				                 rcvr.gameObject,
				                 (int) MessageTypes.MsgType.DeselectUnit,
				                 null);
			}
		}

		// Check to see if the player clicked the mouse button
		if (Input.GetMouseButtonDown (0))
		{
			// We only want to select units that have message receivers
			rcvrs = charPar.GetComponentsInChildren<MessageReceiver>();

			// Deselect all units
			foreach (MessageReceiver rcvr in rcvrs)
			{
				dispatch.SendMsg(0.0f,
				                 this.gameObject,
				                 rcvr.gameObject,
				                 (int) MessageTypes.MsgType.DeselectUnit,
				                 null);
			}

			// Set the initial position
			initClickPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

			// Set the anchor position
			selectRect.anchoredPosition = initClickPos;
			selectionImg.enabled = true;
		}
		// While dragging
		else if (Input.GetMouseButton (0))
		{
			// Store the current mouse position in screen space.
			currentMousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

			// Find how far we have moved the mouse
			difference = currentMousePos - initClickPos;

			// Change the anchor based on which quadrant we are trying to represent
			currentAnchor = initClickPos;

			// Account for which quadrant we are dragging the mouse in
			if (difference.x < 0)
			{
				currentAnchor.x = currentMousePos.x;
				difference.x *= -1;
			}
			if (difference.y < 0)
			{
				currentAnchor.y = currentMousePos.y;
				difference.y *= -1;
			}

			// Set the anchor, width and height at every frame
			selectRect.anchoredPosition = currentAnchor;
			selectRect.sizeDelta = difference;
		}
		// Handle unit selection upon release of the mouse button
		else if (Input.GetMouseButtonUp (0))
		{
			/* Select units */
			rcvrs = charPar.GetComponentsInChildren<MessageReceiver>();
			foreach (MessageReceiver rcvr in rcvrs)
			{
				dispatch.SendMsg (0.0f,
				                  this.gameObject,
				                  rcvr.gameObject,
				                  (int) MessageTypes.MsgType.SelectUnit,
				                  null);
			}

			// Reset
			initClickPos = Vector2.zero;
			selectRect.anchoredPosition = Vector2.zero;
			selectRect.sizeDelta = Vector2.zero;
			selectionImg.enabled = false;
		}
	}

	// Check to see if the given game object lies within the current transform
	public bool IsSelected(GameObject testObj)
	{
		Vector2 screenPos = Camera.main.WorldToScreenPoint (testObj.transform.position);

		Vector2 anchor = selectRect.anchoredPosition;
		Vector2 size = selectRect.sizeDelta;

		if (screenPos.x >= anchor.x && screenPos.x <= (anchor.x + size.x))
		{
			if (screenPos.y >= anchor.y && screenPos.y <= (anchor.y + size.y))
			{
				return true;
			}
		}

		return false;
	}

	private Vector3 RandomizePoint(Vector3 initPoint)
	{
		float randomX = initPoint.x;
		float randomZ = initPoint.z;

		randomX = Random.Range (randomX - unitSpacing, randomX + unitSpacing);
		randomZ = Random.Range (randomZ - unitSpacing, randomZ + unitSpacing);

		Vector3 newPoint = new Vector3 (randomX, initPoint.y, randomZ);

		return newPoint;
	}
}