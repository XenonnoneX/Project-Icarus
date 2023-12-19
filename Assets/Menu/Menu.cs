using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }

    public void GoToUpgradeMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("UpgradeMenu");
    }
}
