using Mono.Cecil;
using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class LoadLevelScene : IPayLoadedState<string>
{
    private const string InitialPointTag = "InitialPoint";
    private const string HeroPath = "Hero/hero";
    private const string HudPath = "MyPrefabs/Hud";
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly LoadingCurtain _curtain;

    public LoadLevelScene(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain)
    {
        _stateMachine = stateMachine;
        _sceneLoader = sceneLoader;
        this._curtain = curtain;
    }

    public void Enter(string sceneName) 
    {
        _curtain.Show();
        //Загрузка сцены, а после что еще грузанется
        Debug.Log(2);
        _sceneLoader.Load(sceneName, OnLoaded);
    }

    private void OnLoaded()
    {
        Debug.Log(3);
        var initialPoint = GameObject.FindWithTag(InitialPointTag);
        GameObject hero = Instantiate(HeroPath, initialPoint.transform.position);
        Instantiate(HudPath);
        //после загрузки героев, просим камеру зафолоувить его
        CameraFollow(hero);
        //после загрузки переходим в другой стэйт
        _stateMachine.EnterState<GameLoopState>();
    }
    private void CameraFollow(GameObject hero) =>
        Camera.main.GetComponent<CameraFollow>().Follow(hero);

    private static GameObject Instantiate(string path)
    {
        //получаем ссылку на префаб
        GameObject prefab = Resources.Load<GameObject>(path);
       // Debug.Log(prefab.name);
        //так как нету монобехи
        return Object.Instantiate(prefab);
    }
    private static GameObject Instantiate(string path, Vector3 spawnPoint)
    {
        //получаем ссылку на префаб
        GameObject prefab = Resources.Load<GameObject>(path);
       // Debug.Log(prefab.name);
        //так как нету монобехи
        return Object.Instantiate(prefab, spawnPoint, Quaternion.identity);
    }

    public void Exit()
    {
        _curtain.Hide();
    }
}