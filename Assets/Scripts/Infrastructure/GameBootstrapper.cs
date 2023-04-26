using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
//Entrypoint

public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
{
    //����
    private Game _game;
    public LoadingCurtain Curtain;

    //����� ����� � ����.
    private void Awake()
    {
        // ������� ����. �������� this, ��� ����� ����������� ��������� ICoroutineRunner
        _game = new Game(this, Curtain);
        //����� � ������� �������� ������� �� ����������
        _game._stateMachine.EnterState<BootstrapState>();
        DontDestroyOnLoad(this);
    }
}
