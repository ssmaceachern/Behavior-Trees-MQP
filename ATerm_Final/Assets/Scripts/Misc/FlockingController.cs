﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FlockingController : MonoBehaviour
{
    public bool IsActive = false;
    public float minVelocity = 5;
    public float maxVelocity = 20;
    public float randomness = 1;
    public int flockSize = 20;
    public Flocking prefab;
    public Transform target;

    internal Vector3 flockCenter;
    internal Vector3 flockVelocity;

    List<Flocking> boids = new List<Flocking>();

    void Start()
    {
        this.IsActive = true;
        //boids2 = GameObject.FindGameObjectsWithTag("Guard") as Flocking[];
        for (int i = 0; i < flockSize; i++)
        {
            //Flocking boid = Instantiate(prefab, transform.position, transform.rotation) as Flocking;
            //boid.transform.parent = transform;
            //boid.transform.localPosition = new Vector3(
            //                Random.value * GetComponent<Collider>().bounds.size.x,
            //                1,
            //                Random.value * GetComponent<Collider>().bounds.size.z) - GetComponent<Collider>().bounds.extents;
            //guards[i].controller = this;

            //boids.Add(boid);
        }
    }

    void Update()
    {
        Vector3 center = Vector3.zero;
        Vector3 velocity = Vector3.zero;
        foreach (Flocking boid in boids)
        {
            center += boid.transform.localPosition;
            velocity += boid.GetComponent<Rigidbody>().velocity;
        }
        flockCenter = center / flockSize;
        flockVelocity = velocity / flockSize;
    }

    public void AddBoid(Flocking flockingEntity)
    {
        
        if (flockingEntity != null && !boids.Contains(flockingEntity))
        {
            Debug.Log("Added to Controller");
            flockingEntity.controller = this;
            boids.Add(flockingEntity);
        }
    }
}

