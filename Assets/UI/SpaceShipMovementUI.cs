using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpaceShipMovementUI : MonoBehaviour
{
    SpaceShipMovement spaceShipMovement;
    [SerializeField] Slider slider;

    private void Awake()
    {
        spaceShipMovement = FindObjectOfType<SpaceShipMovement>();
    }
    void Update()
    {
        slider.value = spaceShipMovement.GetCurrentHeightPercentage();
    }
}
