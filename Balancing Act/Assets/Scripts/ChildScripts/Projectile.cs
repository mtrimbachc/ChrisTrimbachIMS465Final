using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : DamageSource
{
    [SerializeField] private float moveSpeed = 480f;
    protected Rigidbody2D RB = null;

    private void Start()
    {
        RB = this.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        RB.velocity = this.transform.up * moveSpeed * Time.fixedDeltaTime;
    }
}
