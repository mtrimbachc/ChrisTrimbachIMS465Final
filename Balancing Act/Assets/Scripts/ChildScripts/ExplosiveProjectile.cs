using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveProjectile : Projectile
{
    [SerializeField] private GameObject aoeField = null;
    private void Start()
    {
        RB = this.GetComponent<Rigidbody2D>();
    }

    public void CreateAOEField()
    {
        GameObject temp = Instantiate(aoeField, this.transform.position, Quaternion.identity);
        temp.GetComponent<DamageSource>().setOwner(this.owner);
        temp.GetComponent<DamageSource>().setModifiers(generalDamageMod, lightDamageMod, darkDamageMod);
    }
}
