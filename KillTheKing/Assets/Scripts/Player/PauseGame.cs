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
            if (paused)
                UnPause();
            else
                Pause();
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

    public void Pause()
    {
        paused = true;
        Time.timeScale = 0;

        PauseMenu.SetActive(true);
    }

    public void UnPause()
    {
        paused = false;
        Time.timeScale = 1;

        PauseMenu.SetActive(false);
    }

    public void FF()
    {
        Time.timeScale = 2.0f;
    }

    public bool isPaused()
    {
        return paused;
    }
}
