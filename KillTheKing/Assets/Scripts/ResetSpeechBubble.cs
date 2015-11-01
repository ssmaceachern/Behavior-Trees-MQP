using UnityEngine;
using System.Collections;

// Reset the text above an NPC's head.
public class ResetSpeechBubble : MonoBehaviour 
{
	public float timeToWait = 5f;
	private TextMesh speechBubble;
	private float timeLeft;

	// Use this for initialization
	void Start () 
	{
		speechBubble = GetComponentInParent<TextMesh> ();
		timeLeft = timeToWait;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(speechBubble.text != "")
		{
			timeLeft -= Time.deltaTime;
			if(timeLeft <= 0.0f)
			{
				speechBubble.text = "";
				timeLeft = timeToWait;
			}
		}

	}
}
