using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private readonly ICoroutineRunner _coroutineRunner;

    //получаем внешной зависимостью
    public SceneLoader(ICoroutineRunner coroutineRunner) => 
        _coroutineRunner = coroutineRunner;

    public void Load(string name, Action onLoaded = null) =>
        _coroutineRunner.StartCoroutine(LoadScene(name, onLoaded));
    // строковое имя сцены, что-то нужно сделать, когда сцена загруженна, но пока оставляем null. Но вообще это опционально. То есть если ничего не передать, то onLoaded будет таким
    //каким его задали, а если передать, то то что передали
    private IEnumerator LoadScene(string nextScene, Action onLoaded = null)
    {
        //если не сделать так, то инитиал сцена будет загружаться даже тогда, когда мы находимся на ней
        if(SceneManager.GetActiveScene().name == nextScene)
        {
            //колбечим, если кто-то вдруг интересовался, и там есть какая-то плюшечка по переходам
            onLoaded?.Invoke();
            //прерываем итератор корутины
            yield break;
        }
        //асинхронная загрузка сцены. возвращает асинкОперэйшн. Мы запросили асинхронную загрузку сцены
        AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene);
        //Дожидаемся выполнения загрузки сцены. После загрузки нужно выполнить onLoaded колбэк.
        //у anyscOperation completed - событие. discard - отказываеся от операции
        ////  waitNextScene.completed += _ => onLoaded?.Invoke();
        //Однако в этом случае будем знать только о начале и конце операции. Но в таком случае мы не сможем видеть прогресс загрузки сцены, операций и тд.
        //более сложный способ с корутиной
        //проверяем свойством isDone
        while (!waitNextScene.isDone)
            yield return null;
            //yield return null;
            //yield return null; если мы хотим скипнуть 3 кадра
            // yield return new WaitForSeconds();

            onLoaded?.Invoke();
        
        
    }
}
