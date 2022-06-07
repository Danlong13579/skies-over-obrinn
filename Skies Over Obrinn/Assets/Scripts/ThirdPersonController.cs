using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Animator))]
public class ThirdPersonController : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.PlayerControlsActions playerControls;
    private InputAction movementEvent;
    private InputAction jumpEvent;
    
    [Header("Component Data")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private CapsuleCollider playerCollider;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Camera playerCamera;

    [Header("Movement Data")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float runModifier;
    [SerializeField] private int fallModifier;
    [SerializeField] private Vector2 movementInput;
    [SerializeField] private Vector3 forceDirection;

    void Awake()
    {
        playerInput = new PlayerInput();
        playerAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<CapsuleCollider>();
        playerControls = playerInput.PlayerControls;
    }

    void Update()
    {
        movementInput = movementEvent.ReadValue<Vector2>();
        playerAnimator.SetBool("isWalking", true);
    }

    void FixedUpdate()
    {
        forceDirection += movementInput.x * GetCameraRight(playerCamera) * moveSpeed * Time.fixedDeltaTime;
        forceDirection += movementInput.y * GetCameraForward(playerCamera) * moveSpeed * Time.fixedDeltaTime;

        rb.AddForce(forceDirection, ForceMode.Impulse);
        forceDirection = Vector3.zero;
    }

    #region ActionMaping

    void DoJump(InputAction.CallbackContext input)
    {
        if(IsGrounded())
        {
            Debug.Log("Jumping");
        }
    }

    void DoSprint(InputAction.CallbackContext input)
    {
        moveSpeed = moveSpeed * runModifier;
        Debug.Log("Sprinting");
    }

    void DoPause(InputAction.CallbackContext input)
    {
        PauseController.PauseGame();
        Debug.Log(PauseController.isPaused);
    }

    #endregion

    #region Controller Utils

    Vector3 GetCameraRight(Camera playerCamera)
    {
        Vector3 right = playerCamera.transform.right;
        right.y = 0;
        return right.normalized;
    }

    Vector3 GetCameraForward(Camera playerCamera)
    {
        Vector3 forward = playerCamera.transform.forward;
        forward.y = 0;
        return forward.normalized;
    }

    bool IsGrounded()
    {
        // Collision logic & Coyote Time for grounding
        return true;
    }

    #endregion

    void OnEnable()
    {
        playerControls.Enable();
        movementEvent = playerControls.Movement;
        playerControls.Jump.performed += DoJump;
        playerControls.Sprint.performed += DoSprint;
        playerControls.PauseMenu.performed += DoPause;
    }

    void onDisable()
    {
        playerControls.Disable();
        playerControls.Jump.performed -= DoJump;
        playerControls.Sprint.performed -= DoSprint;
        playerControls.PauseMenu.performed -= DoPause;
    }
}
