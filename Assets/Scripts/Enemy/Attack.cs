using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(EnemyAnimator))]
public class Attack : MonoBehaviour
{
    public float RadiusSphere;

    [SerializeField]
    private EnemyAnimator _animator;

    private IGameFactory _gameFactory;
    private Transform _heroTransform;

    public float AttackColdown = 3f;
    public float Distance = 0.5f;

    private float _attackCooldown;
    private bool _isAttacking;
    private int _layerMask;
    //сколько пересечений может завиксировать оверлап и сохранить в буфер
    private Collider[] _hits = new Collider[1];
    private bool _attackWasActiving;
    [SerializeField]
    private float Damage = 10f;

    private void Awake()
    {
        _gameFactory = AllServices.Container.Single<IGameFactory>();
        _gameFactory.HeroCreated += HeroCreated;

        //сдвиг битовой маски
        _layerMask = 1 << LayerMask.NameToLayer("Player");

     //   _attackIsActive = false;
        // _animator = GetComponent<EnemyAnimator>(); 
        OnAttackEnded();
    }

    private void Update()
    {
        UpdateCooldown();

        if (CanAttack())
            StartAttack();
    }

    public void EnableAttack() =>
        _attackWasActiving = true;

    public void DisableAttack() =>
        _attackWasActiving = false;

    private void OnAttack()
    {
        if(Hit(out Collider hit))
        {
            hit.transform.GetComponent<HeroHealth>().TakeDamage(Damage);
        }
    }


    private void OnAttackEnded()
    {
        _attackCooldown = AttackColdown;
        _isAttacking = false;
    }

    private bool CanAttack() =>
        _attackWasActiving && !_isAttacking && CooldownEnd();

    private void UpdateCooldown()
    {
        if (!CooldownEnd())
            _attackCooldown -= Time.deltaTime;
    }

    private bool CooldownEnd() =>
        _attackCooldown <= 0;

    private bool Hit(out Collider hit)
    {
        Vector3 startPosition = StartPoint();
        int countOverlaps = Physics.OverlapSphereNonAlloc(startPosition, RadiusSphere, _hits, _layerMask);
        //достать из буфера первый оверлап, если он есть
        hit = _hits.FirstOrDefault();

        return countOverlaps > 0;
    }

    private Vector3 StartPoint() =>
        new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z) + transform.forward * Distance;

    private void StartAttack()
    {
        transform.LookAt(_heroTransform);
        _animator.PlayAttack1();

        _isAttacking = true;
    }

    private void HeroCreated() => 
        _heroTransform = _gameFactory.HeroGameObject.transform;
}
