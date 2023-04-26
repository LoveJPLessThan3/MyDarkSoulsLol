using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
//Entrypoint

public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
{
    //поле
    private Game _game;
    public LoadingCurtain Curtain;

    //Точка входа в игру.
    private void Awake()
    {
        // Создаем игру. Передаем this, ибо здесь реализуется интерфейс ICoroutineRunner
        _game = new Game(this, Curtain);
        //точка с которой начинает переход по состояниям
        _game._stateMachine.EnterState<BootstrapState>();
        DontDestroyOnLoad(this);
    }
}
