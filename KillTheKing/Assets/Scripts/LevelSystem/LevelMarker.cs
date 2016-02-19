using UnityEngine;
using System.Collections;

public class LevelMarker : MonoBehaviour {

    public GameObject[] NextLevels;
    public LevelMarker[] Parents;
    private LevelInfo levelInfo;
    public string LevelName;

    public bool isTutorial;
    public bool isComplete;
    public bool isRoot;

	// Use this for initialization
	void Start () {
		Renderer r = GetComponent<Renderer>();

		if(Parents == null)
        {
            isRoot = true;
        }

        if(LevelCoordinator.instance.GetLevelRegistry().TryGetValue(LevelName, out levelInfo))
        {
            isComplete = levelInfo.isComplete;
			Debug.Log(LevelName + ": " + levelInfo.isComplete);
        }
        else
        {
            Debug.Log("LevelMarker: Could not get level");
        }

		if(isComplete)
		{
			r.material = Resources.Load("Icon-Star") as Material;
			r.material.color = Color.yellow;
		}else if(ParentIsComplete() || isRoot){
			r.material.color = Color.white;
		}
		else{
			r.material.color = Color.gray;
		}

        //Debug.Log(levelInfo.isComplete);
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
        else if (ParentIsComplete())
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
            Debug.Log(ParentIsComplete());
        }
        // this object was clicked - do something
        Debug.Log("Level button clicked");
    }

	bool ParentIsComplete(){
		foreach(LevelMarker Parent in Parents){
			if(Parent.isComplete){
				return true;
			}
		}

		return false;
	}
}
