using System;
using UnityEngine;

public class SpaceShipMovement : MonoBehaviour
{
    [SerializeField] float speed = 10f;

    [SerializeField] float currentHeight;
    [SerializeField] int goalHeight;

    private void Update()
    {
        if((int)currentHeight != goalHeight)
        {
            currentHeight += Mathf.Sign(goalHeight - currentHeight) * speed * Time.deltaTime;
        }
    }

    public void SetGoalHeight(int height)
    {
        goalHeight = height;
    }

    internal int GetGoalHeight()
    {
        return goalHeight;
    }

    public int GetCurrentHeight()
    {
        return (int)currentHeight;
    }
}
