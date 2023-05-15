using System.Collections.Generic;
using UnityEngine;

public interface IGameFactory : IService
{
    GameObject CreateHero(GameObject initialPoint);
    void CreateHud();
    void CleanUp();
    List<ISavedProgress> ProgressReaders { get; }
    List<ISavedProgressReader> ProgressWriters { get; }
}