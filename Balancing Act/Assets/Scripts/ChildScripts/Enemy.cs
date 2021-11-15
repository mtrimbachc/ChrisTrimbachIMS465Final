using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Damageable
{
    private Rigidbody2D RB = null;
    private Player player = null;

    protected float moveSpeed = 160f;
    [SerializeField] protected float followDistance = 30f;
    [SerializeField] protected float approachDistance = 2f;
    protected float distanceToPlayer = float.MaxValue;
    [SerializeField] protected bool mobile = true;
    protected bool meleeCD = false;
    protected bool rangedCD = false;
    [SerializeField] protected float meleeTime = 0.2f;
    [SerializeField] protected float meleeCDTime = 1f;
    [SerializeField] protected float rangedCDTime = 1f;

    private Vector3 playerV = new Vector3();
    [SerializeField] private GameObject meleeAttack = null;
    [SerializeField] private GameObject rangedAttack = null;


    // Start is called before the first frame update
    void Start()
    {
        _team = Team.Enemy;

        RB = this.GetComponent<Rigidbody2D>();
        RB.freezeRotation = true;
        player = GameObject.Find("Player").GetComponent<Player>();

        if (meleeAttack != null)
            meleeAttack.GetComponent<DamageSource>().setOwner(this.gameObject);
    }
    void FixedUpdate()
    {
        Movement();

        if (distanceToPlayer <= approachDistance)
            Attack();
    }

    private void Movement()
    {
        playerV = player.transform.position - this.transform.position;
        distanceToPlayer = playerV.magnitude;

        if (mobile && distanceToPlayer <= followDistance && distanceToPlayer > approachDistance)
        {
            RB.velocity = playerV.normalized * moveSpeed * Time.fixedDeltaTime;
        }
        else
        {
            RB.velocity = new Vector2();
        }

        float angle = Mathf.Atan2(playerV.normalized.x, playerV.normalized.y) % (2 * Mathf.PI);
        angle = angle * -Mathf.Rad2Deg;

        this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    protected void Attack()
    {
        //Debug.Log("In Attacking Range");

        if (meleeAttack != null)
        {
            if (meleeCD)
                return;

            meleeAttack.SetActive(true);
            meleeAttack.GetComponent<BoxCollider2D>().enabled = true;
            meleeCD = true;

            StartCoroutine(MeleeDespawn());
            StartCoroutine(MeleeCoolDown());
        }

        if (rangedAttack != null)
        {
            if (rangedCD)
                return;

            GameObject temp = Instantiate(rangedAttack, this.transform.position, this.transform.rotation);
            temp.GetComponent<DamageSource>().setOwner(this.gameObject);

            rangedCD = true;

            StartCoroutine(RangedCoolDown());
        }
    }

    private IEnumerator MeleeDespawn()
    {
        yield return new WaitForSeconds(meleeTime);

        meleeAttack.GetComponent<BoxCollider2D>().enabled = false;
        meleeAttack.SetActive(false);
    }

    private IEnumerator MeleeCoolDown()
    {
        yield return new WaitForSeconds(meleeCDTime);

        meleeCD = false;
    }

    private IEnumerator RangedCoolDown()
    {
        yield return new WaitForSeconds(rangedCDTime);

        rangedCD = false;
    }

    public void OnDeath()
    {
        // double check health to make sure the call is valid
        if (health <= 0)
            Destroy(this.gameObject);
    }
}
