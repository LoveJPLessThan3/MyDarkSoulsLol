using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveTrigger : MonoBehaviour
{
    private ISaveLoadService _saveLoadService;

    public BoxCollider BoxCollider;
    public void Awake()
    {
        _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _saveLoadService.SaveProgress();

        Debug.Log("Progress Save");

      //  gameObject.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        if (!BoxCollider)
            return;
        //нарисовать гизмос на коллайдере
        Gizmos.color = new Color32(20, 180, 20, 100);
        Gizmos.DrawCube(transform.position + BoxCollider.center, BoxCollider.size);
    }
}
