using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashVisuals : MonoBehaviour
{
    Dash dash;
    PlayerMovement player;
    [SerializeField] float waitForFadeDuration;

    [SerializeField] GameObject playerVisual;
    [SerializeField] int numberOfVisuals = 5;
    [SerializeField] Color visualColor;

    List<PlayerVisual> playerVisualsList = new List<PlayerVisual>();

    private void Awake()
    {
        player = FindObjectOfType<PlayerMovement>();
        dash = GetComponent<Dash>();

        dash.onAbilityUsed += ShowDashVisuals;
    }

    private void Start()
    {
        for (int i = 0; i < numberOfVisuals; i++)
        {
            PlayerVisual visual = new PlayerVisual(Instantiate(playerVisual, player.transform.position, Quaternion.identity), visualColor);
            playerVisualsList.Add(visual);

            visual.gameObject.SetActive(false);
        }
    }

    private void ShowDashVisuals()
    {
        StartCoroutine(DashRoutine());
    }



    private IEnumerator DashRoutine()
    {
        for (int i = 0; i < numberOfVisuals; i++)
        {
            playerVisualsList[i].gameObject.SetActive(true);


            playerVisualsList[i].gameObject.transform.position = player.transform.position;

            StartCoroutine(FadeVisual(playerVisualsList[i].gameObject, waitForFadeDuration + (float)i / numberOfVisuals * dash.dashDuration));
        }

        SetCorrectSpritesVisible();

        Vector3[] positions = new Vector3[numberOfVisuals];

        for (float i = 0; i < dash.dashDuration; i += Time.deltaTime)
        {
            int index = Mathf.FloorToInt(i / dash.dashDuration * numberOfVisuals);
            
            playerVisualsList[(int)index].gameObject.transform.position = player.transform.position;
            positions[index] = player.transform.position;

            
            yield return null;
        }
    }

    private void SetCorrectSpritesVisible()
    {
        Animator[] animators = player.GetComponentsInChildren<Animator>();

        string activeSpriteName = "";

        foreach (Animator animator in animators)
        {
            if (animator.gameObject.activeSelf)
            {
                activeSpriteName = animator.gameObject.name;
                break;
            }
        }

        foreach (PlayerVisual visual in playerVisualsList)
        {
            visual.SetVisualDirection(activeSpriteName);
        }
    }

    private IEnumerator FadeVisual(GameObject gameObject, float duration)
    {
        yield return new WaitForSeconds(duration);

        gameObject.SetActive(false);
    }
}
