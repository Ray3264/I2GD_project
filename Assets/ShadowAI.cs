using UnityEngine;
public class ShadowAI : MonoBehaviour
{
    public float speed = 3.0f;
    public float obstacleRange = 5.0f;

    public float rotationSpeed = 100.0f;
    private Quaternion targetRotation;
    
    private Transform player;
    public float detectionRange = 10f;  // Дистанция, с которой тень замечает игрока
    private bool isChasing;     // Флаг преследования

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        //targetRotation = transform.rotation;
        
    }
    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            // Если игрок в зоне обнаружения, тень начинает преследование
            if (distanceToPlayer < detectionRange)
            {
                isChasing = true;
            }

            if (isChasing)
            {
                // Двигаемся к игроку
                transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

                // Поворачиваемся лицом к игроку
                transform.LookAt(player);
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerCharacter playerScript = other.GetComponent<PlayerCharacter>();
            if (playerScript != null)
            {
                playerScript.Hurt(1); // Наносим урон игроку
            }

            Destroy(gameObject); // Тень исчезает после удара
        }
    }
    
}