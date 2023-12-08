using System;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    public bool isBroken
    { get; set; }

    internal void Break()
    {
        isBroken = true;
    }

    internal void Fix()
    {
        isBroken = false;
    }
}