using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.PlayerControlsActions playerControls;
    [SerializeField] private CharacterController charControllerComponent;
    [SerializeField] private Vector2 currentMovementInput;
    [SerializeField] private Vector3 moveInput;
    [SerializeField] private bool isGrounded;
    [SerializeField] private float volacity;
    [SerializeField] private int fallModifier;
    [SerializeField] private float jumpButton;
    [SerializeField] private float pauseButton;

    void Awake()
    {
        playerInput = new PlayerInput();
        playerControls = playerInput.PlayerControls;

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
        #endregion
    }

    void Update()
    {
        
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
