using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class GameStateMachine
{
    private Dictionary<Type, IExitableState> _states;
    //активный стэйт у стэйт машины
    private IExitableState _activeState;

    public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain curtain)
    {
        //используем словарь, ибо это однозначное определение типа и самого инстанса стэйта (создание стэйта)
        // Ключ - тип, значение istate. Получается подается состояние и говорится входим мы или выходим.
        //после создания стэйта(рукотворно) здесь добавляется стэйт.
        _states = new Dictionary<Type, IExitableState>()
        {
            [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader),
            [typeof(LoadLevelScene)] = new LoadLevelScene(this, sceneLoader, curtain, AllServices.Container.Single<IGameFactory>()),
            [typeof(GameLoopState)] = new GameLoopState(this)
            
        };
    }

    //Дженериковый параметр с типом стэйтов, который мы хотим перевести. Не хочу передавать никаких парамтеров в функцию, а чтобы фурычила по типу
    //Ограничиваем по типу. Будем юзать классы отнаследованные от IState
    public void EnterState<TState>() where TState : class, IState
    {
        IState state = ChangeState<TState>();
        state.Enter();
    }

    //полезная нагрузка TPayLoad
    //Так как в IState нету метода, который бы принимал аргумент. Создаем с разной сигнатурой метод для переопределения
    public void EnterState<TState, TPayLoad>(TPayLoad payLoad) where TState : class, IPayLoadedState<TPayLoad>
    {
        
        IPayLoadedState<TPayLoad> state = ChangeState<TState>();

        //отправляемся в стэйт
        state.Enter(payLoad);
    }
    private TState ChangeState<TState>() where TState : class, IExitableState
    {
        //чтобы узнать активный стейт
        //активный стэйт в начале игры является null, поэтому это валидная проверка на null
        _activeState?.Exit();
        TState state = GetState<TState>();
        //сохраняем стэйт в который щас отправимся.
        _activeState = state;
        //отправляемся в стэйт
        return state;
    }
    private TState GetState<TState>() where TState : class, IExitableState
    {
        //Выбираем стэйт по типу 
        //мы действительно знаем всегда из интерфейса  вызова, какой именно тип мы передадим (в TState). Метод будет по факту в момент вызова, будет всегда знать, какой там тип
        //
        return _states[typeof(TState)] as TState;
    }
}
