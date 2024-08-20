using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageShaker : MonoBehaviour
{
    [SerializeField]
    private CharacterAnimationController animator;

    [SerializeField]
    private HealthController healthController;

    void Start() {
        healthController.OnHealthLost += () => animator.Damage();
    }
}
