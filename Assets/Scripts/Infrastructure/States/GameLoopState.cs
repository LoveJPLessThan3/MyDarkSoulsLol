using System;
//СТэйт холдер, 
public class GameLoopState : IState
{
    private readonly GameStateMachine _gameStateMachine;

    public GameLoopState(GameStateMachine gameStateMachine)
    {
        this._gameStateMachine = gameStateMachine;
    }

    public void Enter()
    {
        
    }

    public void Exit()
    {
        
    }
}