using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void StartGame()
    {
        LoadScene("Game");
    }

    public void GoToMenu()
    {
        LoadScene("Menu");
    }

    public void GoToUpgradeMenu()
    {
        LoadScene("UpgradeMenu");
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
