using UnityEngine;
using System.Collections;
using RAIN.Core;

public class DisplayThoughts : MonoBehaviour 
{
	public float timeToDisplay;					// The time to display the thought bubble
	public float statementSpacing = 0.525f;		// The amount of vertical space in between each statement
	public Vector3 statementStart = new Vector3 (-0.125f, 0.65f, 0.0f);	// The space the first statement will start at.

	private SpriteRenderer bubbleRenderer;		// The renderer of the thought bubble sprite
	private AIRig parentAI;						// The AI of the containing body
	private GameObject[] currentStatements = new GameObject[3];	// A list of all of the statements currently being displayed
	private float[] timeLeftForStatements = new float[3];		// The time left for us to display the corresponding statement
	private int numStatements = 0;								// The number of statements we are displaying
	private GameObject knownEnemy;
	private GameObject knownTarget;
	public Queue thing;

	// Use this for initialization
	void Start () 
	{
		bubbleRenderer = GetComponent<SpriteRenderer> ();

		// Deactivate the thought bubble in the initial state
		bubbleRenderer.enabled = false;
	}

	void Update()
	{
		parentAI = transform.parent.gameObject.GetComponentInChildren<AIRig> ();

		// Check the parent's AI to turn on at certain points
		GameObject target = parentAI.AI.WorkingMemory.GetItem<GameObject> ("Target");
		GameObject enemy = parentAI.AI.WorkingMemory.GetItem<GameObject> ("Enemy");

		if ((enemy != null) && (knownEnemy == null))
		{
			string enemyType = enemy.GetComponentInChildren<AIRig> ().AI.WorkingMemory.GetItem<string> ("UnitType");

			TurnOn ("King", "Fight", enemyType);

			knownEnemy = enemy;
		}
		else
		{
			if (enemy == null)
				knownEnemy = null;
		}

        if ((target != null) && (knownTarget == null))
        {
            TurnOn("King", "Check", "Unknown");

            knownTarget = target;
        }
        else
        {
            if (target == null)
                knownTarget = null;
        }

		// See if any statements have expired and turn them off if so
		for (int i = 0; i < numStatements; i++)
		{
			timeLeftForStatements[i] -= Time.deltaTime;

			if (timeLeftForStatements[i] <= 0.0f)
			{
				PopStatement();
			}
		}

		if (numStatements >= 1) {
			bubbleRenderer.enabled = true;
		} else
			bubbleRenderer.enabled = false;
	}

	// Turn on the thought bubble to be alerted
	public void TurnOn(int actorIndex, int actionIndex, int recipIndex)
	{
		GameObject newStatement = Instantiate<GameObject>(Resources.Load<GameObject> ("Statement"));
		newStatement.transform.parent = this.gameObject.transform;

		newStatement.GetComponent<HandleStatement> ().InitStatement ();
		newStatement.GetComponent<HandleStatement> ().DisplayStatement (actorIndex, actionIndex, recipIndex);

		if (numStatements >= 3)
			PopStatement ();
		PushStatement (newStatement);
	}

	public void TurnOn(string actorName, string actionName, string recipName)
	{
		GameObject newStatement = Instantiate<GameObject>(Resources.Load<GameObject> ("Statement"));
		newStatement.transform.parent = this.gameObject.transform;

		newStatement.GetComponent<HandleStatement> ().InitStatement ();
		newStatement.GetComponent<HandleStatement> ().DisplayStatement (actorName, actionName, recipName);

		if (numStatements >= 3)
			PopStatement ();
		PushStatement (newStatement);
	}

    public void TurnOnThreshold(int actorIndex, int actionIndex, bool increasing)
    {
		GameObject newStatement = Instantiate<GameObject>(Resources.Load<GameObject> ("Statement"));
		newStatement.transform.parent = this.gameObject.transform;

		newStatement.GetComponent<HandleStatement> ().InitStatement ();
		newStatement.GetComponent<HandleStatement> ().DisplayThreshold (actorIndex, actionIndex, increasing);

		if (numStatements >= 3)
			PopStatement ();
		PushStatement (newStatement);
	}

	public void TurnOnThreshold(string actorName, bool increasing)
	{
		GameObject newStatement = Instantiate<GameObject>(Resources.Load<GameObject> ("Statement"));
		newStatement.transform.parent = this.gameObject.transform;

		newStatement.GetComponent<HandleStatement> ().InitStatement ();
		newStatement.GetComponent<HandleStatement> ().DisplayThreshold (actorName, increasing);

		if (numStatements >= 3)
			PopStatement ();
		PushStatement (newStatement);
	}

	// Pop the top statement off of the array
	public void PopStatement()
	{
		if (numStatements == 0)
			return;

		GameObject popped = currentStatements [0];

		for (int j = 0; j < (numStatements - 1); j++)
		{
			currentStatements[j] = currentStatements[j + 1];

			currentStatements[j].transform.localPosition = new Vector3(currentStatements[j].transform.localPosition.x,
			                                                      	   currentStatements[j].transform.localPosition.y + (statementSpacing / 2),
			                                                     	   currentStatements[j].transform.localPosition.z);

			timeLeftForStatements[j] = timeLeftForStatements[j + 1];
		}

		if (popped != null)
			Destroy (popped);

		numStatements--;
	}

	// Push a new statement onto the bottom of the array
	public void PushStatement(GameObject statement)
	{
		if (numStatements >= 3)
			return;

		if (numStatements > 0)
		{
			statement.transform.localPosition = currentStatements [numStatements - 1].transform.localPosition;
			statement.transform.localPosition = new Vector3(statement.transform.localPosition.x,
		    	                                       statement.transform.localPosition.y - (statementSpacing / 2f),
		        	                                   statement.transform.localPosition.z);
		}
		else
		{
			statement.transform.localPosition = statementStart;
		}

		currentStatements [numStatements] = statement;
		currentStatements [numStatements].transform.localPosition = new Vector3 (statement.transform.localPosition.x,
		                                                                        statement.transform.localPosition.y + (statementSpacing / 2f),
		                                                                        statement.transform.localPosition.z);
		timeLeftForStatements [numStatements] = timeToDisplay;

		numStatements++;
	}
}
