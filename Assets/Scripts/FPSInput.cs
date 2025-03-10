using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSInput : MonoBehaviour
{
    public float speed = 6.0f;
    public float gravity = -9.8f;
    public float sprintMultiplier = 2f;
    public float crouchMultiplier = 0.5f;
    private float defaultHeight;
    public float jumpForce = 7f;
    private Vector3 movementVert;

    private CharacterController charController;
    
    private void Start()
    {
        charController = GetComponent<CharacterController>();
        defaultHeight = charController.height;
    }

    private void Update()
    {
        float moveSpeed = Input.GetKey(KeyCode.LeftShift) ? speed * sprintMultiplier : speed;
        moveSpeed = Input.GetKey(KeyCode.C) ? moveSpeed * crouchMultiplier : moveSpeed;
        float deltaX = Input.GetAxis("Horizontal") * moveSpeed;
        float deltaZ = Input.GetAxis("Vertical") * moveSpeed;
        float dt = Time.deltaTime;
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, moveSpeed);
        
        // Прыжок
        if (charController.isGrounded)
        {
            movementVert.y /= 3;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                movementVert.y = jumpForce;
            }
        }
        else
        {
            movementVert.y += gravity * dt;  
        }
        
        // Приседание
        if (Input.GetKey(KeyCode.C))
        {
            charController.height = defaultHeight / 2;
        }
        else
        {
            charController.height = defaultHeight;
        }
        
        movement *= dt;
        movement = transform.TransformDirection(movement);
        charController.Move(movement + movementVert * dt);
    }
    
    public void JumpFromPad(float force)
    {
        movementVert.y = force;
    }
    
}
