using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject[] objectsToSpawn; // Array of prefabs to spawn
    public Transform[] spawnPoints; // Spawn points on the tray
    public float spawnInterval = 2f; // Time between spawns

    private float timer;
    private void Update()
    {
        if (PS5.Core.GameManager.Instance.gameStates == GameStates.gaming)
        {
            timer += Time.deltaTime;

            if (timer >= spawnInterval)
            {
                SpawnObject();
                timer = 0f;
            }
        }
    }

    private void SpawnObject()
    {
        // Select a random object and spawn point
        GameObject objectToSpawn = objectsToSpawn[Random.Range(0, objectsToSpawn.Length)];
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // Instantiate the object at the spawn point
        Instantiate(objectToSpawn, spawnPoint.position, Quaternion.identity, spawnPoint);
    }
}
