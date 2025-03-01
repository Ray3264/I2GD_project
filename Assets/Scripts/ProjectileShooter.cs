using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    public Camera cam;
    public GameObject projectilePrefab;
    public float coolDown = 0.8f;
    private float cooolDownCounter = 0f;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (cooolDownCounter < coolDown)
        {
            cooolDownCounter += Time.deltaTime;
        }
        else if (Input.GetMouseButtonDown(0))
        {
            Vector3 point = new Vector3(cam.pixelWidth / 2, cam.pixelHeight / 2, 0);
            Shoot();
            cooolDownCounter = 0f;
        }
    }
    private void Shoot()
    {
        var proj = Instantiate(projectilePrefab);
        proj.transform.position = cam.transform.position + cam.transform.forward * 3f;
        proj.transform.rotation = cam.transform.rotation;

    }
    private void OnGUI()
    {
        int size = 12;
        float posX = cam.pixelWidth / 2 - size / 4;
        float posY = cam.pixelHeight / 2 - size / 2;
        GUI.Label(new Rect(posX, posY, size, size), "*");
    }
}
