using UnityEngine;
using System.Collections;

public class ToggleTutorials : MonoBehaviour {

    GameObject MainMenuGroup;
    public int MoveDistance = 1350;
    int toggle = -1;

	public void LoadLevelSelector()
    {
        MainMenuGroup = GameObject.Find("MainMenuGroup");

        if(MainMenuGroup != null)
        {
            transform.position = new Vector3(transform.position.x + (MoveDistance * toggle),
                transform.position.y,
                transform.position.z);
        }
    }
}
