using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject loadingPanel;
    [SerializeField] Image loadingBarFill;

    private void Start()
    {
        if(loadingPanel != null) loadingPanel.SetActive(false);
    }

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

        // StartCoroutine(LoadSceneAsync(sceneName));
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        loadingPanel.SetActive(true);

        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            loadingBarFill.fillAmount = asyncLoad.progress / 0.9f;

            if (asyncLoad.progress >= 0.9f)
            {
                asyncLoad.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}