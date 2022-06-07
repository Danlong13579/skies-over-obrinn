using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.PlayerControlsActions playerControls;

    [Header("Movement Data")]
    [SerializeField] private Rigidbody playerRb;
    [SerializeField] private CapsuleCollider playerCollider;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Vector2 currentMovementInput;
    [SerializeField] private Vector3 moveInput;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float runModifier;
    [SerializeField] private int fallModifier;
    [Header("Rotation Data")]
    [SerializeField] private Vector2 currentRotationInput;
    [SerializeField] private Vector3 rotatonInput;
    [SerializeField] private float rotationSpeed;

    [Header("Button Settings")]
    [SerializeField] private float jumpButton;
    [SerializeField] private float runButton;
    [SerializeField] private float pauseButton;

    [Header("Game Logic")]
    [SerializeField] private bool isRunning;
    [SerializeField] private bool isGrounded;

    void Awake()
    {
        playerInput = new PlayerInput();
        playerAnimator = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<CapsuleCollider>();
        playerControls = playerInput.PlayerControls;
        isRunning = false;
        moveSpeed = 50;
        runModifier = 1;

        #region Movement
        playerControls.Movement.started += (ctx) =>
        {
            currentMovementInput = ctx.ReadValue<Vector2>();
        };

        playerControls.Movement.canceled += (ctx) =>
        {
            currentMovementInput = ctx.ReadValue<Vector2>();
        };

        playerControls.Movement.performed += (ctx) =>
        {
            currentMovementInput = ctx.ReadValue<Vector2>();
        };

        playerControls.CameraRotation.started += (ctx) =>
        {
            currentRotationInput = ctx.ReadValue<Vector2>();
        };

        playerControls.CameraRotation.canceled += (ctx) =>
        {
            currentRotationInput = ctx.ReadValue<Vector2>();
        };

        playerControls.CameraRotation.performed += (ctx) =>
        {
            currentRotationInput = ctx.ReadValue<Vector2>();
        };
        #endregion

        #region Buttons
        playerControls.Jump.started += (ctx) =>
        {
            jumpButton = ctx.ReadValue<float>();
        };

        playerControls.Jump.canceled += (ctx) =>
        {
            jumpButton = ctx.ReadValue<float>();
        };

        playerControls.PauseMenu.started += (ctx) =>
        {
            pauseButton = ctx.ReadValue<float>();
        };

        playerControls.PauseMenu.canceled += (ctx) =>
        {
            pauseButton = ctx.ReadValue<float>();
        };

        playerControls.Sprint.started += (ctx) =>
        {
            runButton = ctx.ReadValue<float>();
        };

        playerControls.Sprint.canceled += (ctx) =>
        {
            runButton = ctx.ReadValue<float>();
        };
        #endregion
    }

    void Update()
    {
        Debug.Log(Input.GetAxis("Mouse X"));
        if (runButton > 0) isRunning = true;
        moveInput.z = currentMovementInput.y;
        moveInput.x = currentMovementInput.x;
        rotatonInput.x = currentMovementInput.x;
        rotatonInput.z = currentMovementInput.y;

        HandlePlayerAnimation();
        MovePlayer();
        RotatePlayer();
    }

    void MovePlayer() // TODO is velocity the right thing here
    {
        playerRb.velocity = moveInput * (moveSpeed * runModifier) * Time.fixedDeltaTime;
    }

    void RotatePlayer() // TODO fix snappy rotation spinning issue
    {
        Vector3 currentPosition = transform.position;
        Vector3 newPosition = new Vector3(currentMovementInput.x, 0, currentMovementInput.y);
        Vector3 pta = currentPosition + newPosition;
        transform.LookAt(pta);
    }

    void HandlePlayerAnimation() // TODO clean up animation logic from update
    {
        if (currentMovementInput == Vector2.zero)
        {
            runModifier = 1;
            isRunning = false;
            playerAnimator.SetBool("isRunning", false);
            playerAnimator.SetBool("isWalking", false);
            return;
        }

        if (currentMovementInput.y != 0 || currentMovementInput.x != 0)
        {
            playerAnimator.SetBool("isWalking", true);
            if (isRunning)
            {
                runModifier = 3.3f;
                playerAnimator.SetBool("isRunning", true);
            }
        }

    }

    void OnEnable()
    {
        playerControls.Enable();
    }

    void OnDisable()
    {
        playerControls.Disable();
    }

}
