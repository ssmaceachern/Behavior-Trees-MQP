using UnityEngine;
using System.Collections;

/*
 * Acts as a registry for the icons that can be used to make a statement.
 * 
 * Statements are split into 3 parts:
 * 1. Actor
 * 2. Action
 * 3. Recipient
 * 
 * For example, if a king wants a knight to check a chest, the statement to be displayed would be something like...
 * 1. Knight
 * 2. Check
 * 3. Chest
 * ...where the Knight icon is the actor, the check icon is the action, and the chest icon is the recipient.
 */
public class StatementIconRegistry : MonoBehaviour 
{
	private static Sprite[] actorSprites;		// A list of sprites that can be used as an actor in a statement
	private static Sprite[] actionSprites;		// A list of sprites that can be used as actions in a statement
	private static Sprite[] recipientSprites;	// A list of sprites that can receive an action in a statement

	void Start()
	{
		actorSprites = Resources.LoadAll<Sprite> ("Icons/ActorSprites");
		actionSprites = Resources.LoadAll<Sprite> ("Icons/ActionSprites");
		recipientSprites = Resources.LoadAll<Sprite> ("Icons/RecipientSprites");

		if (actorSprites.Length == 0)
			Debug.Log ("No actor sprites found");
		if (actionSprites.Length == 0)
			Debug.Log ("No action sprites found");
		if (recipientSprites.Length == 0)
			Debug.Log ("No recipient sprites found");
	}

	// Get an actor by index
	public static Sprite GetActor(int index)
	{
		if ((index < actorSprites.Length) &&
		    (actorSprites[index] != null))
		{
			return actorSprites[index];
		}
		else
			return null;
	}

	// Get an actor by name
	public static Sprite GetActor(string name)
	{
		foreach (Sprite s in actorSprites)
		{
			if (s.name == name)
				return s;
		}
		
		return GetRecipient("Unknown");
	}

	// Get an action by index
	public static Sprite GetAction(int index)
	{
		if ((index < actionSprites.Length) &&
		    (actionSprites[index] != null))
		{
			return actorSprites[index];
		}
		else 
			return null;
	}

	// Get an action by name
	public static Sprite GetAction(string name)
	{
		foreach (Sprite s in actionSprites)
		{
			if (s.name == name)
				return s;
		}

		return GetRecipient("Unknown");
	}

	// Get a recipient by index
	public static Sprite GetRecipient(int index)
	{
		if ((index < recipientSprites.Length) &&
		    (recipientSprites[index] != null))
		{
			return actorSprites[index];
		}
		else 
			return null;
	}
	
	// Get a recipient by name
	public static Sprite GetRecipient(string name)
	{
		foreach (Sprite s in recipientSprites)
		{
			if (s.name == name)
				return s;
		}

		return GetRecipient("Unknown");
	}
}
