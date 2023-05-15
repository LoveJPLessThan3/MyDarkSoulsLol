using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeroMove : MonoBehaviour, ISavedProgress
{
    public CharacterController _characterController;
    public float MovementSpeed;
    private IInputService _inputService;
    private Camera _camera;

    public void LoadProgress(ProgressPlayer progress)
    {
        //Extention
        progress.WorldData.PositionOnLevel = new PositionOnLevel(position: transform.position.AsVectorData(), level: CurrentLevel());

    }

    public void UpdateProgress(ProgressPlayer progress)
    {
        //��������� ������ ������ �� ������ ��� �� ���������
        if (CurrentLevel() == progress.WorldData.PositionOnLevel.Level)
        {
            var savedPosition = progress.WorldData.PositionOnLevel.Position;

            if (savedPosition != null)
            {
                Warp(to: savedPosition);
            }
        }

    }

    private void Warp(Vector3Data to)
    {
        //����� �� ���� ����������� � ���������.
        _characterController.enabled = false;
        transform.position = to.AsUnityVector().AddY(_characterController.height);
        _characterController.enabled = true;
    }

    private static string CurrentLevel()
    {
        return SceneManager.GetActiveScene().name;
    }


    private void Awake()
    {
        _inputService = AllServices.Container.Single<IInputService>();
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
            //����������� �� ��������� ��������� �������� � �������
            movementVector = _camera.transform.TransformDirection(_inputService.Axis);
            movementVector.y = 0;
            //�� ����� ������ �� ������� ������ �������� ��� �������
            movementVector.Normalize();
            //������������� ����� ����� � ����������� 
            transform.forward = movementVector;
        }
        //����������
        movementVector += Physics.gravity;
        _characterController.Move(MovementSpeed * movementVector * Time.deltaTime);
    }

}
