using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : DamageSource
{
    public float moveSpeed = 480f;
    protected Rigidbody2D RB = null;

    private void Start()
    {
        RB = this.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        RB.velocity = this.transform.up * moveSpeed * Time.fixedDeltaTime;
    }

    public void DespawnTime(float time)
    {
        StartCoroutine(Despawn(time));
    }

    private IEnumerator Despawn(float time)
    {
        Debug.Log("Despawning");

        yield return new WaitForSeconds(time);

        Despawn();
    }

    public void Despawn()
    {
        if (this is ExplosiveProjectile)
            this.GetComponent<ExplosiveProjectile>().CreateAOEField();

        if (owner != null)
        {
            //moveSpeed = 0f;
            RB.velocity = Vector2.zero;
            this.transform.position = owner.transform.position;
            this.gameObject.SetActive(false);
        }
        else
            Destroy(this.gameObject);
    }
}
