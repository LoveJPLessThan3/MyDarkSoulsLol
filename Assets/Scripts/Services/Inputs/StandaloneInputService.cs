using UnityEngine;

public class StandaloneInputService : InputService
{
    public override Vector2 Axis
    {
        get
        {
            Vector2 axis = SimpleInputAxis();

            if (axis == Vector2.zero)
            {
                axis = UnityInputAxis();
            }
            return axis;
        }
    }

    private static Vector2 UnityInputAxis() =>
        new Vector2(UnityEngine.Input.GetAxis(Horizontal), UnityEngine.Input.GetAxis(Vertical));

  
}