using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSource : ElementOverhead
{
    [SerializeField] private float damage = 0f;
    protected float generalDamageMod = 1f;
    protected float lightDamageMod = 1f;
    protected float darkDamageMod = 1f;

    [SerializeField] private Element _element = Element.Gray;

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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != owner && collision.gameObject.GetComponent<Damageable>() != null)
        {
            Damageable other = collision.GetComponent<Damageable>();

            float tempDamage = damage;

            if (other._team != owner.GetComponent<Damageable>()._team)
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
                    Destroy(this.gameObject);
                }
            }
        } 
        else if (collision.gameObject != owner && collision.gameObject.layer == 6)
        {
            if (!persistent)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
