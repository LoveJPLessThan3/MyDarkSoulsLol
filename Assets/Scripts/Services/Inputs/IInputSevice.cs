using UnityEngine;

public interface IInputService : IService
{
    //джойстик
    Vector2 Axis { get; }

    //баттон
    bool IsAttackButtonUp();
}
