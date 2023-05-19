using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HeroAnimator))]
public class HeroHealth : MonoBehaviour, ISavedProgress
{
    private State _state;
    public HeroAnimator HeroAnimator;
    public event Action HeroHealthChange;
    public float CurrentHp
    {
        get => _state.CurrentHp;
        set
        {
            if(_state.CurrentHp != value)
            {
                _state.CurrentHp = value;
                HeroHealthChange?.Invoke();
            }

        }
    }
    public float MaxHp { get => _state.MaxHp; set => _state.MaxHp = value; }
    public void LoadProgress(ProgressPlayer progress)
    {
        _state = progress.HeroState;
        HeroHealthChange?.Invoke();

    }

    public void UpdateProgress(ProgressPlayer progress)
    {
        progress.HeroState.CurrentHp = CurrentHp;
        progress.HeroState.MaxHp = MaxHp;
    }

    public void TakeDamage(float damage)
    {
        if (CurrentHp <= 0)
            return;
        CurrentHp -= damage;
        HeroAnimator.PlayHit();
    }
}
