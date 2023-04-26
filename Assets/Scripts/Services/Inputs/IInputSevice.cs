using UnityEngine;

public interface IInputSevice
{
    //джойстик
    Vector2 Axis { get; }

    //баттон
    bool IsAttackButtonUp();
}
