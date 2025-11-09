using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PaddleController : MonoBehaviour, InputSystem_Actions.IPlayerActions
{
    InputSystem_Actions controls;
    Rigidbody rb;
    private Vector2 inputVector;
    [SerializeField] private int paddleMovementSpeed;
    [SerializeField] private float leftLimit;   
    [SerializeField] private float rightLimit;
    private void Awake()
    {
        controls = new InputSystem_Actions();
        controls.Player.SetCallbacks(this);
        rb = GetComponent<Rigidbody>();
    }

    public void OnEnable()
    {
        controls.Enable();
    }

    public void OnDisable()
    {
        controls.Disable();
    }
  


    public void FixedUpdate()
    {
        Vector3 move = new Vector3(inputVector.x, 0f, 0f) * paddleMovementSpeed * Time.fixedDeltaTime;
        Vector3 paddlePos = rb.position + move;

        paddlePos.x = Mathf.Clamp(paddlePos.x, leftLimit, rightLimit);
        rb.position = paddlePos;
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
    }

    public void OnJump(InputAction.CallbackContext context)
    {
    }

    public void OnLook(InputAction.CallbackContext context)
    {
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        inputVector = context.ReadValue<Vector2>();
    }

    public void OnNext(InputAction.CallbackContext context)
    {
    }

    public void OnPrevious(InputAction.CallbackContext context)
    {
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
    }
}

