using UnityEngine;
using System.Collections;

public class LevelLoad : MonoBehaviour 
{
	// Load a given level by index
    public void LoadLevel(int level)
    {
		PlayerPrefs.SetInt ("LastLevel", level);
        Application.LoadLevel(level);
    }

	// Load a given level by name
	public void LoadLevel(string level)
	{
		Application.LoadLevel(level);
	}

	// Load a given level by name without needing to define a LevelLoad object
    public static void StaticLoadLevel(string level)
    {
        Application.LoadLevel(level);
    }

	// Load the level that was last loaded before the level we are in currently
    public void LoadLastLevel()
	{
		Application.LoadLevel (PlayerPrefs.GetInt ("LastLevel"));
	}

	// Load the level we are currently on to reset the game.
	public void ReloadLevel()
	{
		Application.LoadLevel (Application.loadedLevel);
	}
}
