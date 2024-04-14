using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Spawn : MonoBehaviour
{
    public GameObject[] objectsToSpawn; // Array of prefabs to spawn
    public Transform[] spawnPoints; // Spawn points on the tray
    public float spawnInterval = 2f; // Time between spawns

    private float timer;
    List<GameObject> objectSpawned = new List<GameObject>();

    private void Update()
    {
        if (GameManager.Instance.gameStates == GameStates.gaming)
        {
            timer += Time.deltaTime;
            
            if (timer >= spawnInterval)
            {
                objectSpawned.Add(SpawnObject());
                timer = 0;
            }
            
            foreach (var food in objectSpawned)
            {
                if (food == null)
                    return;
                
                food.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            }
        }
        else if (GameManager.Instance.gameStates == GameStates.pause)
        {
            foreach (var food in objectSpawned)
            {
                if (food == null)
                    return;
                
                food.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            }
        }
    }

    private GameObject SpawnObject()
    {
        // Select a random object and spawn point
        GameObject objectToSpawn = objectsToSpawn[Random.Range(0, objectsToSpawn.Length)];
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // Instantiate the object at the spawn point
        GameObject spawnedObject = Instantiate(objectToSpawn, spawnPoint.position, Quaternion.identity, spawnPoint);
        return spawnedObject;
    }
}
