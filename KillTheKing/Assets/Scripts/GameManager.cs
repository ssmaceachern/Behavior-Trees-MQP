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
}
