using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Spawn : MonoBehaviour
{
    public GameObject[] objectsToSpawn; // Array of prefabs to spawn
    public Transform[] spawnPoints; // Spawn points on the tray

    [SerializeField, Range(1, 10)] private int _minObjects = 2;
    [SerializeField, Range(1, 10)] private int _maxObjects = 5;
    
    List<GameObject> objectSpawned = new List<GameObject>();
    
    [SerializeField] private MoveAlongPath _moveAlongPath;

    public event Action<int> OnSpawnObjects;

    private void OnEnable()
    {
        _moveAlongPath.OnSplineEndAction += SpawnObjects;
        GameManager.Instance.OnGameStart += SpawnObjects;
    }

    private void OnDisable()
    {
        _moveAlongPath.OnSplineEndAction -= SpawnObjects;
        GameManager.Instance.OnGameStart -= SpawnObjects;
    }

    private void SpawnObjects()
    {
        int objectSpawnAmount = Random.Range(_minObjects, _maxObjects);

        for (int i = 0; i < objectSpawnAmount; i++)
        {
            GameObject objectToSpawn = objectsToSpawn[Random.Range(0, objectsToSpawn.Length)];
            Transform spawnPoint = spawnPoints[i];
            GameObject spawnedObject = Instantiate(objectToSpawn, spawnPoint.position, objectToSpawn.transform.rotation, spawnPoint);
            spawnedObject.transform.Rotate(Vector3.up, Random.Range(0, 360));
        }

        OnSpawnObjects?.Invoke(objectSpawnAmount);
    }
}
