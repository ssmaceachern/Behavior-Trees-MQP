using UnityEngine;
using System.Collections;

public class TestCommand : MonoBehaviour {

    void OnSelect(string command)
    {
        Debug.Log("A Menu Command Received: " + command);
    }
}

