using System.Collections.Generic;
using UnityEngine;

public class ShowKeys : MonoBehaviour
{
    [SerializeField] PressKeysInOrderTask keysTask;

    [SerializeField] Transform keyShowParent;
    [SerializeField] KeyShowObject keyShowPrefab;
    [SerializeField] List<KeyShowObject> keyShowObjects;


    void Awake()
    {
        keysTask.onInputRegistered += UpdateVisuals;
        keysTask.onStartTask += UpdateVisuals;
    }

    void UpdateVisuals()
    {
        SpawnKeyShowObjects();

        UpdateKeys();
    }

    private void UpdateKeys()
    {
        List<KeyCode> keys = keysTask.GetKeyCodes;


        for (int i = 0; i < keyShowObjects.Count; i++)
        {
            if (i >= keys.Count) keyShowObjects[i].gameObject.SetActive(false);
            else
            {
                keyShowObjects[i].SetKey(keys[i]);
                if (keysTask.GetCurrentKeyIndex > i) keyShowObjects[i].ShowCorrect();
            }
        }
    }

    private void SpawnKeyShowObjects()
    {
        if (keyShowObjects.Count >= keysTask.GetKeyCodes.Count) return;

        for (int i = keyShowObjects.Count; i < keysTask.GetKeyCodes.Count; i++)
        {
            keyShowObjects.Add(Instantiate(keyShowPrefab, keyShowParent));
        }
    }
}
