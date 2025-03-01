using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereMovement : MonoBehaviour
{
    public float radius = 5f;
    public float speed = 2f;
    public float verticalAmplitude = 1f;
    public float verticalFrequency = 1f;
    public float sizeAmplitude = 0.5f;
    public float sizeFrequency = 1f;
    public Vector3 rotationAxis = new Vector3(0.2f, 1f, 0);
    public float rotationSpeed = 30f;
    private float angle = 0f;
    private Vector3 startPosition;
    private Vector3 initialScale;
    void Start()
    {
        startPosition = transform.position;
        initialScale = transform.localScale;
    }
    private void Update()
    {
        angle += Time.deltaTime * speed;
        float x = Mathf.Cos(angle) * radius;
        float z = Mathf.Sin(angle) * radius;
        float y = Mathf.Sin(Time.time * verticalFrequency) * verticalAmplitude;

        transform.position = startPosition + new Vector3(x, y, z);

        float scale = 1 + Mathf.Sin(Time.time * sizeFrequency) * sizeAmplitude;
        transform.localScale = initialScale * scale;

        transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime);
    }
}
