using System;

[Serializable]
public class WorldData
{
    public PositionOnLevel PositionOnLevel;
    private string _initialLevel;

    public WorldData(string initialLevel)
    {
        this._initialLevel = initialLevel;
        PositionOnLevel = new PositionOnLevel(_initialLevel);
    }
}
