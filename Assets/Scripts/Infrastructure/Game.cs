using JetBrains.Annotations;
using UnityEngine;

public class Game
{
    //пока временно тусуется эта регистрация на сервис
    public static IInputSevice InputService;  //чтобы не пришлось создавать экземпляр класс game, а обращаться на прямую
    private readonly LoadingCurtain _curtain;

    //инициализируем стэйт машину в конструкторе
    // публичная ибо будем к ней обращаться
    public GameStateMachine _stateMachine;

    public Game(ICoroutineRunner coroutineRunner, LoadingCurtain curtain)
    {
        //Необходимо передать в конструктор монобеху от GameBootstrapper, ибо в BootstrapState инициализируется InitialScene
        this._curtain = curtain;

        _stateMachine = new GameStateMachine(new SceneLoader(coroutineRunner),_curtain);
    }

  
}