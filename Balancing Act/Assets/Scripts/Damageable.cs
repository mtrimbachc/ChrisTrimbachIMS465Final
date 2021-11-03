using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Damageable : ElementOverhead
{
    public enum Team { Player, Enemy };
    public Team _team;
    [SerializeField] private Element _resistance;

    protected float health = 100f;

    public void TakeDamage(float damage, Element incoming)
    {
        if (_resistance != Element.Gray && _resistance == incoming)
        {
            damage *= 0.25f;
        }

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
