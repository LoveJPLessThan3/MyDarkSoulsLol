using Unity.Burst.CompilerServices;
using UnityEditor.UIElements;
using UnityEngine;

public class HeroMove : MonoBehaviour
{
    public CharacterController CharacterController;
    public float MovementSpeed;
    private IInputSevice _inputService;
    private Camera _camera;

    private void Awake()
    {
        _inputService = Game.InputService;
    }

    private void Start()
    {
        _camera = Camera.main;
    }

    

    private void Update()
    {
        Vector3 movementVector = Vector3.zero;
        if (_inputService.Axis.sqrMagnitude > Constants.Epsilon)
        {
            //преобразуем из локальных координат джостика в мировые
            movementVector = _camera.transform.TransformDirection(_inputService.Axis);
            movementVector.y = 0;
            //не будет влиять на сколько сильно джойстик был вдавлен
            movementVector.Normalize();
            //разворачиваем героя лицом к направлению 
            transform.forward = movementVector;
        }
        //гравитация
        movementVector += Physics.gravity;
        CharacterController.Move(MovementSpeed * movementVector * Time.deltaTime);
    }
    
}
