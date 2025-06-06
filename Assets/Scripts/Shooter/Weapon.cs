using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private Camera cam;
    
    //  Shooting
    public bool isShooting, readyToShoot;
    private bool allowReset = true;
    public float shootingDelay = 2f;
    
    //  Burst
    public int bulletsPerBurst = 3;
    public int burstBulletsLeft;
    
    //  Spread
    public float spreadIntensity;
    
    //  Bullet
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletVelocity = 30;
    public float bulletLifeTime = 3f; // seconds
    
    private Animator animator;

    public enum ShootingMode
    {
        Single,
        Burst,
        Auto
    }

    public ShootingMode currentShootingMode;

    private void Awake()
    {
        readyToShoot = true;
        burstBulletsLeft = bulletsPerBurst;
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        cam = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        if (currentShootingMode == ShootingMode.Auto)
        {
            //  Holding Down Left Mouse Button
            isShooting = Input.GetKeyDown(KeyCode.Mouse0);
        }
        else if(currentShootingMode == ShootingMode.Single ||
                currentShootingMode == ShootingMode.Burst)
        {
            //  Clicking Left Mouse button Once
            isShooting = Input.GetKeyDown(KeyCode.Mouse0);
        }

        if (readyToShoot && isShooting)
        {
            burstBulletsLeft = bulletsPerBurst;
            FireWeapon();
        }
        // if (Input.GetKeyDown(KeyCode.Mouse0))
        // {
        //     FireWeapon();
        // }        
    }

    private void FireWeapon()
    {
        animator.SetTrigger("RECOIL");
        
        readyToShoot = false;
        
        Vector3 shootingDirection = CalculateDirectionAndSpread().normalized;
        
        //  Instantiate the bullet 
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
        
        //  Poiting the bullet to face the shooting direction
        bullet.transform.forward = shootingDirection;
        
        //  Shoot the bullet
        bullet.GetComponent<Rigidbody>().AddForce(bulletSpawn.forward.normalized * bulletVelocity, ForceMode.Impulse);
        
        //  Destroy the bullet after some time
        StartCoroutine(DestroyBulletAfterTime(bullet, bulletLifeTime));

        //  Checking if we are done shooting
        if (allowReset)
        {
            Invoke("ResetShot", shootingDelay); 
            allowReset = false;
        }
        
        // Burst Mode
        if (currentShootingMode == ShootingMode.Burst && burstBulletsLeft > 1) // we already shoot once before this check
        {
            burstBulletsLeft--;
            Invoke("FireWeapon", shootingDelay);
        }
    }

    private void ResetShot()
    {
        readyToShoot = true;
        allowReset = true;
    }

    public Vector3 CalculateDirectionAndSpread()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
        {
            //  Hitting Something
            targetPoint = hit.point;
        }
        else
        {
            //  Shooting at the air
            targetPoint = ray.GetPoint(100);
        }
        
        Vector3 direction = targetPoint - bulletSpawn.position;
        
        float x = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);
        float y = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);
        
        //  Returning the shooting direction and spread 
        return direction + new Vector3(x, y, 0);
    } 

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }
    
    private void OnGUI()
    {
        int size = 12;
        float posX = cam.pixelWidth / 2 - size / 4;
        float posY = cam.pixelHeight / 2 - size / 2;
        GUI.Label(new Rect(posX, posY, size, size), "*");
    }
}
