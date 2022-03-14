using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationAndMovementController : MonoBehaviour
{
    // Start is called before the first frame update
    PlayerInput playerInput;
    GameObject ostacle;
    CharacterController characterController;
    public CharacterController modelController;
    public Animator animator;
    public Transform model;
    Vector2 currentMovementInput;
    Vector3 currentMovement;
    bool isMovementPressed;
    bool isMoving;
    bool isMovingBackward;
    bool isMovingRight;
    bool isMovingLeft;
    bool isJumping;
    bool flag = false;
    bool fallflag = false;
    bool forward=false;
    bool backward=false;


    
    public float speed = 6;
    public float gravity = -9.81f;
    public float jumpHeight = 3;
    Vector3 velocity;
    bool isGrounded;
    public Transform groundCheck;
    public float groundDistance = 1;
    public LayerMask groundMask;

    public float acceleration = 10f;
    public float currentReach;
    public GameManager gameManager;
    public float dragFactor = 5f;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        ostacle = hit.collider.gameObject;
        if (hit.collider.gameObject.tag == "forward")
        {
            forward = true;
            handleAnimation();
            
            Debug.Log("forward");
        }
        if (hit.collider.gameObject.tag == "back")
        {
            backward = true;
            Debug.Log("backward");
        }


    }

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;

        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        //animator = GetComponent<Animator>();
        playerInput.CharacterControl.Movement.started += onMovementInput;
        playerInput.CharacterControl.Movement.performed += onMovementInput;
        playerInput.CharacterControl.Movement.canceled += onMovementInput;
    }
    void onMovementInput (InputAction.CallbackContext context)
    {
        
        currentMovementInput = context.ReadValue<Vector2>();
        currentMovement.z = -currentMovementInput.x;
        currentMovement.x = currentMovementInput.y;
        Vector3 input = new Vector3(currentMovement.z, 0f, currentMovement.x);


        //velocity.y -= gravity * Time.deltaTime;
        isMovementPressed = currentMovementInput.x != 0 | currentMovementInput.y != 0;
        isMovingLeft = currentMovementInput.x < 0 ;
        isMovingRight = currentMovementInput.x > 0;
        isMovingBackward = currentMovementInput.y < 0;
        isMoving = currentMovementInput.y > 0;
        if (isMovementPressed)
        {
            animator.SetBool("isPressing", true);
            Debug.Log("Press");
            
            //animator.Play("New Animation");
        }
        else
        {
            animator.SetBool("isPressing", false);
        }
        //Debug.Log(context.ReadValue<Vector2>());
    }

    // Update is called once per frame
    
    void handleTripping()
    {

    }
    void handleAnimation()
    {

        //bool isMoving = animator.GetBool("isMoving");
        //bool isRunning = animator.GetBool("isRunning");
        animator.SetBool("forward", false);
        animator.SetBool("backward", false);
        if (isMovingLeft == true)
        {
            animator.SetBool(nameof(isMovingLeft), true);
        }
        if (isMovingLeft == false)
        {
            animator.SetBool(nameof(isMovingLeft), false);
        }
        if (isMovingRight == true)
        {
            animator.SetBool(nameof(isMovingRight), true);
        }
        if (isMovingRight == false)
        {
            animator.SetBool(nameof(isMovingRight), false);
        }
        if (isMoving == true)
        {
            animator.SetBool(nameof(isMoving), true);
            animator.SetBool(nameof(isMovingRight), false);
            animator.SetBool(nameof(isMovingLeft), false);
        }
        if (isMoving == false)
        {
            animator.SetBool(nameof(isMoving), false);
        }
        if (isMovingBackward == true)
        {
            animator.SetBool(nameof(isMovingBackward), true);
            animator.SetBool(nameof(isMovingRight), false);
            animator.SetBool(nameof(isMovingLeft), false);
        }
        if (isMovingBackward == false)
        {
            animator.SetBool(nameof(isMovingBackward), false);
        }
        if (isJumping == true && !flag)
        {
            animator.Play("jump");
            flag = true;
            /*
            animator.SetBool(nameof(isJumping), true);
            animator.SetBool(nameof(isMovingRight), false);
            animator.SetBool(nameof(isMovingLeft), false);
            animator.SetBool(nameof(isMoving), false);
            animator.SetBool(nameof(isMovingBackward), false);
            */
        }
        if (isJumping == false)
        {
            animator.SetBool(nameof(isJumping), false);
            
        }
        if (backward == true)
        {

            //animator.SetBool("backward", true);
            
            animator.Play("falling_backward");
            animator.Play("standingup");
            //fallflag = true;
        }
        if (forward == true )
        {
            //animator.SetBool("forward", true);
            
            animator.Play("falling_forward");
            animator.Play("standingup");
            //fallflag = true;
        }
        forward = false;
        backward = false;





    }
    void Update()
    {


        handleAnimation();
        //jump
        
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        playerInput.CharacterControl.jump.started += contex =>
        {
            
            if (isGrounded)
            {
                isJumping = true;
                handleAnimation();
                velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
            }
        };

        playerInput.CharacterControl.jump.canceled += contex =>
        {
            flag = false;
            isJumping = false;
        };


        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        playerInput.CharacterControl.pause.performed += contex =>
        {
            gameManager.EndGame();
        };
        fallflag = false;
        //gravity
        velocity.y += gravity * Time.deltaTime;
        
        characterController.Move(velocity * Time.deltaTime*acceleration );
        

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
