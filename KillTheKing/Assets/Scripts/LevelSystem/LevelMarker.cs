using UnityEngine;
using System.Collections;

public class LevelMarker : MonoBehaviour {

    //public GameObject[] NextLevels;
    public LevelMarker[] Parents;
    public LevelInfo levelInfo;
    public string LevelName;

    public bool isTutorial;
    public bool isComplete;
    public bool isRoot;

    private bool hasUpdatedParents = false;

	// Use this for initialization
	void Start () {
		Renderer r = GetComponent<Renderer>();

		if(Parents == null)
        {
            isRoot = true;
        }

        if(LevelCoordinator.instance.GetLevelRegistry().TryGetValue(LevelName, out this.levelInfo))
        {
            levelInfo = LevelCoordinator.instance.GetLevelRegistry()[LevelName];
            isComplete = levelInfo.isComplete;

            Debug.Log("LevelMarker: " + LevelName + ": " + levelInfo.isComplete);
        }
        else
        {
            Debug.LogWarning("LevelMarker: Could not get level");
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

        UpdateConnections();
        //Debug.Log(levelInfo.isComplete);
    }

    //Update Color of LevelMarker to Reflect Status
    void UpdateSelf()
    {
        Renderer r = GetComponent<Renderer>();

        if (isComplete)
        {
            r.material = Resources.Load("Icon-Star") as Material;
            r.material.color = Color.yellow;
        }
        else if (ParentIsComplete() || isRoot)
        {
            r.material.color = Color.white;
        }
        else
        {
            r.material.color = Color.gray;
        }
    }

    void UpdateConnections()
    {
        //Destroys Previous Connections
        if(transform.childCount > 0)
        {
            foreach (Transform child in transform)
            {
				if(child.GetComponent<LineRenderer>() != null)
                GameObject.Destroy(child.gameObject);
            }
        }

        //Draw LevelMarker Connection
        for (int i = 0; i < Parents.Length; i++)
        {
            GameObject connection = new GameObject();
            connection.name = this.LevelName + " to " + Parents[i].LevelName;

            LineRenderer line = connection.AddComponent<LineRenderer>();

            line.material.shader = Shader.Find("Unlit/Color");

            line.SetPosition(0, transform.position);
            line.SetPosition(1, Parents[i].transform.position);

            //Debug.Log(LevelName + " Parent[" + i + "] : " + Parents[i].isComplete);

            if (Parents[i].isComplete)
            {
                //Debug.Log("Found Parent to be Complete");
                line.material.color = Color.yellow;
            }
            else
            {
                line.material.color = Color.gray;
            }

            connection.transform.parent = this.transform;
        }

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
            if (isTutorial)
            {
                LevelCoordinator.instance.LoadLevel(LevelName);
            }
            else
            {
                LevelCoordinator.instance.LoadLevelScene(LevelName);
            }
        }
        
        else
        {
            Debug.Log("Could not load level. Either level doesn't exist or specifications were not met");
        }
    }

    void Update()
    {
        //Ensures that this only updates once.
        if (ParentIsComplete() && hasUpdatedParents == false)
        {
            UpdateConnections();
            UpdateSelf();
            hasUpdatedParents = true;
        }
    }

	public bool ParentIsComplete(){
		foreach(LevelMarker Parent in Parents){
			if(Parent.isComplete){
				return true;
			}
		}

		return false;
	}
}
