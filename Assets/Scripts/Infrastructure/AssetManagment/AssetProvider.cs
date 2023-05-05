using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetProvider : IAssetProvider
{
   
    public GameObject Instantiate(string path)
    {
        //получаем ссылку на префаб
        GameObject prefab = Resources.Load<GameObject>(path);
        //так как нету монобехи
        return Object.Instantiate(prefab);
    }
    public GameObject Instantiate(string path, Vector3 spawnPoint)
    {
        //получаем ссылку на префаб
        GameObject prefab = Resources.Load<GameObject>(path);
        //так как нету монобехи
        return Object.Instantiate(prefab, spawnPoint, Quaternion.identity);
    }

   
}
