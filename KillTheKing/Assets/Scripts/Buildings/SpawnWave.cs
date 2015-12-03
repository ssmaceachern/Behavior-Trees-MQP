using UnityEngine;
using System.Collections;
using RAIN.Core;

public class SpawnWave : MonoBehaviour 
{
	public float timeTilWave = 5f;		// The amount of time it will take to spawn a wave
	public float warningTimer = 1f;		// The amount of time the warning will enabled or disabled before a wave is spawned
	public bool spawnWave = false;		// Whether to spawn a wave

	private float currentTimeTilWave;	// The current time left to spawn a wave
	private float currentWarningTimer;	// The current timer for the warning
	private MeshRenderer warningMesh;	// The mesh renderer of our warning arrow
	private GameObject charPar;			// The charaters game object, so our waves will stop when the game is frozen

	// Use this for initialization
	void Start () 
	{
		warningMesh = GetComponentInChildren<MeshRenderer> ();
		warningMesh.enabled = false;

		currentTimeTilWave = timeTilWave;
		currentWarningTimer = 0.0f;

		charPar = GameObject.FindGameObjectWithTag ("Characters");
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (spawnWave == true)
		{
			if (currentTimeTilWave > 0.0f)
			{
				currentTimeTilWave -= Time.deltaTime;
				
				if (currentWarningTimer > 0.0f)
				{
					currentWarningTimer -= Time.deltaTime;
					return;
				}

				currentWarningTimer = warningTimer;
				warningMesh.enabled = !warningMesh.enabled;

				return;
			}
			
			warningMesh.enabled = false;
			
			GameObject newWave = (GameObject)Instantiate (Resources.Load ("Wave"));
			
			newWave.transform.position = transform.position;
			newWave.transform.parent = charPar.transform;

			currentTimeTilWave = timeTilWave;
			spawnWave = false;

			if (charPar.GetComponent<FreezeGameplay>().IsFrozen ())
			{
				newWave.GetComponentInChildren<AIRig>().AI.IsActive = false;
			}
		}
	}

	public void SpawnAWave()
	{
		spawnWave = true;
	}
}
