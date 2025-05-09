using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class PlayerCharacter : MonoBehaviour
{
    private static int health;
    private Vector3 startPosition;
    void Start()
    {
        startPosition = transform.position;
        health = 5;
    }
    public void Hurt(int damage)
    {
        if (health > 0)
        {
            health -= damage;
            Debug.Log($"Health: {health}");
            if (health <= 0)
            {
                StartCoroutine(RestartLevel());
            }
        }
    }
    public static int getHealth() { return health; }
    private IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(2f); 
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
        health = 5;
        transform.position = startPosition;
    }
}
