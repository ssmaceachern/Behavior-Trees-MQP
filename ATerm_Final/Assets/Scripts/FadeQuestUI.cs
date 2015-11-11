using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class FadeQuestUI : MonoBehaviour
{

    public float Duration = 5f;
    public float FadeOutDuration = 2f;
    public CanvasGroup QuestUI;

    private float TimeLeft;
    private float Fade;

    // Use this for initialization
    void Start()
    {
        TimeLeft = Duration;
        Fade = FadeOutDuration;
    }

    // Update is called once per frame
    void Update()
    {
        if (TimeLeft < 0)
        {
            FadeOutDuration -= Time.deltaTime;
            QuestUI.alpha = FadeOutDuration;
        }
        else
        {
            TimeLeft -= Time.deltaTime;
        }

    }
}
