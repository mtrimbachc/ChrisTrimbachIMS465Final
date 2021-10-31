using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private CharacterController controller = null;
    private PlayerInput PI = null;
    private Camera mainCamera = null;

    private float horizontal = 0;
    private float vertical = 0;

    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private GameObject innerPlayer = null;
    [SerializeField] private GameObject cursor = null;

    // Start is called before the first frame update
    void Start()
    {
        controller = this.GetComponent<CharacterController>();
        PI = this.GetComponent<PlayerInput>();
        mainCamera = this.GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
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

        controller.Move(velocity * moveSpeed * Time.deltaTime);
    }

    public void OnLook(InputValue value)
    {
        Vector3 newLook = new Vector3();
        Vector2 input = value.Get<Vector2>();

        if (PI.currentControlScheme == "Keyboard&Mouse")
        {
            newLook = mainCamera.ScreenToWorldPoint(input);
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
}
