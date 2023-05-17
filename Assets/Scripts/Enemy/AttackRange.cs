using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Attack))]
public class AttackRange : MonoBehaviour
{
    private Attack _attack;
    [SerializeField]
    private TriggerObserver _triggerObserver;

    private void Start()
    {
        _attack = GetComponent<Attack>();

        _triggerObserver.TriggerEnter += TriggerEnter;
        _triggerObserver.TriggerExit += TriggerExit;

        _attack.DisableAttack();
    }

    private void TriggerEnter(Collider obj)
    {
        _attack.EnableAttack();
    }

    private void TriggerExit(Collider obj)
    {
        _attack.DisableAttack();
    }
}
