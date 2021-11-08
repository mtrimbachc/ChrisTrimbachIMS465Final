using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Damageable
{
    // Start is called before the first frame update
    void Start()
    {
        _team = Team.Enemy;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Will apparently never actually get called
    private void OnDeath()
    {
        Destroy(this.gameObject);
    }
}
