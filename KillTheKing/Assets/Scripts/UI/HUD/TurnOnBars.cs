using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using RAIN.Core;

public class TurnOnBars : MonoBehaviour
{
    public GameObject paranoiaHUD;
    public GameObject greedHUD;
    public GameObject fearHUD;
    public GameObject healthHUD;

    private Scrollbar paranoiaBar;
    private Scrollbar greedBar;
    private Scrollbar fearBar;
    private Scrollbar healthBar;

    private Image paranoiaEmoji;
    private Image greedEmoji;
    private Image fearEmoji;

    private AIRig kingAI;

	// Use this for initialization
	void Start ()
    {
        paranoiaBar = paranoiaHUD.GetComponentInChildren<Scrollbar>();
        greedBar = greedHUD.GetComponentInChildren<Scrollbar>();
        fearBar = fearHUD.GetComponentInChildren<Scrollbar>();
        healthBar = healthHUD.GetComponentInChildren<Scrollbar>();

        paranoiaEmoji = paranoiaHUD.transform.FindChild("Emoji").gameObject.GetComponent<Image>();
        greedEmoji = greedHUD.transform.FindChild("Emoji").gameObject.GetComponent<Image>();
        fearEmoji = fearHUD.transform.FindChild("Emoji").gameObject.GetComponent<Image>();

        // Set up the initial values of the bars
        kingAI = GameObject.FindGameObjectWithTag("King").GetComponentInChildren<AIRig>();

        if (GameObject.FindGameObjectWithTag("King") == null)
        {
            Debug.Log("No king found");
        }
        if (kingAI == null)
        {
            Debug.Log("No AI found");
        }
        int initFear = kingAI.AI.WorkingMemory.GetItem<int>("Fear");
        Debug.Log("Initial fear is " + initFear);
        InitFear(kingAI.AI.WorkingMemory.GetItem<int>("Fear"));
        InitParanoia(kingAI.AI.WorkingMemory.GetItem<System.Int32>("Paranoia"));
        InitGreed(kingAI.AI.WorkingMemory.GetItem<System.Int32>("Greed"));
    }

	// Update is called once per frame
	void Update ()
    {
        ChangeBar(healthBar, kingAI.AI.WorkingMemory.GetItem<int>("Health"));
	}

 
    private void ChangeBar(Scrollbar toChange, float value)
    {
        toChange.size = value / 100;
    }

    public void ChangeFear(int value)
    {
        ChangeBar(fearBar, value);

        fearEmoji.GetComponent<FlashColor>().Flash();
    }

    public void InitFear(int value)
    {
        ChangeBar(fearBar, value);
    }

    public void ChangeParanoia(int value)
    {
        ChangeBar(paranoiaBar, value);

        paranoiaEmoji.GetComponent<FlashColor>().Flash();
    }

    public void InitParanoia(int value)
    {
        ChangeBar(paranoiaBar, value);
    }

    public void ChangeGreed(int value)
    {
        ChangeBar(greedBar, value);

        greedEmoji.GetComponent<FlashColor>().Flash();
    }

    public void InitGreed(int value)
    {
        ChangeBar(greedBar, value);
    }

    private void FlashOnChange(Image toFlash, Color start, Color end, float length)
    {
        for (float i = 0.0f; i < 1.0; i += Time.deltaTime * (1 / length))
        {
            toFlash.color = Color.Lerp(start, end, i);
        }
        toFlash.color = end;

    } //end Fade
}
