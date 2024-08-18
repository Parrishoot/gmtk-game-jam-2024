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

    // TODO: REMOVE THIS ANNOTATION AS IT'S NOT NECESSARY
    [SerializeField]
    private int currentHealth;

    void Start() {
        currentHealth = health;
    }

    public void Damage(int damage) {
        currentHealth -= damage;

        OnHealthLost?.Invoke();

        if(currentHealth <= 0) {
            OnDeath?.Invoke();
        }
    }

    public void Heal(int healAmount) {
        currentHealth = Math.Min(currentHealth + healAmount, health);
        OnHeal?.Invoke();
    }

    public bool CanBeHealed() {
        return currentHealth < health;
    }
}
