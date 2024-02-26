using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public Vector3 spawnPosition; 
    public float spawnRate = 1f; 
    public float spawnDelay = 1f; 
    public float objectLifetime = 5f; 

    private float spawnTimer;
    private float lastSpawnTime;

    void Update()
    {
       
        spawnTimer += Time.deltaTime;

      
        if (Time.time - lastSpawnTime > spawnDelay && spawnTimer >= 1f / spawnRate)
        {
            SpawnObject();
            spawnTimer = 0f;
            lastSpawnTime = Time.time;
        }
    }

    void SpawnObject()
    {
       
        GameObject spawnedObject = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);

        
        Destroy(spawnedObject, objectLifetime);
    }
}