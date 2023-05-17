using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
//using UnityEditor.Animations;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour, IAnimationStateReader
{
    private static readonly int Die = Animator.StringToHash("Die");
    private static readonly int Victory = Animator.StringToHash("Victory");
    private static readonly int Attack1 = Animator.StringToHash("Attack_1");
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int Attack2 = Animator.StringToHash("Attack_2");
    private static readonly int Hit = Animator.StringToHash("Hit");

    private readonly int _walkingStateHash = Animator.StringToHash("Move");
    private readonly int _attackStateHash = Animator.StringToHash("attack01");
    private readonly int _idleStateHash = Animator.StringToHash("idle");
    private readonly int _dieStateHash = Animator.StringToHash("die");

    public event Action<AnimatorState> StateEntered;
    public event Action<AnimatorState> StateExited;

    private Animator _animator;

    public AnimatorState State { get; private set; }

    // Start is called before the first frame update
    private void Awake() => 
        _animator = gameObject.GetComponent<Animator>();

    public void PlayDeath() =>
        _animator.SetTrigger(Die);

    public void PlayVictory()
        => _animator.SetTrigger(Victory);
    public void PlayAttack1()
        => _animator.SetTrigger(Attack1);
    public void PlayAttack2()
        => _animator.SetTrigger(Attack2);
    public void PlayHit()
        => _animator.SetTrigger(Hit);
    public void Move(float speed)
    {
        _animator.SetBool(IsMoving, true);
        _animator.SetFloat(Speed, speed);
    }
    public void StopMove()
        => _animator.SetBool(IsMoving, false);

   
    public void EnteredState(int stateHash)
    {
        State = StateFor(stateHash);
        StateEntered?.Invoke(State);
    }
    public void ExitedState(int stateHash)
        => StateExited?.Invoke(State);

    private AnimatorState StateFor(int stateHash)
    {
        AnimatorState state;
        if (stateHash == _idleStateHash)
            state = AnimatorState.Idle;
        else if (stateHash == _dieStateHash)
            state = AnimatorState.Die;
        else if (stateHash == _attackStateHash)
            state = AnimatorState.Attack;
        else if (stateHash == _walkingStateHash)
            state = AnimatorState.Walking;
        else state = AnimatorState.Unknown;

        return state;
    }

}
