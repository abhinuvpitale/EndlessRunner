﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour {
    // The original cubes that existed in far away and close scenes.
    public Transform prefab;

    public int numberOfObjects;             // How many cubes to create
    public Vector3 startPosition;           // Start position of the cube
    public float recycleOffset;             // How far behind the runner should the view be recycled.
    public Vector3 minSize, maxSize;        // Rescaling size for the skyline
    public Vector3 minGap, maxGap;          // Gap for the platforms
    public float minY, maxY;


    private Vector3 nextPosition;           // next position to spawn
    private Queue<Transform> objectQueue;   // Queue 



    // Use this for initialization
    void Start()
    {
        objectQueue = new Queue<Transform>(numberOfObjects);
        for (int i = 0; i < numberOfObjects; i++)
        {
            objectQueue.Enqueue((Transform)Instantiate(prefab));
        }

        nextPosition = startPosition;
        for (int i = 0; i < numberOfObjects; i++)
        {
            Recycle();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (objectQueue.Peek().localPosition.x + recycleOffset < Runner.distanceTraveled)
            Recycle();

    }
    private void Recycle()
    {
        Vector3 scale = new Vector3(
            Random.Range(minSize.x, maxSize.x),
            Random.Range(minSize.y, maxSize.y),
            Random.Range(minSize.z, maxSize.z));

        Vector3 position = nextPosition;
        // Not super sure on why it is scaled by half.
        position.x += scale.x * 0.5f;
        position.y += scale.y * 0.5f;
        Transform o = objectQueue.Dequeue();
        o.localScale = scale;
        o.localPosition = position;
        objectQueue.Enqueue(o);

        // Update for Platform!!!
        nextPosition += new Vector3(
            Random.Range(minGap.x, maxGap.x) + scale.x,
            Random.Range(minGap.y, maxGap.y),
            Random.Range(minGap.z, maxGap.z));

        if (nextPosition.y < minY)
        {
            nextPosition.y = minY + maxGap.y;
        }
        else if (nextPosition.y > maxY)
        {
            nextPosition.y = maxY - maxGap.y;
        }

        
    }
}
