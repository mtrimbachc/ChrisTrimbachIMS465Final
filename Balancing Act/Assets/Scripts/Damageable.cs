using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Damageable : MonoBehaviour
{
    [SerializeField] public enum Team {Player, Enemy};
    [SerializeField] public Team _team;

    [SerializeField] public float health = 100f;

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            OnDeath();
        }
    }

    private void OnDeath()
    {
        // This is a stub that children are meant to override
    }
}
