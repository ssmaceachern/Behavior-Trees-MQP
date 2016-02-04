using UnityEngine;
using System.Collections;

public class PauseGame : MonoBehaviour 
{
	public GameObject PauseMenu;
	private bool paused = false;
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.Escape) == true)
		{
			paused = !paused;
			
			if (paused)
				Time.timeScale = 0;
			else
				Time.timeScale = 1;
			
			PauseMenu.SetActive (paused);
		}
	}
}
