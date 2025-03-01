using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalRotation : MonoBehaviour
{
    public float spinSpeed = 100.0f;
    void Update()
    {
        float horizontalRot = Time.deltaTime * spinSpeed;
        transform.Rotate(0, horizontalRot, 0);
    }
}
