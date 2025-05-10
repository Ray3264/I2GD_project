using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WanderingAI : MonoBehaviour
{
    [SerializeField] GameObject fireballPrefab;
    private GameObject fireball;

    public float speed = 3.0f;
    public float obstacleRange = 5.0f;
    private bool isAlive;

    public float rotationSpeed = 100.0f;
    private Quaternion targetRotation;
    private bool isRotating = false;

    private void Start()
    {
        targetRotation = transform.rotation;
        isAlive = true;
    }
    void Update()
    {
        if (isAlive & !isRotating)
        {
            transform.Translate(0, 0, speed * Time.deltaTime);
        }

        if (isRotating)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            if (Quaternion.Angle(transform.rotation, targetRotation) < 1f)
            {
                isRotating = false;
            }
        }

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.SphereCast(ray, 0.75f, out hit))
        {
            GameObject hitObject = hit.transform.gameObject;
            if (hitObject.GetComponent<PlayerCharacter>())
            {
                if (fireball == null)
                {
                    fireball = Instantiate(fireballPrefab) as GameObject;
                    fireball.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
                    fireball.transform.rotation = transform.rotation;
                }
            }
            else if (hit.distance < obstacleRange)
            {
                float angle = Random.Range(-110, 110);
                targetRotation = Quaternion.Euler(0, transform.eulerAngles.y + angle, 0);
                isRotating = true;
            }
        }
    }
    public void SetAlive(bool alive)
    {
        isAlive = alive;
    }
}