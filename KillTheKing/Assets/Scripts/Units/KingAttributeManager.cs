using UnityEngine;
using System.Collections;
using RAIN.Core;

// Handle changing the king's attributes and checking for thresholds when doing so
public class KingAttributeManager : MonoBehaviour
{
    private AIRig ai;
    private DisplayThoughts kingThoughts;

    // Use this for initialization
    void Start()
    {
        ai = GetComponentInChildren<AIRig>();
        kingThoughts = GetComponentInChildren<DisplayThoughts>();
    }

    public void ChangeAttribute(string att, int amount)
    {
        // Store the old value of the attribute
        int oldValue = ai.AI.WorkingMemory.GetItem<int>(att);

        int newValue = oldValue + amount;

        // Make sure no attribute drops below 0
        if (newValue <= 0)
            newValue = 0;

        // Set the new value
        ai.AI.WorkingMemory.SetItem<int>(att, newValue);

        // Check for thresholds
        int result = 0;
        switch(att)
        {
            case "Paranoia":
                result = CheckThreshold(oldValue, newValue, 80);
                if (result >= 1)
                    kingThoughts.TurnOnThreshold("Paranoia", true);
                else if (result <= -1)
                    kingThoughts.TurnOnThreshold("Paranoia", false);
                break;
            case "Greed":
                result = CheckThreshold(oldValue, newValue, 0);
           //     Debug.Log(result);
                if (result >= 1)
                {
                //    kingThoughts.TurnOnThreshold("Greed", true);
                    break;
                }
                else if (result <= -1)
                {
                //    kingThoughts.TurnOnThreshold("Greed", false);
                    break;
                }
                result = CheckThreshold(oldValue, newValue, 100);
                Debug.Log(result);
                if (result >= 1)
                {
                //    kingThoughts.TurnOnThreshold("Greed", true);
                    break;
                }
                else if (result <= -1)
                {
                //    kingThoughts.TurnOnThreshold("Greed", false);
                    break;
                }
                break;
            case "Fear":
                result = CheckThreshold(oldValue, newValue, 10);
                if (result >= 1)
                {
                    kingThoughts.TurnOnThreshold("Fear", true);
                    break;
                }
                else if (result <= -1)
                {
                    kingThoughts.TurnOnThreshold("Fear", false);
                    break;
                }
                result = CheckThreshold(oldValue, newValue, 60);
                if (result >= 1)
                {
                    kingThoughts.TurnOnThreshold("Fear", true);
                    break;
                }
                else if (result <= -1)
                {
                    kingThoughts.TurnOnThreshold("Fear", false);
                    break;
                }
				result = CheckThreshold (oldValue, newValue, 100);
				if (result >= 1)
				{;
					kingThoughts.TurnOnThreshold("Fear", true);
					break;
				}
				else if (result <= -1)
				{
					kingThoughts.TurnOnThreshold("Fear", false);
					break;
				}
                break;
            default:
                Debug.Log("Attribute not recognized");
                break;
        }
    }

    // Return true if a threshold was crossed between the old value of an attribute and the new one
    private int CheckThreshold(float oldVal, float newVal, float thresh)
    {
        if (oldVal < thresh && newVal >= thresh)
            return 1;
        else if (oldVal > thresh && newVal <= thresh)
            return -1;
        else
            return 0;
    }
}
