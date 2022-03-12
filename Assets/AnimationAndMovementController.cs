using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationAndMovementController : MonoBehaviour
{
    // Start is called before the first frame update
    PlayerInput playerInput;
    CharacterController characterController;
    Animator animator;

    Vector2 currentMovementInput;
    Vector3 currentMovement;
    bool isMovementPressed;
    bool isMoving;
    bool isMovingBack;
    bool isMovingRight;
    bool isMovingLeft;

    public float acceleration = 10f;
    public float currentReach;
    public Vector3 velocity;
    public float dragFactor = 5f;

    void Awake()
    {
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        playerInput.CharacterControl.Movement.started += onMovementInput;
        playerInput.CharacterControl.Movement.canceled += onMovementInput;
    }
    void onMovementInput (InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        currentMovement.z = -currentMovementInput.x;
        currentMovement.x = currentMovementInput.y;
        Vector3 input = new Vector3(currentMovement.z, 0f, currentMovement.x);

        velocity += input * acceleration * Time.deltaTime;
        velocity *= dragFactor;
        //velocity.y -= gravity * Time.deltaTime;
        isMovementPressed = currentMovementInput.x != 0 | currentMovementInput.y != 0;
        Debug.Log(context.ReadValue<Vector2>());
    }

    // Update is called once per frame
    void handleAnimation()
    {
        bool isMoving = animator.GetBool("isMoving");
        bool isRunning = animator.GetBool("isRunning");
        if (isMovementPressed && !isMoving)
        {
            animator.SetBool("isMoving", true);
        }
        else if (!isMovementPressed && isMoving)
        {
            animator.SetBool("isMoving", false);
        }
    }
    void Update()
    {   

        handleAnimation();
        characterController.Move(currentMovement*Time.deltaTime*10f);
    }
    void OnEnable()
    {
        playerInput.CharacterControl.Enable();
    }
    void OnDisable()
    {
        playerInput.CharacterControl.Disable();
    }

}
