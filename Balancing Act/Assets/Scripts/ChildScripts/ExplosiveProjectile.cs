using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveProjectile : Projectile
{
    [SerializeField] private GameObject aoeField = null;
    [SerializeField] private float lifeTime = 1f;
    private void Start()
    {
        RB = this.GetComponent<Rigidbody2D>();

        StartCoroutine(TimedAOEField());
    }

    private IEnumerator TimedAOEField()
    {
        yield return new WaitForSeconds(lifeTime);

        CreateAOEField();
    }

    private void CreateAOEField()
    {
        GameObject temp = Instantiate(aoeField, this.transform.position, Quaternion.identity);
        temp.GetComponent<DamageSource>().setOwner(this.owner);
        temp.GetComponent<DamageSource>().setModifiers(generalDamageMod, lightDamageMod, darkDamageMod);

        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != owner && (collision.gameObject.GetComponent<Damageable>() != null || collision.gameObject.layer == 6) )
        {
            CreateAOEField();
        }
    }
}
