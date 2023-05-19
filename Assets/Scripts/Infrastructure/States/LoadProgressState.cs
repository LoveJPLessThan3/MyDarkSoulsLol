using System;

public class LoadProgressState : IState
{
    private GameStateMachine _gameStateMachine;
    private readonly ISaveLoadService _saveLoadService;
    private readonly IPersistentProgressService _progressService;

    public LoadProgressState(GameStateMachine gameStateMachine, IPersistentProgressService progressService, ISaveLoadService saveLoadService)
    {
        this._gameStateMachine = gameStateMachine;
        this._progressService = progressService;
        _saveLoadService = saveLoadService;
    }

    public void Enter()
    {
        //загрузить прогресс если он есть или проинициализировать нвоый
        LoadProgressOrInitNew();
        //переходим на уровень 
        _gameStateMachine.EnterState<LoadLevelState, string>(_progressService.Progress.WorldData.PositionOnLevel.Level);
    }

    private void LoadProgressOrInitNew() => 
        _progressService.Progress = _saveLoadService.LoadProgress() ?? NewProgress();

    private ProgressPlayer NewProgress()
    {

        ProgressPlayer progressPlayer = new ProgressPlayer(initialLevel: "Main");
        progressPlayer.HeroState.MaxHp = 50;
        progressPlayer.HeroState.ResetHp();
        return progressPlayer;
    }

    public void Exit()
    {
    }
}
