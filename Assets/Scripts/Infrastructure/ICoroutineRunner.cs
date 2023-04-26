using System.Collections;
using UnityEngine;

public interface ICoroutineRunner
{
    //метод, который повторяет сигнатурой метод, который в юнити уже реализован в бехе

    //После этой сигнатуры интерфейс спокойно реализуется в классе, без лишних действий
    Coroutine StartCoroutine(IEnumerator coroutine);
}