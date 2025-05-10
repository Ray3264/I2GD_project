using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AdvancedShooterAI : MonoBehaviour
{
    [Header("Combat Settings")]
    public GameObject projectilePrefab;
    public float attackRange = 15f;
    public float visionRange = 20f;
    public float attackCooldown = 2f;
    public float projectileSpeed = 10f;
    
    [Header("Movement Settings")]
    public float patrolSpeed = 3f;
    public float chaseSpeed = 5f;
    public float waypointWaitTime = 2f;
    public Transform[] patrolWaypoints;

    private NavMeshAgent agent;
    private Transform player;
    private float lastAttackTime;
    private int currentWaypointIndex;
    private float waypointReachedTime;
    private bool isPlayerVisible;
    private Vector3 lastKnownPlayerPosition;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (!agent.isOnNavMesh)
        {
            Debug.LogError($"{name} is not on NavMesh! Please check NavMesh baking.", gameObject);
            enabled = false;
            return;
        }
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent.speed = patrolSpeed;
        currentWaypointIndex = 0;
        
        if (patrolWaypoints.Length > 0)
        {
            agent.SetDestination(patrolWaypoints[currentWaypointIndex].position);
        }
    }

    void Update()
    {
        CheckPlayerVisibility();
        
        if (isPlayerVisible)
        {
            EngagePlayer();
        }
        else if (Time.time - waypointReachedTime > waypointWaitTime)
        {
            Patrol();
        }
    }

    void CheckPlayerVisibility()
    {
        Vector3 directionToPlayer = player.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;
        
        // Проверка расстояния
        if (distanceToPlayer > visionRange)
        {
            isPlayerVisible = false;
            return;
        }

        // Проверка линии видимости
        RaycastHit hit;
        if (Physics.Raycast(transform.position, directionToPlayer.normalized, out hit, visionRange))
        {
            isPlayerVisible = hit.collider.CompareTag("Player");
            if (isPlayerVisible)
            {
                lastKnownPlayerPosition = player.position;
            }
        }
    }

    void EngagePlayer()
    {
        if (!GetComponent<NavMeshAgent>().isOnNavMesh) {
            Debug.LogError("Враг не на NavMesh!", gameObject);
        }

        // Остановка агента при атаке
        if (agent.isOnNavMesh)
        {
            agent.isStopped = true;
        }
    
        // Поворот к игроку
        Vector3 lookDirection = (player.position - transform.position).normalized;
        lookDirection.y = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, 
            Quaternion.LookRotation(lookDirection), 
            Time.deltaTime * 5f);

        // Атака
        if (Vector3.Distance(transform.position, player.position) <= attackRange && 
            Time.time - lastAttackTime > attackCooldown)
        {
            Attack();
            lastAttackTime = Time.time;
        }
    }

    void Attack()
    {
        Vector3 spawnPosition = transform.position + transform.forward * 1.5f + Vector3.up * 0.75f;
    
        GameObject projectile = Instantiate(
            projectilePrefab,
            spawnPosition,  // Новая позиция с смещением по Y
            transform.rotation
        );
    
        // Задаём скорость снаряда
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 aimDirection = (player.position - transform.position).normalized;
            rb.velocity = aimDirection * projectileSpeed;
        }
    
        Destroy(projectile, 5f);
    }

    void Patrol()
    {
        if (patrolWaypoints.Length == 0 || !agent.isOnNavMesh) 
            return;

        agent.isStopped = false;
        agent.speed = patrolSpeed;
    
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            if (Time.time - waypointReachedTime > waypointWaitTime)
            {
                currentWaypointIndex = (currentWaypointIndex + 1) % patrolWaypoints.Length;
                agent.SetDestination(patrolWaypoints[currentWaypointIndex].position);
                waypointReachedTime = Time.time;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        // Визуализация радиуса атаки и видимости
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRange);
    }
}