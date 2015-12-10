using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class LevelCoordinator : MonoBehaviour {

    static LevelCoordinator _instance;

    //Dictionary<Scene Name, Scene Level Info>
    static Dictionary<string, LevelInfo> LevelRegistry;

    //Holds index of scene to be loaded
    private string LevelToBeLoaded;

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
        LevelRegistry = new Dictionary<string, LevelInfo>();
        PopulateLevelRegistry();
    }

    void PopulateLevelRegistry()
    {        
        string LevelRegistryPath = Application.dataPath + "/Scenes/MainLevels/";
        string[] MainLevelNames = Directory.GetFiles(LevelRegistryPath, "*.unity");

        //Used for holding a reference to MainLevelName
        string tmp;

        foreach(string MainLevelName in MainLevelNames)
        {
            tmp = MainLevelName.Substring(MainLevelName.LastIndexOf("/") + 1);
            LevelRegistry.Add(tmp, new LevelInfo(tmp));
        }

    }

    public void LoadLevelScene(string levelToBeLoaded)
    {
        LevelToBeLoaded = levelToBeLoaded;
        Application.LoadLevel("LevelLoad");
    }

    public void LoadLevelToBeLoaded()
    {
        Application.LoadLevel(LevelToBeLoaded);
    }

    public Dictionary<string, LevelInfo> GetLevelRegistry()
    {
        return LevelRegistry;
    }

    public string GetLevelToBeLoaded()
    {
        return LevelToBeLoaded;
    }

    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(transform.gameObject);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
