using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class PressKeysInOrderTask : Task
{
    [SerializeField] float numberOfKeys = 3f;
    List<KeyCode> keyCodes = new List<KeyCode>();
    public override void StartTask(Interactable interactable)
    {
        base.StartTask(interactable);

        if (keyCodes.Count == 0) GenerateKeyCodes();

        for (int i = 0; i < keyCodes.Count; i++)
        {
            print("Press " + keyCodes[i]);
        }
    }

    private void GenerateKeyCodes()
    {
        for (int i = 0; i < numberOfKeys; i++)
        {
            keyCodes.Add(GetRandomLetterKeyCode());
        }
    }

    protected override void UpdateLogic()
    {
        base.UpdateLogic();

        if (keyCodes.Count == 0) return;

        foreach (KeyCode code in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(code))
            {
                print(code);
            }
        }

        if (Input.GetKeyDown(keyCodes[0]))
        {
            keyCodes.RemoveAt(0);

            if (keyCodes.Count == 0)
            {
                interactable.CompleteTask();
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
}