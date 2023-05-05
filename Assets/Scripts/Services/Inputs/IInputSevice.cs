using UnityEngine;

public interface IInputSevice : IService
{
    //джойстик
    Vector2 Axis { get; }

    //баттон
    bool IsAttackButtonUp();
}
