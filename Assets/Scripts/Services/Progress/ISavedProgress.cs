public interface ISavedProgressReader
{
    void LoadProgress(ProgressPlayer progress);

}
// потому что многие вещи только пишут, но не отображают, поэтому такое разделение
public interface ISavedProgress : ISavedProgressReader
{
    void UpdateProgress(ProgressPlayer progress);

}