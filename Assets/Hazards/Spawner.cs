using System.Collections.Generic;
using UnityEngine;

public class Spawner<T> where T : MonoBehaviour
{
    public List<T> allObjects = new List<T>();

    public void SpawnRandom()
    {
        if(allObjects.Count == 0)
        {
            Debug.Log("No objects to spawn");
            return;
        }

        T temp = GameObject.Instantiate(GetRandomObject(), GetRandomSpawnPos(), Quaternion.identity);
    }
    
    private Vector3 GetRandomSpawnPos()
    {
        Camera mainCamera = Camera.main;

        if (mainCamera == null)
        {
            Debug.LogError("Main camera not found!");
            return Vector3.zero; // Return a default position if the main camera is not found
        }

        float cameraHeight = 2f * mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        float spawnX = UnityEngine.Random.Range(-cameraWidth / 2f, cameraWidth / 2f);
        float spawnY = UnityEngine.Random.Range(-cameraHeight / 2f, cameraHeight / 2f);
        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0f);

        // Check for 2D colliders at the spawn position
        Collider2D[] colliders = Physics2D.OverlapPointAll(spawnPosition);

        // Check if any colliders were found
        if (colliders.Length > 0)
        {
            // If colliders are found, recursively call the method to find a new spawn position
            return GetRandomSpawnPos();
        }

        // If no colliders are found at the spawn position, return it
        return spawnPosition;
    }
    
    T GetRandomObject()
    {
        return allObjects[UnityEngine.Random.Range(0, allObjects.Count)];
    }
}
