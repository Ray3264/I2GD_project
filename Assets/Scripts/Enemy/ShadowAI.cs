// using UnityEngine;
// public class ShadowAI : MonoBehaviour
// {
//     public float speed = 3.0f;
//     public float obstacleRange = 5.0f;
//
//     public float rotationSpeed = 100.0f;
//     private Quaternion targetRotation;
//     
//     private Transform player;
//     public float detectionRange = 10f;  // Дистанция, с которой тень замечает игрока
//     private bool isChasing;     // Флаг преследования
//
//     private void Start()
//     {
//         player = GameObject.FindGameObjectWithTag("Player")?.transform;
//         //targetRotation = transform.rotation;
//         
//     }
//     void Update()
//     {
//         if (player != null)
//         {
//             float distanceToPlayer = Vector3.Distance(transform.position, player.position);
//
//             // Если игрок в зоне обнаружения, тень начинает преследование
//             if (distanceToPlayer < detectionRange)
//             {
//                 isChasing = true;
//             }
//
//             if (isChasing)
//             {
//                 // Двигаемся к игроку
//                 transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
//
//                 // Поворачиваемся лицом к игроку
//                 transform.LookAt(player);
//             }
//         }
//     }
//     void OnTriggerEnter(Collider other)
//     {
//         if (other.CompareTag("Player"))
//         {
//             PlayerCharacter playerScript = other.GetComponent<PlayerCharacter>();
//             if (playerScript != null)
//             {
//                 playerScript.Hurt(1); // Наносим урон игроку
//             }
//
//             //gameObject.GetComponent<Enemy>().Die();
//             Destroy(gameObject); // Тень исчезает после удара
//         }
//     }
//     
// }
using UnityEngine;

public class ShadowAI : MonoBehaviour
{
    public float speed = 3.0f;
    public float detectionRange = 10f;
    public float rotationSpeed = 5.0f; // Более плавный поворот
    
    private Transform player;
    private bool isChasing;
    private float originalY; // Сохраняем исходную позицию по Y

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        originalY = transform.position.y; // Запоминаем начальную высоту
    }

    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer < detectionRange)
            {
                isChasing = true;
            }

            if (isChasing)
            {
                // Создаем новую позицию для движения, сохраняя исходную высоту
                Vector3 targetPosition = new Vector3(player.position.x, originalY, player.position.z);
                
                // Плавное перемещение с сохранением высоты
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

                // Плавный поворот к игроку (только по оси Y)
                Vector3 direction = (targetPosition - transform.position).normalized;
                if (direction != Vector3.zero)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(direction);
                    // Ограничиваем поворот только по оси Y
                    targetRotation.eulerAngles = new Vector3(0, targetRotation.eulerAngles.y, 0);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                }
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
                playerScript.Hurt(1);
            }
            Destroy(gameObject);
        }
    }
}