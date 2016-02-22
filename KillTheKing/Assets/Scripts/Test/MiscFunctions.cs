using UnityEngine;
using System.Collections;

public class MiscFunctions : MonoBehaviour
{
    // Deactivate all of the children of a given game object, but not the game object itself
    public static void DeactivateChildren(GameObject parent)
    {
        foreach (Transform child in parent.transform)
        {
            DeactivateChildren(child.gameObject);
            child.gameObject.SetActive(false);
        }
    }

    // Activate all of the children of a given game object
    public static void ActivateChildren(GameObject parent)
    {
        foreach (Transform child in parent.transform)
        {
            ActivateChildren(child.gameObject);
            child.gameObject.SetActive(true);
        }
    }
}
