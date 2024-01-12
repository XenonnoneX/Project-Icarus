using System.Collections.Generic;
using UnityEngine;

public class PressKeysInOrderTask : Task
{
    [SerializeField] float numberOfKeys = 3f;
    List<KeyCode> keyCodes = new List<KeyCode>();
    public List<KeyCode> GetKeyCodes => keyCodes;

    int currentKeyIndex = 0;
    public int GetCurrentKeyIndex => currentKeyIndex;

    public delegate void OnInputRegistered();
    public event OnInputRegistered onInputRegistered;

    public override void StartTask(Interactable interactable)
    {
        if (keyCodes.Count == 0) GenerateKeyCodes();
        
        currentKeyIndex = 0;
        
        base.StartTask(interactable);
    }

    private void GenerateKeyCodes()
    {
        for (int i = 0; i < numberOfKeys; i++)
        {
            keyCodes.Add(GetRandomArrowKeyCode());
        }
    }

    protected override void UpdateLogic()
    {
        base.UpdateLogic();

        if (keyCodes.Count == 0) return;

        foreach (KeyCode code in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(code) && code != keyCodes[currentKeyIndex])
            {
                currentKeyIndex = 0;
                onInputRegistered?.Invoke();
            }
        }

        if (Input.GetKeyDown(keyCodes[currentKeyIndex]))
        {
            currentKeyIndex++;

            onInputRegistered?.Invoke();

            if (currentKeyIndex >= keyCodes.Count)
            {
                keyCodes.Clear();
                interactable.CompleteTask();
                EndTask();
                currentKeyIndex = 0;
            }
        }
    }

    KeyCode GetRandomLetterKeyCode()
    {
        KeyCode[] letterKeyCodes =
        {
            KeyCode.A, KeyCode.B, KeyCode.C, KeyCode.D, KeyCode.E, KeyCode.F, KeyCode.G,
            KeyCode.H, KeyCode.I, KeyCode.J, KeyCode.K, KeyCode.L, KeyCode.M, KeyCode.N,
            KeyCode.O, KeyCode.P, KeyCode.Q, KeyCode.R, KeyCode.S, KeyCode.T, KeyCode.U,
            KeyCode.V, KeyCode.W, KeyCode.X, KeyCode.Y, KeyCode.Z
        };

        return letterKeyCodes[Random.Range(0, letterKeyCodes.Length)];
    }

    KeyCode GetRandomArrowKeyCode()
    {
        KeyCode[] arrowKeyCodes =
        {
            KeyCode.DownArrow, KeyCode.UpArrow, KeyCode.LeftArrow, KeyCode.RightArrow
        };

        return arrowKeyCodes[Random.Range(0, arrowKeyCodes.Length)];
    }
}
