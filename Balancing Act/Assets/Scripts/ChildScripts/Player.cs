using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Damageable
{
    private Element _selectedElement;
    private Element _unbalanced = Element.Gray;

    private Rigidbody2D RB = null;
    private PlayerInput PI = null;
    private UIManager UI = null;
    private Camera mainCamera = null;

    private float horizontal = 0;
    private float vertical = 0;

    private bool meleeCD = false;
    private bool magic1CD = false;
    private bool magic2CD = false;
    private bool magic3CD = false;
    private bool unbalanced = false;
    [SerializeField] private int lightBuildUp = 0;
    [SerializeField] private int darkBuildUp = 0;
    private int grayBuildUp = 0;
    private float generalDamageMod = 1f;
    private float lightDamageMod = 1f;
    private float darkDamageMod = 1f;

    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float meleeCDTime = 1f;
    [SerializeField] private float magic1CDTime = 1.5f;
    [SerializeField] private float magic2CDTime = 2f;
    [SerializeField] private float magic3CDTime = 5f;
    [SerializeField] private float meleeTime = 0.2f;
    [SerializeField] private int meleeBuildUp = 5;
    [SerializeField] private int magic1BuildUp = 10;
    [SerializeField] private int magic2BuildUp = 15;
    [SerializeField] private int magic3BuildUp = 10;
    [SerializeField] private GameObject innerPlayer = null;
    [SerializeField] private GameObject cursor = null;
    [SerializeField] private GameObject[] meleeAttacks = null;
    [SerializeField] private GameObject[] magic1Attacks = null;
    [SerializeField] private GameObject[] magic2Attacks = null;

    // Start is called before the first frame update
    void Start()
    {
        _team = Team.Player;
        health = 100;

        _selectedElement = Element.Light;

        RB = this.GetComponent<Rigidbody2D>();
        PI = this.GetComponent<PlayerInput>();
        UI = GameObject.Find("Canvas").GetComponent<UIManager>();
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
            meleeAttacks[currentAttack].GetComponent<DamageSource>().setModifiers(generalDamageMod, lightDamageMod, darkDamageMod);
            meleeCD = true;

            BuildUpGauge(meleeBuildUp, _selectedElement);

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

    public void OnMagic1()
    {
        if (magic1CD)
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

        if (currentAttack != -1 && magic1Attacks[currentAttack] != null)
        {
            GameObject temp = Instantiate(magic1Attacks[currentAttack], this.transform.position, innerPlayer.transform.rotation);
            temp.GetComponent<DamageSource>().setOwner(this.gameObject);
            temp.GetComponent<DamageSource>().setModifiers(generalDamageMod, lightDamageMod, darkDamageMod);
            magic1CD = true;

            BuildUpGauge(magic1BuildUp, _selectedElement);

            StartCoroutine(Magic1CoolDown());
        }
    }

    private IEnumerator Magic1CoolDown()
    {
        yield return new WaitForSeconds(magic1CDTime);

        magic1CD = false;
    }

    public void OnMagic2()
    {
        if (magic2CD)
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

        if (currentAttack != -1 && magic2Attacks[currentAttack] != null)
        {
            GameObject temp = Instantiate(magic2Attacks[currentAttack], this.transform.position, innerPlayer.transform.rotation);
            temp.GetComponent<DamageSource>().setOwner(this.gameObject);
            temp.GetComponent<DamageSource>().setModifiers(generalDamageMod, lightDamageMod, darkDamageMod);
            magic2CD = true;

            BuildUpGauge(magic2BuildUp, _selectedElement);

            StartCoroutine(Magic2CoolDown());
        }
    }

    private IEnumerator Magic2CoolDown()
    {
        yield return new WaitForSeconds(magic2CDTime);

        magic2CD = false;
    }

    public void OnMagic3()
    {

    }

    public void OnSwapElement(InputValue value)
    {
        if (_selectedElement == Element.Gray)
            return;

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

    public void OnEnterGray()
    {
        if (lightBuildUp != 100 || darkBuildUp != 100)
            return;

        _selectedElement = Element.Gray;
        _unbalanced = Element.Gray;

        lightBuildUp = 0;
        darkBuildUp = 0;
        grayBuildUp = 100;

        UI.UpdateGauges(lightBuildUp, Element.Light);
        UI.UpdateGauges(darkBuildUp, Element.Dark);
        UI.UpdateGauges(grayBuildUp, Element.Gray);
    }

    public void BuildUpGauge(int buildUp, Element element)
    {
        switch (element)
        {
            case Element.Light:
                lightBuildUp += buildUp;

                if (lightBuildUp > 100)
                    lightBuildUp = 100;

                UI.UpdateGauges(lightBuildUp, element);
                break;
            case Element.Dark:
                darkBuildUp += buildUp;

                if (darkBuildUp > 100)
                    darkBuildUp = 100;

                UI.UpdateGauges(darkBuildUp, element);
                break;
            case Element.Gray:
                grayBuildUp -= (int) (buildUp * 1.5);

                if (grayBuildUp < 0)
                {
                    grayBuildUp = 0;

                    _selectedElement = Element.Light;
                }

                UI.UpdateGauges(grayBuildUp, element);
                break;
        }
    }

    private void OnDeath()
    {
        // This is an override of the parent OnDeath method

        Debug.Log("The Player has died");
    }
}
