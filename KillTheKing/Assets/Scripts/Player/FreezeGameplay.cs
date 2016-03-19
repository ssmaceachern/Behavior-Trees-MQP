using UnityEngine;
using UnityStandardAssets.ImageEffects;
using System.Collections;
using RAIN.Core;
using RAIN.Minds;
using RAIN.BehaviorTrees;

// Turn off all behavior trees when the player presses spacebar
public class FreezeGameplay : MonoBehaviour 
{
    public bool startFrozen = true; // Whether the level should start frozen

	private bool frozen = false;	// Whether the game s currently frozen
	private GameObject freezeIMG;

	private Camera mainCamera;
	EdgeDetectionColor EdgeShaderComponent;
	public float ShaderTransitionSpeed = 0.01f;
	private float TransitionVelocity = 0.0f;

    void Start()
    {
		mainCamera = Camera.main;
		EdgeShaderComponent = mainCamera.GetComponent<EdgeDetectionColor>();

        freezeIMG = GameObject.Find("FreezeIMG");
        freezeIMG.SetActive(startFrozen);

        if (startFrozen)
        {
            frozen = true;

            AIRig[] ais = GetComponentsInChildren<AIRig>();
            for (int i = 0; i < ais.Length; i++)
            {
                ais[i].AI.IsActive = false;
                if (ais[i].AI.Body.GetComponent<Rigidbody>() != null)
                    ais[i].AI.Body.GetComponent<Rigidbody>().isKinematic = true;
            }

            if (EdgeShaderComponent != null){
				EdgeShaderComponent.enabled = true;
				EdgeShaderComponent.edgesOnly = Mathf.SmoothDamp(0.0f, 1.0f, ref TransitionVelocity, ShaderTransitionSpeed);
			}
        }
    }

	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown ("space") && !frozen)
		{
            Freeze();
		}
		else if (Input.GetKeyDown ("space") && frozen)
		{
            UnFreeze();
        }
	}

	public bool IsFrozen()
	{
		return frozen;
	}

    public void Freeze()
    {
        frozen = true;

        AIRig[] ais = GetComponentsInChildren<AIRig>();
        for (int i = 0; i < ais.Length; i++)
        {
            ais[i].AI.IsActive = false;
            if (ais[i].AI.Body.GetComponent<Rigidbody>() != null)
                ais[i].AI.Body.GetComponent<Rigidbody>().isKinematic = true;

        }

        if (EdgeShaderComponent != null){
			EdgeShaderComponent.enabled = true;
			EdgeShaderComponent.edgesOnly = Mathf.SmoothDamp(0.0f, 1.0f, ref TransitionVelocity, ShaderTransitionSpeed);
		}
    }

    public void UnFreeze()
    {
        frozen = false;
        AIRig[] ais = GetComponentsInChildren<AIRig>();
        for (int i = 0; i < ais.Length; i++)
        {
            ais[i].AI.IsActive = true;
            if (ais[i].AI.Body.GetComponent<Rigidbody>() != null)
                ais[i].AI.Body.GetComponent<Rigidbody>().isKinematic = false;


        }

        if (EdgeShaderComponent != null){
			EdgeShaderComponent.edgesOnly = Mathf.SmoothDamp(1.0f, 0.0f, ref TransitionVelocity, ShaderTransitionSpeed);
			EdgeShaderComponent.enabled = false;
		}
	}


}
