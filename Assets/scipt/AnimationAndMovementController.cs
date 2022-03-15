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
    bool isRunning;
    bool isMovementPressed;
    bool isMoving;
    bool isMovingBackward;
    bool isMovingRight;
    bool isMovingLeft;
    bool isJumping;
    bool flag = false;
    bool flagstandup = true;
    bool fallflag = false;
    bool forward=false;
    bool backward=false;
    bool isGrounded;
   


    public float speed = 10f;
    public float gravity = -9.81f;
    public float jumpHeight = 3;
    Vector3 velocity;
    
    public Transform groundCheck;
    public float groundDistance = 1;
    public LayerMask groundMask;

    public float orginalAceleration = 2f;
    public float acceleration = 0.1f;
    public float currentReach;
    public GameManager gameManager;
    public float dragFactor = 5f;
    
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        ostacle = hit.collider.gameObject;
        if (flagstandup)
        {
            if (hit.collider.gameObject.tag == "forward")
            {
                acceleration = orginalAceleration;
                forward = true;
                Vector3 direction = new Vector3(currentMovementInput.x, 0f, currentMovementInput.y).normalized;
                transform.rotation = Quaternion.LookRotation(direction);
                handleAnimation();
                
                Debug.Log("forward");
            }
            if (hit.collider.gameObject.tag == "back")
            {
                acceleration = orginalAceleration;
                backward = true;
                Vector3 direction = new Vector3(currentMovementInput.x, 0f, currentMovementInput.y).normalized;
                transform.rotation = Quaternion.LookRotation(direction);
                Debug.Log("backward");
            }
            //Debug.Log(flagstandup);
            
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
            flagstandup = true;
            Debug.Log("Press");
            
            //animator.Play("New Animation");
        }
        else
        {
            animator.SetBool("isRunning", false);
            acceleration = orginalAceleration;
            animator.SetBool("isPressing", false);
        }
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("falling_backward") && !animator.GetCurrentAnimatorStateInfo(0).IsName("falling_forward"))
        {
            flagstandup = true;
            acceleration = orginalAceleration;
            Vector3 direction = new Vector3(0, 0f, 1).normalized;
            transform.rotation = Quaternion.LookRotation(direction);
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
            acceleration += acceleration * Time.deltaTime;
            if(acceleration > 20)
            {
                acceleration = 20;
                animator.SetBool("isRunning", true);
                
            }
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
            backward = false;
            flagstandup = false;
            animator.Play("falling_backward");
            animator.Play("standingup");
            //fallflag = true;
        }
        if (forward == true )
        {
            //animator.SetBool("forward", true);
            forward = false;
            flagstandup = false;
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
        

        characterController.Move(currentMovement*Time.deltaTime*(speed+acceleration));
        Debug.Log(acceleration);

        //transform.Rotate(Vector3.up, 20 * Time.deltaTime);
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
