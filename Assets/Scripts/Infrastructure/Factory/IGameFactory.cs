using System;
using System.Collections.Generic;
using UnityEngine;

public interface IGameFactory : IService
{
    GameObject CreateHero(GameObject initialPoint);
    GameObject CreateHud();
    void CleanUp();
    List<ISavedProgress> ProgressReaders { get; }
    List<ISavedProgressReader> ProgressWriters { get; }
    public GameObject HeroGameObject { get; set; }
    public event Action HeroCreated;
}