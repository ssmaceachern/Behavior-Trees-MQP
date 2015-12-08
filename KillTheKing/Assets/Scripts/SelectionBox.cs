using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// Script for drawing a rectangle and selecting all units that lie within it
public class SelectionBox : MonoBehaviour 
{
	private RectTransform selectRect = new RectTransform();	// The rectangle we will use to select our units.

	private Vector2 initClickPos = new Vector2();		// The positon of the initial click
	private Vector2 currentMousePos = new Vector2();	// The current position of the mouse
	private Vector2 difference = new Vector2();			// How far the mouse has moved in one frame
	private Vector2 currentAnchor = new Vector2();		// The anchor point at any given frame
	private GameObject[] selectedUnits = new GameObject[20];	// All units selected from the rectangle
	private Image selectionImg;
	private GameObject charPar;
	private PieMenu[] pieMenus;
	private GameObject[] selectableUnits = new GameObject[20];

	void Start()
	{
		selectRect = GetComponent<RectTransform> ();
		selectionImg = GetComponent<Image> ();
		selectionImg.enabled = false;
	}

	// Update is called once per frame
	void Update () 
	{
		// Check to see if the player clicked the mouse button
		if (Input.GetMouseButtonDown (0))
		{
			// We only want to select units that have pie menus
			charPar = GameObject.FindGameObjectWithTag("Characters");
			pieMenus = charPar.GetComponentsInChildren<PieMenu>();

			/* Select units */
			for (int i = 0; i < pieMenus.Length; i++)
			{
				selectableUnits[i] = pieMenus[i].gameObject;
				selectableUnits[i].GetComponent<SelectUnit>().Deselect();
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
			int numSelected = 0;
			for (int i = 0; i < pieMenus.Length; i++)
			{
				// Check to see if the units lie within the RectTransform
				if (IsSelected (selectableUnits[i]))
				{
					selectedUnits[numSelected] = selectableUnits[i];
					selectedUnits[numSelected].GetComponent<SelectUnit>().MakeSelected ();

					numSelected++;
				}
			}

			// Reset
			initClickPos = Vector2.zero;
			selectRect.anchoredPosition = Vector2.zero;
			selectRect.sizeDelta = Vector2.zero;
			selectionImg.enabled = false;
		}
	}

	// Check to see if the given game object lies within the current transform
	private bool IsSelected(GameObject testObj)
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
}