using UnityEngine;

public class FanController : MonoBehaviour
{
    public float maxSpeed = 200f;
    public float acceleration = 50f;
    private float currentSpeed = 0f;
    private bool isActive = false;

    void Update()
    {
        // Плавное ускорение и замедление
        if (isActive)
            currentSpeed = Mathf.MoveTowards(currentSpeed, maxSpeed, acceleration * Time.deltaTime);
        else
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0, acceleration * Time.deltaTime);
        
        transform.Rotate(0, 0, currentSpeed * Time.deltaTime);
    }

    public void ToggleFan()
    {
        isActive = !isActive;
    }
}
