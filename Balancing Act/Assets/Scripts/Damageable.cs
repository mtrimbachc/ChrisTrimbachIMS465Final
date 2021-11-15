using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Damageable : ElementOverhead
{ 
    public Team _team;
    [SerializeField] private Element _resistance;

    [SerializeField] protected float health = 100f;

    public void TakeDamage(float damage, Element incoming)
    {
        //Debug.Log("Hit by: " + incoming);

        if (_resistance != Element.Gray && _resistance == incoming)
        {
            damage *= 0.25f;
            //Debug.Log("Resisted");
        }

        health -= damage;
        //Debug.Log(health);

        if (this is Player)
            this.GetComponent<Player>().UpdateHealth();

        if (health <= 0)
        {
            if (this is Player)
                this.GetComponent<Player>().OnDeath();

            if (this is Enemy)
                this.GetComponent<Enemy>().OnDeath();
        }
    }
}
