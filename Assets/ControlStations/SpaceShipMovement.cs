using System;
using UnityEngine;

public class SpaceShipMovement : ControlStation
{
    [SerializeField] float minHeight, maxHeight;

    [SerializeField] float speedUp = 10f;
    [SerializeField] float speedDown = 10f;

    [SerializeField] float currentHeight;

    bool goingDown = true;

    public static SpaceShipMovement instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Update()
    {
        if (goingDown)
        {
            if (currentHeight > minHeight) currentHeight -= speedDown * Time.deltaTime;
            else
            {
                print("Game over");
                currentHeight = minHeight;
                GameOver();
            }
        }
        else
        {
            if (currentHeight < maxHeight) currentHeight += speedUp * Time.deltaTime;
            else currentHeight = maxHeight;
        }
    }

    public void GoUp()
    {
        goingDown = false;
    }

    public void GoDown()
    {
        goingDown = true;
    }

    public int GetCurrentHeight()
    {
        return (int)currentHeight;
    }

    internal float GetCurrentHeightPercentage()
    {
        return (currentHeight - minHeight) / maxHeight;
    }

    private void GameOver()
    {
        GameManager.instance.GameOver();
    }
}
