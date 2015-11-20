using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GoldTracker : MonoBehaviour 
{
	[Range(0, 10000)]
	public int initGold = 100;			// The amount of gold the player will have at the start of the level
	[Range(0, 10000)]
	public int gps = 1;					// The amount of gold the player will receive at the appropriate update time
	[Range(0, 10000)]
	public int goldIncreaseRate = 5;	// The rate at which the player will get gold (the # of frames until the player gets more gold)
	public GameObject goldUI;			// The text displaying the amount of gold to the player

	private int goldAmount;
	private int timeTilGold;		// The number of frames left until the player gets more gold

	// Use this for initialization
	void Start () 
	{
		goldAmount = initGold;
		timeTilGold = goldIncreaseRate;
	}
	
	// Update is called once per frame
	void Update () 
	{
		/* Check to see if we should award the player more gold */
		if (timeTilGold > 0)
		{
			timeTilGold--;
		}
		else
		{
			goldAmount += gps;
			timeTilGold = goldIncreaseRate;
		}

		/* Update the UI text to display the proper amount of gold */
		if (goldUI != null)
		{
			goldUI.GetComponent<Text> ().text = "Gold: " + goldAmount;
		}
	}

	// Changes the total amount of gold the player has by the given amount
	public void ChangeGold(int amtToChange)
	{
		goldAmount += amtToChange;
	}
}
