using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Spawn : MonoBehaviour
{
    public GameObject[] objectsToSpawn; // Array of prefabs to spawn
    public Transform[] spawnPoints; // Spawn points on the tray
    
    List<GameObject> objectSpawned = new List<GameObject>();
    
    [SerializeField] private MoveAlongPath _moveAlongPath;

    private WaitForSeconds _wsSpawnDelay;
    public event Action<int> OnSpawnObjects;

    private void Awake()
    {
        _wsSpawnDelay = new WaitForSeconds(0.2f);
    }

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
        StartCoroutine(SpawnObjectsCoroutine());
    }
    
    private IEnumerator SpawnObjectsCoroutine()
    {
        int objectSpawnAmount = Random.Range(GameManager.Instance.GameValues.MinObjects, GameManager.Instance.GameValues.MaxObjects);

        for (int i = 0; i < objectSpawnAmount; i++)
        {
            GameObject objectToSpawn = objectsToSpawn[Random.Range(0, objectsToSpawn.Length)];
            Transform spawnPoint = spawnPoints[i];
            GameObject spawnedObject = Instantiate(objectToSpawn, spawnPoint.position, objectToSpawn.transform.rotation, spawnPoint);
            spawnedObject.transform.Rotate(spawnedObject.transform.up, Random.Range(0, 360));
            yield return _wsSpawnDelay;
        }

        OnSpawnObjects?.Invoke(objectSpawnAmount);
    }
}
