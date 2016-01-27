using UnityEngine;
using System.Collections;

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
                }
            }
            return _instance;
        }
    }

    public void SetGameState(GameState gameState)
    {
        this.gameState = gameState;
        if (OnStateChange != null)
        {
            OnStateChange();
        }
    }
}
