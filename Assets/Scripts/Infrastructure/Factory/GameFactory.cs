using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFactory : IGameFactory
{

    private readonly IAssetProvider _assets;
    //кто хочет записаться
    public List<ISavedProgress> ProgressReaders { get; } = new List<ISavedProgress>();
    //кто хочет еще и перезаписаться
    public List<ISavedProgressReader> ProgressWriters { get; } = new List<ISavedProgressReader>();
    public GameFactory(IAssetProvider assets)
    {
        this._assets = assets;
    }

    public GameObject CreateHero(GameObject initialPoint)
    {
        GameObject hero = _assets.Instantiate(AssetsPath.HeroPath, initialPoint.transform.position);
        RegisterProgressWatcher(hero);

        return hero;
    }


    public void CreateHud()
    {
        _assets.Instantiate(AssetsPath.HudPath);
    }
    //Иногда коллекции нужно чистить
    public void CleanUp()
    {
        ProgressReaders.Clear();
        ProgressWriters.Clear();
    }
    private void RegisterProgressWatcher(GameObject hero)
    {
        //перебираем объекты которые должны сереализовываться
        foreach (ISavedProgress progressReader in hero.GetComponentsInChildren<ISavedProgress>())
            Register(progressReader);
    }
    private void Register(ISavedProgress progressReader)
    {
        //Если можно апкастнуть. progressWriter - будет апкаст.
        if (progressReader is ISavedProgressReader progressWriter)
            ProgressWriters.Add(progressWriter);
        ProgressReaders.Add(progressReader);
    }

}
