using UnityEngine;

public static class Utils
{
    public static Vector3 V2_To_V3(Vector2 v)
    {
        return new Vector3(v.x, v.y, 0);
    }

    public static Vector3 GetPositionOutOfScreen(float margin)
    {
        Vector3 pos = Vector3.zero;

        float rand = Random.value;

        if (rand < 0.25f)
        {
            pos.x = -Camera.main.orthographicSize * Camera.main.aspect - margin;
            pos.y = Random.Range(-Camera.main.orthographicSize, Camera.main.orthographicSize);
        }
        else if (rand < 0.5f)
        {
            pos.x = Camera.main.orthographicSize * Camera.main.aspect + margin;
            pos.y = Random.Range(-Camera.main.orthographicSize, Camera.main.orthographicSize);
        }
        else if (rand < 0.75f)
        {
            pos.y = -Camera.main.orthographicSize - margin;
            pos.x = Random.Range(-Camera.main.orthographicSize * Camera.main.aspect, Camera.main.orthographicSize * Camera.main.aspect);
        }
        else
        {
            pos.y = Camera.main.orthographicSize + margin;
            pos.x = Random.Range(-Camera.main.orthographicSize * Camera.main.aspect, Camera.main.orthographicSize * Camera.main.aspect);
        }

        return pos;
    }

    public static Vector3 RotateVectorBy(float angle, Vector3 vector)
    {
        return Quaternion.Euler(0, 0, angle) * vector;
    }

    public static Vector3 GetRandomPosOnWalkableArea()
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
            return GetRandomPosOnWalkableArea();
        }

        // If no colliders are found at the spawn position, return it
        return spawnPosition;
    }

    internal static Vector3 GetWalkablePosNextTo(Vector3 position, float maxDist)
    {
        Vector3 spawnPosition = position + Random.insideUnitSphere * maxDist;
        spawnPosition.z = 0f;

        // Check for 2D colliders at the spawn position
        Collider2D[] colliders = Physics2D.OverlapPointAll(spawnPosition);

        // Check if any colliders were found
        if (colliders.Length > 0)
        {
            // If colliders are found, recursively call the method to find a new spawn position
            return GetWalkablePosNextTo(position, maxDist);
        }

        // If no colliders are found at the spawn position, return it
        return spawnPosition;
    }
}