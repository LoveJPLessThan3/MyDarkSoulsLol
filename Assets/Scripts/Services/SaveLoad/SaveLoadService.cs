using UnityEngine;

public class SaveLoadService : ISaveLoadService
{
    private const string ProgressKey = "Progress";
    private readonly IPersistentProgressService _progressService;
    private readonly IGameFactory _gameFactory;

    public SaveLoadService(IPersistentProgressService progressService, IGameFactory gameFactory)
    {
        this._progressService = progressService;
        this._gameFactory = gameFactory;
    }
    public ProgressPlayer LoadProgress()
    {
        return PlayerPrefs.GetString(ProgressKey)?.
            ToDeserialized<ProgressPlayer>();
    }

    public void SaveProgress()
    {
        foreach (ISavedProgress progressWriter in _gameFactory.ProgressWriters)
        {
            progressWriter.UpdateProgress(_progressService.Progress);
        }
        PlayerPrefs.GetString(ProgressKey, _progressService.Progress.ToJson());
    }
}

