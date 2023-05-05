using Mono.Cecil;
using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class LoadLevelScene : IPayLoadedState<string>
{
    private const string InitialPointTag = "InitialPoint";

    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly LoadingCurtain _curtain;
    private readonly IGameFactory _gameFactory;

    public LoadLevelScene(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain, IGameFactory gameFactory)
    {
        _stateMachine = stateMachine;
        _sceneLoader = sceneLoader;
        this._curtain = curtain;
        //_gameFactory = new GameFactory(new AssetProvider());
        _gameFactory = gameFactory;

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
        var initialPoint = GameObject.FindWithTag(InitialPointTag);
        GameObject hero = _gameFactory.CreateHero(initialPoint);
        _gameFactory.CreateHud();
        //после загрузки героев, просим камеру зафолоувить его
        CameraFollow(hero);
        //после загрузки переходим в другой стэйт
        _stateMachine.EnterState<GameLoopState>();
    }

    


    private void CameraFollow(GameObject hero) =>
        Camera.main.GetComponent<CameraFollow>().Follow(hero);

   

    public void Exit()
    {
        _curtain.Hide();
    }
}