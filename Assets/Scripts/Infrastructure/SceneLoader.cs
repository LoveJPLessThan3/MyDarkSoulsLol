using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private readonly ICoroutineRunner _coroutineRunner;

    //�������� ������� ������������
    public SceneLoader(ICoroutineRunner coroutineRunner) => 
        _coroutineRunner = coroutineRunner;

    public void Load(string name, Action onLoaded = null) =>
        _coroutineRunner.StartCoroutine(LoadScene(name, onLoaded));
    // ��������� ��� �����, ���-�� ����� �������, ����� ����� ����������, �� ���� ��������� null. �� ������ ��� �����������. �� ���� ���� ������ �� ��������, �� onLoaded ����� �����
    //����� ��� ������, � ���� ��������, �� �� ��� ��������
    private IEnumerator LoadScene(string nextScene, Action onLoaded = null)
    {
        //���� �� ������� ���, �� ������� ����� ����� ����������� ���� �����, ����� �� ��������� �� ���
        if(SceneManager.GetActiveScene().name == nextScene)
        {
            //��������, ���� ���-�� ����� �������������, � ��� ���� �����-�� �������� �� ���������
            onLoaded?.Invoke();
            //��������� �������� ��������
            yield break;
        }
        //����������� �������� �����. ���������� �������������. �� ��������� ����������� �������� �����
        AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene);
        //���������� ���������� �������� �����. ����� �������� ����� ��������� onLoaded ������.
        //� anyscOperation completed - �������. discard - ����������� �� ��������
        ////  waitNextScene.completed += _ => onLoaded?.Invoke();
        //������ � ���� ������ ����� ����� ������ � ������ � ����� ��������. �� � ����� ������ �� �� ������ ������ �������� �������� �����, �������� � ��.
        //����� ������� ������ � ���������
        //��������� ��������� isDone
        while (!waitNextScene.isDone)
            yield return null;
            //yield return null;
            //yield return null; ���� �� ����� �������� 3 �����
            // yield return new WaitForSeconds();

            onLoaded?.Invoke();
        
        
    }
}
