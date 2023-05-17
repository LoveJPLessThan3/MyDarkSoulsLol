using System;
using System.Collections;
using UnityEngine;

public class Aggro : MonoBehaviour
{
    public TriggerObserver TriggerObserver;
    public AgentMove Follow;
    [SerializeField]
    private float Cooldown;
    private Coroutine _aggroCooldownCourutine;
    private bool _aggroTargetExist;

    private void Start()
    {
        TriggerObserver.TriggerEnter += TriggerEnter;
        TriggerObserver.TriggerExit += TriggerExit;

        SwitchFollowOff();
    }

    private void TriggerExit(Collider obj)
    {
        if (_aggroTargetExist)
        {
            _aggroTargetExist = false;
            _aggroCooldownCourutine = StartCoroutine(SwitchFollowCooldown());
        }

    }

    private IEnumerator SwitchFollowCooldown()
    {
        yield return new WaitForSeconds(Cooldown);
        SwitchFollowOff();

    }

    private void TriggerEnter(Collider obj)
    {
        if (!_aggroTargetExist)
        {
            _aggroTargetExist = true;
            StopAggroCoroutine();
            SwitchFollowOn();
        }


    }

    private void StopAggroCoroutine()
    {
        if (_aggroCooldownCourutine != null)
        {
            StopCoroutine(_aggroCooldownCourutine);
            _aggroCooldownCourutine = null;
        }
    }

    private void SwitchFollowOn() =>
        Follow.enabled = true;

    private void SwitchFollowOff() =>
        Follow.enabled = false;
}
