using Mono.Cecil;
using System;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

public class LoadLevelState : IPayLoadedState<string>
{
    private const string InitialPointTag = "InitialPoint";

    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly LoadingCurtain _curtain;
    private readonly IGameFactory _gameFactory;
    private readonly IPersistentProgressService _persistentProgress;

    public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain, IGameFactory gameFactory, IPersistentProgressService persistentProgress)
    {
        _stateMachine = stateMachine;
        _sceneLoader = sceneLoader;
        this._curtain = curtain;
        //_gameFactory = new GameFactory(new AssetProvider());
        _gameFactory = gameFactory;
        this._persistentProgress = persistentProgress;
    }

    public void Enter(string sceneName) 
    {
        _curtain.Show();
        _gameFactory.CleanUp();
        //Загрузка сцены, а после что еще грузанется
        Debug.Log(2);
        _sceneLoader.Load(sceneName, OnLoaded);
    }

    private void OnLoaded()
    {
        InitGameWorld();
        InformProgressReaders();
        //после загрузки переходим в другой стэйт
        _stateMachine.EnterState<GameLoopState>();
    }

    private void InformProgressReaders()
    {
        foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
        {
            progressReader.LoadProgress(_persistentProgress.Progress);
        }
    }

    private void InitGameWorld()
    {
        GameObject hero = InitHero();
        InitHud(hero);
        //после загрузки героев, просим камеру зафолоувить его
        CameraFollow(hero);
    }

    private GameObject InitHero()
    {
        var initialPoint = GameObject.FindWithTag(InitialPointTag);
        GameObject hero = _gameFactory.CreateHero(initialPoint);
        return hero;
    }

    private void InitHud(GameObject hero)
    {
        GameObject hud = _gameFactory.CreateHud();
        hud.GetComponentInChildren<ActorUI>().Construct(hero.GetComponent<HeroHealth>());
    }

    private void CameraFollow(GameObject hero) =>
        Camera.main.GetComponent<CameraFollow>().Follow(hero);

   

    public void Exit()
    {
        _curtain.Hide();
    }
}