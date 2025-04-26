using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float jumpForce = 4f; // Сила прыжка

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FPSInput playerMovement = other.GetComponent<FPSInput>();
            playerMovement.JumpFromPad(jumpForce);
           
        }
    }
    
}