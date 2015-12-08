using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class LevelCoordinator : MonoBehaviour {

    static LevelCoordinator _instance;

    //Dictionary<Scene Name, Scene Level Info>
    static Dictionary<string, LevelInfo> LevelRegistry;

    //Holds index of scene to be loaded
    private int LevelToBeLoaded
    {
        set
        {
            LevelToBeLoaded = value;
        }
    }

    static public bool isActive
    {
        get
        {
            return _instance != null;
        }
    }

    static public LevelCoordinator instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType(typeof(LevelCoordinator)) as LevelCoordinator;

                if(_instance == null)
                {
                    GameObject lc = new GameObject("Level Coordinator");
                    DontDestroyOnLoad(lc);
                    _instance = lc.AddComponent<LevelCoordinator>();
                }
            }

            return _instance;
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        LevelRegistry = new Dictionary<string, LevelInfo>();
    }

    void PopulateLevelRegistry()
    {
        
        string LevelRegistryPath = Application.dataPath + "/Scenes/MainLevels/";
        Debug.Log(LevelRegistryPath);

        string[] MainLevelNames = Directory.GetFiles(LevelRegistryPath, "*.unity");
        Debug.Log(MainLevelNames);

        foreach(string MainLevelName in MainLevelNames)
        {
            Debug.Log(MainLevelName);
            LevelRegistry.Add(MainLevelName, new LevelInfo(MainLevelName));
        }

        Debug.Log(LevelRegistry.Count);
    }

    void LoadLevel(int level)
    {
        PlayerPrefs.SetInt("LastLevel", level);
        Application.LoadLevel(level);
    }

    void LoadLevel(string level)
    {
        Application.LoadLevel(level);
    }

    // Use this for initialization
    void Start () {
        PopulateLevelRegistry();
        //Debug.Log(instance == null);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
