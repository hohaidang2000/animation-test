using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationAndMovementController : MonoBehaviour
{
    // Start is called before the first frame update
    PlayerInput playerInput;
    CharacterController characterController;

    Vector2 currentMovementInput;
    Vector3 currentMovement;
    bool isMovementPressed;

    void Awake()
    {
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();

        playerInput.CharacterControl.Movement.started += context => {
            currentMovementInput = context.ReadValue<Vector2>();
            currentMovement.x = currentMovementInput.x;
            currentMovement.z = currentMovementInput.y;
            isMovementPressed = currentMovementInput.x != 0 | currentMovementInput.y != 0;
            Debug.Log(context.ReadValue<Vector2>()); 
        }; 
    }

    // Update is called once per frame
    void Update()
    {
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
