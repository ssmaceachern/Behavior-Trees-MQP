using UnityEngine;
using System.Collections;

public class LevelLoad : MonoBehaviour 
{
    public void LoadLevel(int level)
    {
		PlayerPrefs.SetInt ("LastLevel", level);
        Application.LoadLevel(level);
    }

    public void LoadLevel(string level)
    {
        Application.LoadLevel(level);
    }

    public void LoadLastLevel()
	{
		Application.LoadLevel (PlayerPrefs.GetInt ("LastLevel"));
	}
}
