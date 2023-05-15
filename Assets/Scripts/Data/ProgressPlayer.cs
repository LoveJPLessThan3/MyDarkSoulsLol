using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ProgressPlayer : MonoBehaviour
{
   
    public WorldData WorldData;
    //private string v;

    public ProgressPlayer(string initialLevel)
    {
        WorldData = new WorldData(initialLevel);
    }
}
