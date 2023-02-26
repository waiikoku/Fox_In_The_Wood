using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] protected CharacterData profile;
    protected int currentHp;
    protected int maxHp;

    protected float currentSpeed;
    protected float maxSpeed;
    protected float acceleration;

    public event Action OnHurt;
    public event Action OnDied;
    public event Action<int> OnHealthChanged;

    protected virtual void Awake()
    {
        if (profile == null) return;
        //Get Profile Data
        maxHp = profile.MaxHp;
        maxSpeed = profile.MaxSpeed;
        acceleration = profile.Acceleration;
        //Initialize Local Data
        currentHp = maxHp;
        currentSpeed = maxSpeed;
    }

    public virtual void TakeDamage(int dmg)
    {
        currentHp -= dmg;
        ClampHp();
        OnHealthChanged?.Invoke(currentHp);
        OnHurt?.Invoke();
    }

    public void Heal(int amount)
    {
        currentHp += amount;
        ClampHp();
        OnHealthChanged?.Invoke(currentHp);
    }

    private void ClampHp()
    {
        currentHp = Mathf.Clamp(currentHp, 0, maxHp);
    }

    protected void RaiseHealthChanged()
    {
        OnHealthChanged?.Invoke(currentHp);
    }

    protected virtual void Died()
    {
        OnDied?.Invoke();
    }
}
