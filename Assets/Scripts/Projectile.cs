using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 100;
    public int speed = 30;
    public float lifeTime = 3f;
    private float _timeAlive = 0;

    private Rigidbody _rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.velocity = transform.forward * speed;
    }

    // Update is called once per frame
    void Update()
    {
        _timeAlive += Time.deltaTime;
        if (_timeAlive > lifeTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().updateHealth(-damage);
            Destroy(gameObject);
        }
    }
}
