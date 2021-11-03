using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSource : MonoBehaviour
{
    [SerializeField] public float damage = 0f;

    [SerializeField] public enum Element {light, dark, gray };
    [SerializeField] public Element element = Element.gray;

    [SerializeField] public GameObject owner = null;
    [SerializeField] public bool persistent = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != owner && collision.gameObject.GetComponent<Damageable>() != null)
        {
            Damageable other = collision.GetComponent<Damageable>();

            if (other._team != owner.GetComponent<Damageable>()._team)
            {
                other.TakeDamage(damage);

                if (!persistent)
                {
                    Destroy(this.gameObject);
                }
            }
        } 
    }
}
