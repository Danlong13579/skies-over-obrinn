using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class PlayerController : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.PlayerControlsActions playerControls;
    [Header("Movement Settings")]
    // [SerializeField] private CharacterController charControllerComponent;
    [SerializeField] private Rigidbody playerRb;
    [SerializeField] private CapsuleCollider playerCollider;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Vector2 currentMovementInput;
    [SerializeField] private Vector3 moveInput;
    [SerializeField] private bool isGrounded;
    [SerializeField] private float volacity;
    [SerializeField] private int fallModifier;
    [Header("Button Settings")]
    [SerializeField] private float jumpButton;
    [SerializeField] private float runButton;
    [SerializeField] private bool isRunning;
    [SerializeField] private float pauseButton;

    void Awake()
    {
        playerInput = new PlayerInput();
        playerAnimator = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<CapsuleCollider>();
        playerControls = playerInput.PlayerControls;
        isRunning = false;

        #region Movement
        playerControls.Movement.started += (ctx) => {
            currentMovementInput = ctx.ReadValue<Vector2>();
        };

        playerControls.Movement.canceled += (ctx) => {
            currentMovementInput = ctx.ReadValue<Vector2>();
        };

        playerControls.Movement.performed += (ctx) => {
            currentMovementInput = ctx.ReadValue<Vector2>();
        };
        #endregion

        #region Buttons
        playerControls.Jump.started += (ctx) => {
            jumpButton = ctx.ReadValue<float>();
        };

        playerControls.Jump.canceled += (ctx) => {
            jumpButton = ctx.ReadValue<float>();
        };

        playerControls.PauseMenu.started += (ctx) => {
            pauseButton = ctx.ReadValue<float>();
        };

        playerControls.PauseMenu.canceled += (ctx) => {
            pauseButton = ctx.ReadValue<float>();
        };

        playerControls.Sprint.started += (ctx) => {
            runButton = ctx.ReadValue<float>();
        };

        playerControls.Sprint.canceled += (ctx) => {
            runButton = ctx.ReadValue<float>();
        };
        #endregion
    }

    void Update()
    {
        if (runButton > 0) isRunning = true;

        if (currentMovementInput.x != 0 || currentMovementInput.y != 0) {
            playerAnimator.SetBool("isWalking", true);
            if (isRunning) {
                playerAnimator.SetBool("isRunning", true);
            }
        }

        if (currentMovementInput == Vector2.zero) {
            isRunning = false;
            playerAnimator.SetBool("isRunning", false);
            playerAnimator.SetBool("isWalking", false);
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
