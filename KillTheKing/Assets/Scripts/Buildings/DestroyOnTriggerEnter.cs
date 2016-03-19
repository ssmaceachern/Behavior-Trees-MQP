using UnityEngine;
using System.Collections;

public class DestroyOnTriggerEnter : MonoBehaviour
{
    public GameObject triggerer;        // The object we want to check for in OnTriggerEnter
    public GameObject toBeDestroyed;    // The object to be destroyed when triggered. If no object specified, we will destroy ourselves

    void OnTriggerEnter(Collider other)
    {
        // Check that the object that entered this is what we are looking for
        if (other.gameObject == triggerer)
        {
            // Check if an object is specified to be destroyed. Else, destroy ourselves
            if (toBeDestroyed == null)
            {
                Destroy(this.gameObject);
            }
            else
            {
                Destroy(toBeDestroyed);
            }
        }
    }
}
