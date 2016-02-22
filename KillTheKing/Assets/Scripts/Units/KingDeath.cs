using UnityEngine;
using System.Collections;

// Handles displaying a bloody death for the king.
public class KingDeath : MonoBehaviour
{
    public float timeTilLevelEnd = 5.0f;    // The time between the king's death and the end of the level.
    public int timeBetweenBlood = 3;        // To control how much blood is spilled. 

    private bool isDead;            // Whether the king has been killed or not.
    private float timeLeftTilEnd;   // The time left until the level actually ends. 
    private int loopsLeftTilBlood;  // The number of updates until we spawn another blood particle

	// Use this for initialization
	void Start ()
    {
        timeLeftTilEnd = timeTilLevelEnd;
        isDead = false;
        loopsLeftTilBlood = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if (isDead)
        {
            // Fix the camera on the king's location
            Vector3 kingPos = new Vector3(transform.position.x,
                                          Camera.main.transform.position.y,
                                          transform.position.z);
            Camera.main.transform.position = kingPos;

            // Count down to the end of the level.
            timeLeftTilEnd -= Time.deltaTime;

            if (timeLeftTilEnd <= 0.0f)
            {
                LevelCoordinator.instance.LoadLevel("WinScreen");
            }

            if (loopsLeftTilBlood > 0)
            {
                loopsLeftTilBlood -= 1;
                return;
            }

            // Spawn a bunch of blood
            GameObject particle = (GameObject)GameObject.Instantiate(Resources.Load("Blood"));
            particle.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            Rigidbody hisBod = particle.GetComponent<Rigidbody>();
            Vector3 nudgeForce = new Vector3();
            nudgeForce = Vector3.up;
            nudgeForce.x += (Random.value * 100 - 50);
            nudgeForce.y = 300;
            nudgeForce.z += (Random.value * 100 - 50);
            hisBod.AddForce(nudgeForce);

            loopsLeftTilBlood = timeBetweenBlood;
        }
	}
    
    public void MakeDead()
    {
        isDead = true;
    }

    public bool IsDead()
    {
        return isDead;
    }
}
