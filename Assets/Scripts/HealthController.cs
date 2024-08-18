using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField]
    private int health;

    public Action OnHealthLost { get; set; }

    public Action OnDeath { get; set; }

    public Action OnHeal { get; set; }

    public int CurrentHealth { get; private set; }

    void Start() {
        CurrentHealth = health;
    }

    public void Damage(int damage) {
        CurrentHealth -= damage;

        OnHealthLost?.Invoke();

        if(CurrentHealth <= 0) {
            OnDeath?.Invoke();
        }
    }

    public void Heal(int healAmount) {
        CurrentHealth = Math.Min(CurrentHealth + healAmount, health);
        OnHeal?.Invoke();
    }

    public bool CanBeHealed() {
        return CurrentHealth < health;
    }
}
