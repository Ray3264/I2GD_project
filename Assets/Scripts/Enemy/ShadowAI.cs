using UnityEngine;

public class ShadowAI : MonoBehaviour
{
    public float speed = 3.0f;
    public float detectionRange = 10f;
    public float rotationSpeed = 5.0f;
    public float damageInterval = 0.5f;
    public float viewAngle = 60f; // Угол обзора игрока, при котором тень замирает
    
    private Transform player;
    private Transform playerCamera; // Камера игрока для определения направления взгляда
    private bool isChasing;
    private bool isPlayerLooking; // Игрок смотрит на тень
    private float originalY;
    private float lastDamageTime;
    private Animator animator;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        playerCamera = Camera.main.transform; // Получаем главную камеру (глаза игрока)
        originalY = transform.position.y;
        animator = GetComponent<Animator>();
        
        if (animator == null)
        {
            Debug.LogWarning("Animator component not found on " + gameObject.name);
        }
    }

    void Update()
    {
        if (player != null)
        {
            CheckIfPlayerLooking(); // Проверяем, смотрит ли игрок на тень

            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer < detectionRange && !isPlayerLooking)
            {
                isChasing = true;
            }
            else
            {
                isChasing = false;
            }

            // Обновляем аниматор
            if (animator != null)
            {
                animator.SetFloat("speed", isChasing ? speed : 0f);
                animator.SetBool("isFrozen", isPlayerLooking);
            }

            if (isChasing && !isPlayerLooking)
            {
                Vector3 targetPosition = new Vector3(player.position.x, originalY, player.position.z);
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

                Vector3 direction = (targetPosition - transform.position).normalized;
                if (direction != Vector3.zero)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(direction);
                    targetRotation.eulerAngles = new Vector3(0, targetRotation.eulerAngles.y, 0);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                }
            }
        }
    }

    void CheckIfPlayerLooking()
    {
        Vector3 directionToShadow = transform.position - playerCamera.position;
        float angle = Vector3.Angle(playerCamera.forward, directionToShadow);

        // Проверяем угол и наличие препятствий
        isPlayerLooking =
            angle < viewAngle; //&& !Physics.Raycast(playerCamera.position, directionToShadow, directionToShadow.magnitude);
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && Time.time > lastDamageTime + damageInterval && !isPlayerLooking)
        {
            PlayerCharacter playerScript = collision.gameObject.GetComponent<PlayerCharacter>();
            if (playerScript != null)
            {
                playerScript.Hurt(1);
                lastDamageTime = Time.time;
                
                if (animator != null)
                {
                    animator.SetTrigger("attack");
                }
            }
        }
    }
}