using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class FadeQuestUI : MonoBehaviour {

    public float Duration = 5f;
    public CanvasGroup QuestUI;

    private float TimeLeft;

	// Use this for initialization
	void Start () {
        TimeLeft = Duration;
	}
	
	// Update is called once per frame
	void Update () {
        TimeLeft -= Time.deltaTime;

        QuestUI.alpha = TimeLeft/10f;
	}
}
