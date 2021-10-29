using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private CharacterController controller = null;

    private float horizontal = 0;
    private float vertical = 0;

    [SerializeField] private float moveSpeed = 8f;

    // Start is called before the first frame update
    void Start()
    {
        controller = this.GetComponent<CharacterController>();
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
}
