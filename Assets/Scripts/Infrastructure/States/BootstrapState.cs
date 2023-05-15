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
        RegisterServices();
    }

    //класс для регистрации сервисов
    public void Enter()
    {
     //   RegisterServices();
        //загружаем сцену Initial
        //после того, как мы загрузим инишиалсцену, мы хотим отправиться в новый стэйт, которого пока что нет
        _sceneLoader.Load(name : InitialScene, onLoaded : EnterLoadLevel);

    }
    // переход на некст стэйт.
    private void EnterLoadLevel()
    {
        // IPayLoadedState<string>. поэтому еще пишем string
        _gameStateMachine.EnterState<LoadProgressState>(); 
    }
    //резолвер зависимостей
    private void RegisterServices()
    {
        //Пока сельская штука через статики
        //после регистрации сервиса 
        //InputService();
        AllServices.Container.RegisterSingle<IInputService>(InputService());
        AllServices.Container.RegisterSingle<IAssetProvider>(new AssetProvider());
        AllServices.Container.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
        //Регистрируем интрефейс...() - выдать реализацию, если кто-то попросит выдать реализацию
        //Спрашиваем у контейнера, дать реализацию инцерфейса
        // Single - когда запрашиваешь у интерфейса отдать реализацию, он возвращал всегда одну и туже реализацию
        //получается контейнер возвращает экземпляр AllServices, а у него вызываем метод регистер
        AllServices.Container.RegisterSingle<IGameFactory>(new GameFactory(AllServices.Container.Single<IAssetProvider>()));
        AllServices.Container.RegisterSingle<ISaveLoadService>
            (new SaveLoadService(AllServices.Container.Single<IPersistentProgressService>(), AllServices.Container.Single<IGameFactory>()));
    }

    public void Exit()
    {
      
    }

    private IInputService InputService()
    {
        //если в эдиторе, то регистрируется стэндэлон подсервис.
        if (Application.isEditor)
        {
           return new StandaloneInputService();
        }
        else
        {
           return new MobileInputService();
        }
    }
}
