using UnityEngine;
using System.Collections;

public class WaypointTracker : MonoBehaviour
{
    private int[] waypointsDeleted = new int[7];

    void Start()
    {
        for (int i = 0; i < waypointsDeleted.Length; i++)
        {
            waypointsDeleted[i] = System.Int32.MaxValue;
        }
    }

    public void addWaypoint(int index)
    {
        for (int i = 0; i < waypointsDeleted.Length; i++)
        {
            if (waypointsDeleted[i] == System.Int32.MaxValue)
            {
                waypointsDeleted[i] = index;

                break;
            }
        }

        Debug.Log(index);
    }

    public int waypointsModifier(int index)
    {
        int modifier = 0;

        for (int i = 0; i < waypointsDeleted.Length; i++)
        {
            if (index > waypointsDeleted[i])
            {
                modifier++;
            }
        }

        Debug.Log(modifier);
        return modifier;
    }
}
