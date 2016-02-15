using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class LevelCoordinator : MonoBehaviour {

    //Public array to hold the names of the levels to be put into the Level Registry
    public string[] MainLevelNames;

    //Instance for other scripts and managers to access
    static LevelCoordinator _instance;

    private GameManager GM;

    //Dictionary to hold pairs of Level Names and their Description Files
    static Dictionary<string, LevelInfo> LevelRegistry;

    //Holds index of scene to be loaded
    private string LevelToBeLoaded;
    public string currentLevel { get; private set; }

    /// <summary>
    /// Singleton Pattern
    /// </summary>
    static public LevelCoordinator instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(LevelCoordinator)) as LevelCoordinator;

                if (_instance == null)
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
        //Grab instance of the GameManager and set the state change handler to a 
        //Custom function.
        GM = GameManager.instance;
        GM.OnStateChange += HandleOnStateChange;

        //Populate the registry with level description files
        LevelRegistry = new Dictionary<string, LevelInfo>();
        PopulateLevelRegistry();

        GM.SetGameState(GameState.Menu);
    }

    /// <summary>
    /// Fills the LevelRegistry with pairs of Level Names and their Description Files
    /// </summary>
    void PopulateLevelRegistry()
    {        
        foreach(string MainLevelName in MainLevelNames)
        {
            //tmp = MainLevelName.Substring(MainLevelName.LastIndexOf("/") + 1);
            LevelRegistry.Add(MainLevelName, new LevelInfo(MainLevelName));
        }

    }

    /// <summary>
    /// Loads the Level Description scene while assigning the name of the 
    /// scene to be loaded to a private string.
    /// </summary>
    /// <param name="levelToBeLoaded">Name of the level to get a description of</param>
    public void LoadLevelScene(string levelToBeLoaded)
    {
        GM.SetGameState(GameState.Loading);

        LevelToBeLoaded = levelToBeLoaded;
        Application.LoadLevel("LevelLoad");
    }

	public void LoadLevel(string level)
	{
		GM.SetGameState(GameState.Loading);

		Application.LoadLevel(level);
	}

    /// <summary>
    /// Loads the scene when in the LevelLoad scene.
    /// </summary>
    public void LoadLevelToBeLoaded()
    {
        GM.SetGameState(GameState.Play);
        currentLevel = LevelToBeLoaded;
        Application.LoadLevel(LevelToBeLoaded);
    }

    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(transform.gameObject);
    }

    void OnLevelWasLoaded(int level)
    {
        Debug.Log(LevelToBeLoaded);
        if(GameObject.Find("LevelCoordinator") == true && GameObject.Find("LevelCoordinator") != transform.gameObject)
        {
            Destroy(GameObject.Find("LevelCoordinator"));
        }
    }

    /*---GETTERS---*/

    public Dictionary<string, LevelInfo> GetLevelRegistry()
    {
        return LevelRegistry;
    }

    public string GetLevelToBeLoaded()
    {
        return LevelToBeLoaded;
    }

    static public bool isActive
    {
        get
        {
            return _instance != null;
        }
    }

    public void HandleOnStateChange()
    {
        Debug.Log("Handling state change from " + GM.previousGameState + " to: " + GM.gameState);
    }
}
