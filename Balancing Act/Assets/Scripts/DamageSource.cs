using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSource : ElementOverhead
{
    [SerializeField] private float damage = 0f;
    protected float generalDamageMod = 1f;
    protected float lightDamageMod = 1f;
    protected float darkDamageMod = 1f;

    [SerializeField] protected Element _element;
    [SerializeField] protected Team _team;

    protected GameObject owner = null;
    [SerializeField] private bool persistent = false;

    public void setModifiers(float general, float light, float dark)
    {
        generalDamageMod = general;
        lightDamageMod = light;
        darkDamageMod = dark;
    }

    public void setOwner(GameObject owner)
    {
        this.owner = owner;

        if (owner.GetComponent<Player>() != null)
            this._team = Team.Player;
        else
            this._team = Team.Enemy;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != owner && collision.gameObject.GetComponent<Damageable>() != null)
        {
            Damageable other = collision.GetComponent<Damageable>();

            float tempDamage = damage;

            //Debug.Log(this.owner);

            if (other._team != this._team)
            {
                switch(_element)
                {
                    case Element.Light:
                        tempDamage *= lightDamageMod;
                        break;
                    case Element.Dark:
                        tempDamage *= darkDamageMod;
                        break;
                }

                other.TakeDamage(tempDamage * generalDamageMod, _element);

                if (!persistent)
                {
                    if (this is Projectile)
                    {
                        this.GetComponent<Projectile>().StopAllCoroutines();
                        this.GetComponent<Projectile>().Despawn();
                    }
                }
            }
        } 
        else if (collision.gameObject != owner && collision.gameObject.layer == 6)
        {
            if (!persistent)
            {
                if (this is Projectile)
                {
                    this.GetComponent<Projectile>().StopAllCoroutines();
                    this.GetComponent<Projectile>().Despawn();
                }
            }
        }
    }
}
