using UnityEngine;
using System.Collections;

public class LevelMarker : MonoBehaviour {

    public GameObject[] NextLevels;
    public LevelMarker Parent;
    private LevelInfo levelInfo;
    public string LevelName;

    public bool isTutorial;
    public bool isComplete;
    public bool isRoot;

	// Use this for initialization
	void Start () {
	    if(Parent == null)
        {
            isRoot = true;
        }

        if(LevelCoordinator.instance.GetLevelRegistry().TryGetValue(LevelName, out levelInfo))
        {
            isComplete = levelInfo.isComplete;
        }
        else
        {
            Debug.Log("LevelMarker: Could not get level");
            
        }

        Debug.Log(levelInfo.isComplete);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown()
    {
        //If this is the first level, then load the next scene. Otherwise, 
        //if the parent level has been completed, then we load that scene as well
        if (isRoot)
        {
            //This is a corner case for when the first root level is a tutorial
            if (isTutorial)
            {
                LevelCoordinator.instance.LoadLevel(LevelName);
            }
            else
            {
                LevelCoordinator.instance.LoadLevelScene(LevelName);
            } 
        }
        else if (Parent.isComplete)
        {
            LevelCoordinator.instance.LoadLevelScene(LevelName);
        }
        else if(isTutorial)
        {
            LevelCoordinator.instance.LoadLevel(LevelName);
        }
        else
        {
            Debug.Log("Could not load level");
            Debug.Log(Parent.isComplete);
        }
        // this object was clicked - do something
        Debug.Log("Level button clicked");
    }
}
