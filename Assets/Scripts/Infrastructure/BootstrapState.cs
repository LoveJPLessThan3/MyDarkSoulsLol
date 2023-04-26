using System;
using UnityEngine;

public class BootstrapState : IState
{
    private const string InitialScene = "Initial";
    private readonly GameStateMachine _gameStateMachine;
    private readonly SceneLoader _sceneLoader;

    //получаем ссылку на стэйт машину. Пока хз зачем

    public BootstrapState(GameStateMachine gameStateMachine, SceneLoader _sceneLoader)
    {
        this._gameStateMachine = gameStateMachine;
        this._sceneLoader = _sceneLoader;
    }

    //класс для регистрации сервисов
    public void Enter()
    {
        RegisterServices();
        //загружаем сцену Initial
        //после того, как мы загрузим инишиалсцену, мы хотим отправиться в новый стэйт, которого пока что нет
        _sceneLoader.Load(name : InitialScene, onLoaded : EnterLoadLevel);

    }
    // переход на некст стэйт.
    private void EnterLoadLevel()
    {
        // IPayLoadedState<string>. поэтому еще пишем string
        _gameStateMachine.EnterState<LoadLevelScene, string>("Main"); 
    }

    private void RegisterServices()
    {
        //Пока сельская штука через статики
        //после регистрации сервиса 
        RegisterInputService();
    }

    public void Exit()
    {
      
    }

    private void RegisterInputService()
    {
        //если в эдиторе, то регистрируется стэндэлон подсервис.
        if (Application.isEditor)
        {
            Game.InputService = new StandaloneInputService();
        }
        else
        {
            Game.InputService = new MobileInputService();
        }
    }
}
