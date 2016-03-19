using UnityEngine;
using System.Collections;

public class HandleStatement : MonoBehaviour 
{
	private SpriteRenderer actorRen;	// The renderer of the actor sprite
	private SpriteRenderer actionRen;	// The renderer of the action sprite
	private SpriteRenderer recipRen;	// The renderer of the recipient sprite

	// Use this for initialization
	void Awake () 
	{
		// Set up our references
		actorRen = transform.FindChild ("Actor").gameObject.GetComponent <SpriteRenderer> ();
		actionRen = transform.FindChild ("Action").gameObject.GetComponent <SpriteRenderer> ();
		recipRen = transform.FindChild ("Recipient").gameObject.GetComponent <SpriteRenderer> ();

		// Make sure they were set up properly
		if (actorRen == null) {
			Debug.Log (gameObject.name + ": No renderer for an actor sprite found");
			return;
		}
		if (actionRen == null) {
			Debug.Log (gameObject.name + ": No renderer for an action sprite found");
			return;
		}
		if (recipRen == null) {
			Debug.Log (gameObject.name + ": No renderer for a recipient sprite found");
			return;
		}
	}

	// Display a statement with icons referenced by index
	public void DisplayStatement(int actor, int action, int recip)
	{
		actorRen.sprite = StatementIconRegistry.GetActor (actor);
		actionRen.sprite = StatementIconRegistry.GetAction (action);
		recipRen.sprite = StatementIconRegistry.GetRecipient (recip);

		actorRen.enabled = true;
		actionRen.enabled = true;
		recipRen.enabled = true;
	}

	// Display a statement with icons referenced by name
	public void DisplayStatement(string actor, string action, string recip)
	{
		actorRen.sprite = StatementIconRegistry.GetActor (actor);
		actionRen.sprite = StatementIconRegistry.GetAction (action);
		recipRen.sprite = StatementIconRegistry.GetRecipient (recip);
		
		actorRen.enabled = true;
		actionRen.enabled = true;
		recipRen.enabled = true;
	}

	// Display a theshold statement
	public void DisplayThreshold(int actor, int action, bool increasing)
	{
		actorRen.sprite = StatementIconRegistry.GetActor (actor);
		actionRen.sprite = StatementIconRegistry.GetAction (action);
		recipRen.sprite = null;

		actorRen.enabled = true;
		actionRen.enabled = true;
		recipRen.enabled = false;

		// Turn the action sprite to indicate whether which way we are crossing a theshold
		if (increasing)
			actionRen.transform.Rotate (new Vector3 (0.0f, 0.0f, 90f));
		else
			actionRen.transform.Rotate (new Vector3 (0.0f, 0.0f, -90f));
	}

	// Display a threshold via string
	public void DisplayThreshold(string actor, bool increasing)
	{
		actorRen.sprite = StatementIconRegistry.GetActor (actor);
		if (increasing)
			actionRen.sprite = StatementIconRegistry.GetAction ("Increase");
		else
			actionRen.sprite = StatementIconRegistry.GetAction ("Check");
		recipRen.sprite = null;

		actorRen.enabled = true;
		actionRen.enabled = true;
		recipRen.enabled = false;
		
		// Turn the action sprite to indicate whether which way we are crossing a theshold
		if (increasing)
			actionRen.transform.Rotate (new Vector3 (0.0f, 0.0f, 90f));
		else
			actionRen.transform.Rotate (new Vector3 (0.0f, 0.0f, 90f));
	}

	// Initialize the statement and turn it off
	public void InitStatement()
	{
		actorRen.sprite = null;
		actorRen.enabled = true;

		actionRen.sprite = null;
		actionRen.enabled = true;

		recipRen.sprite = null;
		recipRen.enabled = true;

		transform.position = transform.parent.position;
		transform.localPosition = new Vector3 (0.0f, 0.0f, 0.0f);
		transform.rotation = new Quaternion();
		transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
	}

	public void DestroyStatement()
	{
		Destroy (this.gameObject);
	}
}
