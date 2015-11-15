using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Flocking : MonoBehaviour
{
    internal FlockingController controller;

    IEnumerator Start()
    {
        
        while (true)
        {
            //Debug.Log(controller.IsActive);

            if (controller)
            {
                //Debug.Log("Here");

                if (controller.IsActive)
                {
                    
                    controller.AddBoid(this);
                }

                //Debug.Log("Here");

                GetComponent<Rigidbody>().velocity += steer() * Time.deltaTime;

                // enforce minimum and maximum speeds for the boids
                float speed = GetComponent<Rigidbody>().velocity.magnitude;
                if (speed > controller.maxVelocity)
                {
                    GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized * controller.maxVelocity;
                }
                else if (speed < controller.minVelocity)
                {
                    GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized * controller.minVelocity;
                }
            }
            else
            {
                controller = (FlockingController)FindObjectOfType(typeof(FlockingController));
                Debug.Log(controller.IsActive);
            }
            float waitTime = Random.Range(0.3f, 0.5f);
            yield return new WaitForSeconds(waitTime);
        }
    }

    public void SetController(FlockingController fc)
    {
        this.controller = fc;
    }

    Vector3 steer()
    {
        Vector3 randomize = new Vector3((Random.value * 2) - 1, (Random.value * 2) - 1, (Random.value * 2) - 1);
        randomize.Normalize();
        randomize *= controller.randomness;

        Vector3 center = controller.flockCenter - transform.localPosition;
        Vector3 velocity = controller.flockVelocity - GetComponent<Rigidbody>().velocity;
        Vector3 follow = controller.target.localPosition - transform.localPosition;

        return (center + velocity + follow * 5);
    }
}
