using UnityEngine;

[CreateAssetMenu()]
public class Item : ScriptableObject
{
    public int id;

    public Sprite sprite;

    public GameObject itemPrefab;
}