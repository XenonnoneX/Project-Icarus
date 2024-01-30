using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] TMP_Text papersReleasedText;
    [SerializeField] TMP_Text timeSurvivedText;

    [SerializeField] TMP_Text foundsGainedText;

    [SerializeField] float timeToGetFounds = 1f;

    // Start is called before the first frame update
    void Start()
    {
        papersReleasedText.text = "Papers Released: " + PlayerPrefs.GetInt("ReleasedPapers").ToString();
        timeSurvivedText.text = "Time Survived: " + PlayerPrefs.GetFloat("TimeSurvived").ToString("F0") + "s";

        StartCoroutine(GainFounds());
    }

    IEnumerator GainFounds()
    {
        int foundsGained = 0;
        int paperCount = PlayerPrefs.GetInt("ReleasedPapers");

        foundsGainedText.text = "Founds Gained: " + foundsGained.ToString();

        for (int i = 0; i < paperCount; i++)
        {
            int rand = Random.Range(-5, 6);

            foundsGained += 20 + rand;

            foundsGainedText.text = "Founds Gained: " + foundsGained.ToString();

            yield return new WaitForSeconds(timeToGetFounds / paperCount);
        }

        PlayerPrefs.SetInt("ReleasedPapers", 0);
        PlayerPrefs.SetInt("FoundsGained", foundsGained);
    }

    public void GoToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }
}
