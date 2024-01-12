using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] TMP_Text papersReleasedText;
    [SerializeField] TMP_Text timeSurvivedText;

    [SerializeField] TMP_Text foundsGainedText;

    // Start is called before the first frame update
    void Start()
    {
        papersReleasedText.text = "Papers Released: " + PlayerPrefs.GetInt("ReleasedPapers").ToString();
        timeSurvivedText.text = "Time Survived: " + PlayerPrefs.GetFloat("TimeSurvived").ToString("F2");

        StartCoroutine(GainFounds());
    }

    IEnumerator GainFounds()
    {
        int foundsGained = 0;

        for (int i = 0; i < PlayerPrefs.GetInt("ReleasedPapers"); i++)
        {
            foundsGained += 20;

            foundsGainedText.text = "Founds Gained: " + foundsGained.ToString();

            yield return new WaitForSeconds(0.1f);
        }

        PlayerPrefs.SetInt("ReleasedPapers", 0);
        PlayerPrefs.SetInt("FoundsGained", foundsGained);
    }

    public void GoToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }
}
