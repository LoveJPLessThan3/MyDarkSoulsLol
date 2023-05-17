using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentMove : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent _agent;
    private Transform _heroTransform;
    [SerializeField]
    private float _minimalDistance;
    private IGameFactory _gameFactory;

    private void Start()
    {

        _gameFactory = AllServices.Container.Single<IGameFactory>();
        if (_gameFactory.HeroGameObject != null)
            InitializeHeroTransform();
        else
        {
            _gameFactory.HeroCreated += HeroCreated;
        }
    }

    private void Update()
    {
        if (!_heroTransform != null && ReachedToHero())
            _agent.destination = _heroTransform.position;
    }
    private void HeroCreated() => 
        InitializeHeroTransform();

    private void InitializeHeroTransform() => 
        _heroTransform = _gameFactory.HeroGameObject.transform;


    private bool ReachedToHero()
    {
        var vectorDisctanceBeetweenHeroAndEnemy = Vector3.Distance(_agent.transform.position, _heroTransform.position);
        Debug.Log(vectorDisctanceBeetweenHeroAndEnemy);
        return vectorDisctanceBeetweenHeroAndEnemy > _minimalDistance;
    }
}
