using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ProgressPlayer : MonoBehaviour
{
    public State HeroState;
    public WorldData WorldData;
    //private string v;

    public ProgressPlayer(string initialLevel)
    {
        WorldData = new WorldData(initialLevel);
        HeroState = new State();
    }
}

[Serializable]
public class State
{
    public float CurrentHp = 50;
    public float MaxHp = 50;

    public float ResetHp() =>
        CurrentHp - MaxHp;
}
