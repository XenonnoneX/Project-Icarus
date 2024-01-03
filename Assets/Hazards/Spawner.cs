using System.Collections.Generic;
using UnityEngine;

public class Spawner<T> where T : MonoBehaviour
{
    public List<T> allObjects = new List<T>();

    public void SpawnRandom(Transform parent)
    {
        if(allObjects.Count == 0)
        {
            Debug.Log("No objects to spawn");
            return;
        }

        T temp = GameObject.Instantiate(GetRandomObject(), Utils.GetRandomPosOnWalkableArea(), Quaternion.identity);
        temp.transform.parent = parent;
    }
    
    T GetRandomObject()
    {
        return allObjects[UnityEngine.Random.Range(0, allObjects.Count)];
    }
}
