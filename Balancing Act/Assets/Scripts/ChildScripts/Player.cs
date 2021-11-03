using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Damageable
{
    public enum Element { Light, Dark, Gray};
    private Element _selectedElement;

    private Rigidbody2D RB = null;
    private PlayerInput PI = null;
    private Camera mainCamera = null;

    private float horizontal = 0;
    private float vertical = 0;

    private bool meleeCD = false;

    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float meleeCDTime = 1f;
    [SerializeField] private float meleeTime = 0.2f;
    [SerializeField] private GameObject innerPlayer = null;
    [SerializeField] private GameObject cursor = null;
    [SerializeField] private GameObject[] meleeAttacks = null;

    // Start is called before the first frame update
    void Start()
    {
        _team = Team.Player;
        health = 100;

        _selectedElement = Element.Light;

        RB = this.GetComponent<Rigidbody2D>();
        PI = this.GetComponent<PlayerInput>();
        mainCamera = this.GetComponentInChildren<Camera>();

        RB.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Movement();
    }

    public void OnMove(InputValue value)
    {
        Debug.Log("In OnMove");

        if (value.Get<Vector2>() != null)
        {
            horizontal = value.Get<Vector2>().x;
            vertical = value.Get<Vector2>().y;
        }
    }

    private void Movement()
    {
        Vector2 velocity = new Vector2(horizontal, vertical).normalized;

        RB.velocity = velocity * moveSpeed * Time.fixedDeltaTime;
    }

    public void OnLook(InputValue value)
    {
        Vector3 newLook = new Vector3();
        Vector2 input = value.Get<Vector2>();

        if (PI.currentControlScheme == "Keyboard&Mouse")
        {
            newLook = mainCamera.ScreenToWorldPoint(input);
            newLook.z = this.transform.position.z;
            newLook = Vector3.Normalize(newLook - this.transform.position);
        }
        else
        {
            cursor.SetActive(false);

            newLook = new Vector3(this.transform.position.x + input.x, this.transform.position.y + input.y, this.transform.position.z);
        }

        float angle = Mathf.Atan2(newLook.x, newLook.y) % (2 * Mathf.PI);
        angle = angle * -Mathf.Rad2Deg;

        innerPlayer.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    public void OnMelee()
    {
        if (meleeCD)
            return;

        int currentAttack = -1;

        switch (_selectedElement)
        {
            case Element.Light:
                currentAttack = 0;

                break;
            case Element.Dark:
                currentAttack = 1;

                break;
            case Element.Gray:
                currentAttack = 2;

                break;
            default:
                break;
        }

        if (currentAttack != -1 && !meleeAttacks[currentAttack].activeSelf)
        {
            meleeAttacks[currentAttack].SetActive(true);
            meleeCD = true;

            StartCoroutine(MeleeDespawn(currentAttack));
            StartCoroutine(MeleeCoolDown());
        }
    }

    private IEnumerator MeleeDespawn(int attack)
    {
        yield return new WaitForSeconds(meleeTime);

        meleeAttacks[attack].SetActive(false);
    }

    private IEnumerator MeleeCoolDown()
    {
        yield return new WaitForSeconds(meleeCDTime);

        meleeCD = false;
    }

    public void OnSwapElement(InputValue value)
    {
        if (value.Get<float>() == 0)
        {
            if (_selectedElement == Element.Light)
            {
                _selectedElement = Element.Dark;
                return;
            }

            if (_selectedElement == Element.Dark)
            {
                _selectedElement = Element.Light;
                return;
            }
        }
    }

    private void OnDeath()
    {
        // This is an override of the parent OnDeath method

        Debug.Log("The Player has died");
    }
}
