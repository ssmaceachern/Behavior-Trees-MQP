using UnityEngine;
using System.Collections;

public class PersonalityGUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        // Make a background box
        GUI.Box(new Rect(Screen.width - 100, Screen.height - 50, 100, 50), "Selected Guard Stats");


    }
}
