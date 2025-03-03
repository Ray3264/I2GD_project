using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSInput : MonoBehaviour
{
    public float speed = 6.0f;
    public float gravity = -9.8f;
    public float sprintMultiplier = 2f;

    private CharacterController charController;
    private void Start()
    {
        charController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        float moveSpeed = Input.GetKey(KeyCode.LeftShift) ? speed * sprintMultiplier : speed;
        float deltaX = Input.GetAxis("Horizontal") * moveSpeed;
        float deltaZ = Input.GetAxis("Vertical") * moveSpeed;
        float dt = Time.deltaTime;
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, moveSpeed);
        
        movement.y = gravity;

        movement *= dt;
        movement = transform.TransformDirection(movement);
        charController.Move(movement);
    }
}
