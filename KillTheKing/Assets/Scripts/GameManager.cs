using UnityEngine;
using System.Collections;

/// <summary>
/// Public enumeration for all of the game states
/// </summary>
public enum GameState
{
    Null,
    Menu,
    Loading,
    Play,
    Freeze,
    Pause,
    Win,
    Lose
}

public delegate void OnStateChangeHandler();

public class GameManager : MonoBehaviour {

	public static int ManagerID = 0;
	public int GameManagerID;

    protected GameManager() { }
    private static GameManager _instance = null;

    public GameState gameState { get; private set; }
    public GameState previousGameState { get; private set; }


    /// <summary>
    /// OnStateChange is a delegate function that allows other managers to add on
    /// their own functions for how to react to when a state is changing.
    /// 
    /// One example of how to use this is given below.
    /// 
    /// CONSTRUCTOR/AWAKE/START
    /// GM = GameManager.instance;
    /// GM.OnStateChange += HandleOnStateChange;
    /// 
    /// METHOD BODY
    /// void HandleOnStateChange() { //Code to execute }
    /// 
    /// </summary>
    public event OnStateChangeHandler OnStateChange;

    static public bool isActive
    {
        get
        {
            return _instance != null;
        }
    }

	void Awake(){
		Debug.Log("New GameManager Created");
		ManagerID++;
		GameManagerID = ManagerID;

		_instance = GameManager.instance;
		//_GameManagerObject = this.gameObject;
	}

    static public GameManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Object.FindObjectOfType(typeof(GameManager)) as GameManager;

                if (_instance == null)
                {
                    GameObject go = new GameObject("GameManager");
                    DontDestroyOnLoad(go);
                    _instance = go.AddComponent<GameManager>();
					//_instance._GameManagerObject = go;
                    _instance.previousGameState = GameState.Null;
                }
            }
            return _instance;
        }
    }

    public void SetGameState(GameState gameState)
    {
        previousGameState = this.gameState;

        this.gameState = gameState;
        if (OnStateChange != null)
        {
            OnStateChange();
        }
    }

	T CopyComponent<T>(T original, GameObject destination) where T : Component
	{
		System.Type type = original.GetType();
		Component copy = destination.AddComponent(type);
		System.Reflection.FieldInfo[] fields = type.GetFields();
		foreach (System.Reflection.FieldInfo field in fields)
		{
			field.SetValue(copy, field.GetValue(original));
		}
		return copy as T;
	}

    void OnLevelWasLoaded(int level)
    {
        GameManager[] GameManagerInstances = FindObjectsOfType<GameManager>();
		if(GameManagerInstances.Length == 1)
			return;

		GameManager original = GameManager.instance;
		foreach(GameManager g in GameManagerInstances){
			if(g.GameManagerID > original.GameManagerID){
				//Destroy(g.gameObject);
				Debug.Log("Duplicate GameManager Found");
			}
				
		}
    }
}
