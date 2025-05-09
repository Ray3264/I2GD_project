using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


public class Player : MonoBehaviour
{
    public Vector3 Position { get; set; }
    private int Health;
    private float timer;
    private bool isAlive; // Indicates if the player is alive

    public void UpdateHealth(int delta)
    {
        Health += delta;
        if (Health <= 0)
        {
            Health = 0;
            isAlive = false;
            Debug.Log("Player died");
            
        }
    }
    

    void Start()
    {
        timer = 0f;
        Position = transform.position;
        Health = 100;
        isAlive = true;
    }
    void Update() 
    {
        if (!isAlive) {return;}
        timer += Time.deltaTime;
        if (timer >= 2f)
        {
            timer = 0f;

            // Change player's position to a random value
            Vector3 randomPosition = new Vector3(
                Random.Range(-25f, 25f),
                2,
                Random.Range(-25f, 25f)
            );
            this.Position = randomPosition;
            this.transform.position = randomPosition;
            Debug.Log("New Position: " + this.Position);

            // Decrease health by a random value
            int randomHealthDelta = Random.Range(-20, -1); // Random negative value
            this.UpdateHealth(randomHealthDelta);
        }
    }
}
