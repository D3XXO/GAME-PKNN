using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class EnemyMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] float speed;
    [SerializeField] float chaseSpeedMultiplier;
    [SerializeField] float rayDistance;
    [SerializeField] LayerMask rayLayer;
    [SerializeField] LayerMask wallLayer;

    [Header("Path Settings")]
    [SerializeField] Transform[] pathPoints;
    [SerializeField] float waypointThreshold = 0.1f;

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 currentDir;
    private int currentWaypoint = 0;
    private bool isChasing = false;
    private Transform player;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Start()
    {
        if (pathPoints.Length > 0)
        {
            currentDir = (pathPoints[currentWaypoint].position - transform.position).normalized;
        }
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        
        if (!isChasing && distanceToPlayer < rayDistance)
        {
            if (HasLineOfSightToPlayer())
            {
                StartChasing();
            }
        }
        else if (isChasing && distanceToPlayer > rayDistance * 1.5f)
        {
            StopChasing();
        }
        else if (isChasing && !HasLineOfSightToPlayer())
        {
            StopChasing();
        }

        if (!isChasing && pathPoints.Length > 0)
        {
            PathMovement();
        }

        if (animator != null)
        {
            UpdateAnimations();
        }
    }

    void PathMovement()
    {
        if (Vector2.Distance(transform.position, pathPoints[currentWaypoint].position) < waypointThreshold)
        {
            currentWaypoint = (currentWaypoint + 1) % pathPoints.Length;
            currentDir = (pathPoints[currentWaypoint].position - transform.position).normalized;
        }
        else
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, currentDir, rayDistance, rayLayer);
            Debug.DrawLine(transform.position, transform.position + (Vector3)currentDir * rayDistance, Color.green);

            if (hit.collider != null && !hit.collider.CompareTag("Player"))
            {
                currentWaypoint = (currentWaypoint + 1) % pathPoints.Length;
                currentDir = (pathPoints[currentWaypoint].position - transform.position).normalized;
            }
        }
    }

    bool HasLineOfSightToPlayer()
    {
        Vector2 directionToPlayer = (player.position - transform.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, rayDistance, wallLayer);
        
        Debug.DrawRay(transform.position, directionToPlayer * rayDistance, Color.red);
        
        return hit.collider == null || hit.collider.CompareTag("Player");
    }

    void StartChasing()
    {
        isChasing = true;
        if (animator != null)
        {
            animator.SetBool("IsChasing", true);
        }
    }

    void StopChasing()
    {
        isChasing = false;
        if (animator != null)
        {
            animator.SetBool("IsChasing", false);
        }
        // Return to nearest path point
        if (pathPoints.Length > 0)
        {
            currentWaypoint = FindNearestWaypoint();
            currentDir = (pathPoints[currentWaypoint].position - transform.position).normalized;
        }
    }

    int FindNearestWaypoint()
    {
        int nearest = 0;
        float minDistance = float.MaxValue;
        
        for (int i = 0; i < pathPoints.Length; i++)
        {
            float distance = Vector2.Distance(transform.position, pathPoints[i].position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = i;
            }
        }
        
        return nearest;
    }

    void UpdateAnimations()
    {
        // Update animation parameters based on movement
        animator.SetFloat("MoveX", currentDir.x);
        animator.SetFloat("MoveY", currentDir.y);
        animator.SetFloat("Speed", currentDir.magnitude);
    }

    void FixedUpdate()
    {
        Vector2 targetVelocity;

        if (isChasing)
        {
            currentDir = (player.position - transform.position).normalized;
            targetVelocity = currentDir * speed * chaseSpeedMultiplier * Time.deltaTime;
        }
        else
        {
            targetVelocity = currentDir * speed * Time.deltaTime;
        }

        rb.velocity = targetVelocity;
    }

    public void StopMovement()
    {
        rb.velocity = Vector2.zero;
        if (animator != null)
        {
            animator.SetFloat("Speed", 0);
        }
    }
}