using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEField : DamageSource
{
    [SerializeField] private float lifeTime = 0.2f;

    private void Start()
    {
        StartCoroutine(RemoveField());
    }

    private IEnumerator RemoveField()
    {
        yield return new WaitForSeconds(lifeTime);

        Destroy(this.gameObject);
    }
}
