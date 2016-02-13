using UnityEngine;
using System.Collections;

public class ToggleTutorials : MonoBehaviour {

    GameObject MainMenuGroup;
    public int MoveDistance = 1350;
    int toggle = -1;

	public void LoadLevelSelector(string whichGroup)
    {
		MainMenuGroup = GameObject.Find(whichGroup);

		Debug.Log (whichGroup);
		if (MainMenuGroup != null)
		{
			for (int i = 0; i < MainMenuGroup.transform.childCount; i++)
			{
				MainMenuGroup.transform.GetChild(i).gameObject.SetActive (true);
			}
			this.gameObject.SetActive (false);
		}
    }
}
