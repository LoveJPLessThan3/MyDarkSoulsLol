using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFactory : IGameFactory
{
   
    private readonly IAssetProvider _assets;


    public GameFactory(IAssetProvider assets)
    {
        this._assets = assets;
    }

    public GameObject CreateHero(GameObject initialPoint)
    {
        return _assets.Instantiate(AssetsPath.HeroPath, initialPoint.transform.position);
    }
    public void CreateHud()
    {
        _assets.Instantiate(AssetsPath.HudPath);
    }
   

}
