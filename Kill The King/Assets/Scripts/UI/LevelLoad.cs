using UnityEngine;
using System.Collections;

public class LevelLoad : MonoBehaviour {

    public void LoadLevel(int level)
    {
        Application.LoadLevel(level);
    }
}
