using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class GameStateMachine
{
    private Dictionary<Type, IExitableState> _states;
    //�������� ����� � ����� ������
    private IExitableState _activeState;

    public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain curtain)
    {
        //���������� �������, ��� ��� ����������� ����������� ���� � ������ �������� ������ (�������� ������)
        // ���� - ���, �������� istate. ���������� �������� ��������� � ��������� ������ �� ��� �������.
        //����� �������� ������(����������) ����� ����������� �����.
        _states = new Dictionary<Type, IExitableState>()
        {
            [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader),
            [typeof(LoadLevelScene)] = new LoadLevelScene(this, sceneLoader, curtain, AllServices.Container.Single<IGameFactory>()),
            [typeof(GameLoopState)] = new GameLoopState(this)
            
        };
    }

    //������������ �������� � ����� �������, ������� �� ����� ���������. �� ���� ���������� ������� ���������� � �������, � ����� �������� �� ����
    //������������ �� ����. ����� ����� ������ ��������������� �� IState
    public void EnterState<TState>() where TState : class, IState
    {
        IState state = ChangeState<TState>();
        state.Enter();
    }

    //�������� �������� TPayLoad
    //��� ��� � IState ���� ������, ������� �� �������� ��������. ������� � ������ ���������� ����� ��� ���������������
    public void EnterState<TState, TPayLoad>(TPayLoad payLoad) where TState : class, IPayLoadedState<TPayLoad>
    {
        
        IPayLoadedState<TPayLoad> state = ChangeState<TState>();

        //������������ � �����
        state.Enter(payLoad);
    }
    private TState ChangeState<TState>() where TState : class, IExitableState
    {
        //����� ������ �������� �����
        //�������� ����� � ������ ���� �������� null, ������� ��� �������� �������� �� null
        _activeState?.Exit();
        TState state = GetState<TState>();
        //��������� ����� � ������� ��� ����������.
        _activeState = state;
        //������������ � �����
        return state;
    }
    private TState GetState<TState>() where TState : class, IExitableState
    {
        //�������� ����� �� ���� 
        //�� ������������� ����� ������ �� ����������  ������, ����� ������ ��� �� ��������� (� TState). ����� ����� �� ����� � ������ ������, ����� ������ �����, ����� ��� ���
        //
        return _states[typeof(TState)] as TState;
    }
}
