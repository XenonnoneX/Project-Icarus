using TMPro;
using UnityEngine;

public class SpaceShipControlPanel : MonoBehaviour
{
    [SerializeField] SpaceShipMovement spaceShipMovement;
    [SerializeField] TMP_Text currentHeightText;
    [SerializeField] TMP_Text goalHeightText;

    void Update()
    {
        currentHeightText.text = spaceShipMovement.GetCurrentHeight().ToString() + "k km";
        goalHeightText.text = spaceShipMovement.GetGoalHeight().ToString() + "k km";
    }

    public void SetGoalHeight(int height)
    {
        spaceShipMovement.SetGoalHeight(height);
    }

    public void IncreaseGoalHeight(int value)
    {
        spaceShipMovement.SetGoalHeight(spaceShipMovement.GetGoalHeight() + value);
    }
}
