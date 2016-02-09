using UnityEngine;
using System.Collections;
using RAIN.Core;

public class DisplayThoughts : MonoBehaviour 
{
	public SpriteRenderer checkerRenderer;		// The icon of the person checking the trap
	public SpriteRenderer whatToCheckRenderer;	// The icon of the thing being checked
	public SpriteRenderer actionRenderer;		// The icon of the action
	public float timeToDisplay;					// The time to display the thought bubble
	/*
	 * Checker icon indices:
	 * 
	 * 0: King
	 * 1: Guard
	 */
	public Sprite[] checkerIcons;			// The icons of the different people who can check things
	/*
	 * ToCheck icon indices:
	 * 
	 * 0: Unknown
	 * 1: Archer
	 * 2: Thug
	 * 3: Trapper
	 * 4: Assassin
	 */
	public Sprite[] toCheckIcons;			// The icons of the different things we can check
	/*
	 * Action icon indices:
	 * 
	 * 0: Default arrow
	 * 1: Fight arrow
	 */
	public Sprite[] actionIcons;			// The icons of the different actions we want to explain

	private SpriteRenderer bubbleRenderer;	// The renderer of the base bubble
	private float timeLeft;					// The seconds left until we stop displaying the thought bubble
	private AIRig parentAI;					// The AI of the parent object

	// Use this for initialization
	void Start () 
	{
		bubbleRenderer = GetComponent<SpriteRenderer> ();

		// Deactivate the thought bubble in the initial state
		TurnOff ();
	}

	void Update()
	{
		parentAI = transform.parent.gameObject.GetComponentInChildren<AIRig> ();

		// Check the parent's AI to turn on at certain points
		GameObject target = parentAI.AI.WorkingMemory.GetItem<GameObject> ("Target");
		GameObject enemy = parentAI.AI.WorkingMemory.GetItem<GameObject> ("Enemy");

		if (enemy != null) {
			//Debug.Log ("WTF");
			string enemyType = enemy.GetComponentInChildren<AIRig> ().AI.WorkingMemory.GetItem<string> ("UnitType");
			int unitType = 0;

			switch (enemyType) {
			case "Archer":
				unitType = 1;
				break;
			case "Thug":
				unitType = 2;
				break;
			case "Trapper":
				unitType = 3;
				break;
			case "Assassin":
				unitType = 4;
				break;
			default:
				unitType = 0;
				break;
			}

			TurnOn (0, unitType, 1);
		} else if (target != null) {
			TurnOn (0, 0, 0);
		} else {
			//Debug.Log ("No Enemy");
		}
		timeLeft -= Time.deltaTime;

		if (timeLeft <= 0.0f)
		{
			TurnOff ();

		}
	}

	// Turn on the thought bubble to be alerted
	public void TurnOn(int checkerIndex, int toCheckIndex, int actionIndex)
	{
		bubbleRenderer.enabled = true;

		if (checkerRenderer != null)
		{
			// Make sure a valid index was passed
			checkerIndex = (checkerIndex % checkerIcons.Length);
			checkerRenderer.sprite = checkerIcons[checkerIndex];

			checkerRenderer.enabled = true;
		}

		if (whatToCheckRenderer != null)
		{
			// Make sure a valid index was passed
			toCheckIndex = (toCheckIndex % toCheckIcons.Length);
			whatToCheckRenderer.sprite = toCheckIcons[toCheckIndex];

			whatToCheckRenderer.enabled = true;
		}

		if (actionRenderer != null)
		{
			actionIndex = (actionIndex % actionIcons.Length);
			actionRenderer.sprite = actionIcons[actionIndex];

			actionRenderer.enabled = true;
		}

		timeLeft = timeToDisplay;
	}

	// Turn off the thought bubble
	public void TurnOff()
	{
		bubbleRenderer.enabled = false;
		
		if (checkerRenderer != null)
		{
			checkerRenderer.enabled = false;
		}
		
		if (whatToCheckRenderer != null)
		{
			whatToCheckRenderer.enabled = false;
		}

		if (actionRenderer != null)
		{
			actionRenderer.enabled = false;
		}
	}
}
