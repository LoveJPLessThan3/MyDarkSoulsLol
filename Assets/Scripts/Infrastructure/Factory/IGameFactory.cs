using UnityEngine;

public interface IGameFactory : IService
{
    GameObject CreateHero(GameObject initialPoint);
    void CreateHud();
}