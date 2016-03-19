using UnityEngine;
using System.Collections;

public class PauseGame : MonoBehaviour 
{
	public GameObject PauseMenu;
	private bool paused = false;
    private float gameSpeed = 1.0f;

	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.Escape) == true)
		{
			paused = !paused;

            if (paused)
                Time.timeScale = 0;
            else
                Time.timeScale = gameSpeed;
			
			PauseMenu.SetActive (paused);
		}
	}

    // Allow the player to set the timeScale from the puase menu.
    public void SetTimeScale(float newGameSpeed)
    {
        gameSpeed = newGameSpeed;
    }

    // Allow the player to reset the timeScale to 1 fromt the pause menu
    public void InitTimeScale()
    {
        gameSpeed = 1.0f;
    }
}
