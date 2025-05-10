using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Rigidbody2D rb;
    private Animator animator;
    [SerializeField] float speed;
    Vector2[] directions = { Vector2.up, Vector2.right, Vector2.down, Vector2.left };
    int directionIndex = 0;
    [SerializeField] Vector2 currentDir;
    [SerializeField] float rayDistance;
    [SerializeField] LayerMask rayLayer;

    // Animation parameters
    private bool isMoving = false;
    private static readonly int IsWalking = Animator.StringToHash("isWalking");

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentDir = directions[directionIndex];
        UpdateAnimation();
    }

    // Update is called once per frame
    void Update()
    {
        // Raycast
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, currentDir, rayDistance, rayLayer);
        Vector3 endPoint = currentDir * rayDistance;
        Debug.DrawLine(transform.position, transform.position + endPoint, Color.green);

        if (hit2D.collider != null)
        {
            ChangeDirection();
        }

        // Set animation direction based on movement direction
        SetAnimationDirection();
    }

    void SetAnimationDirection()
    {
        // Update animation parameters based on direction
        if (currentDir == Vector2.up)
        {
            animator.SetInteger("Direction", 0); // Up
        }
        else if (currentDir == Vector2.right)
        {
            animator.SetInteger("Direction", 1); // Right
        }
        else if (currentDir == Vector2.down)
        {
            animator.SetInteger("Direction", 2); // Down
        }
        else if (currentDir == Vector2.left)
        {
            animator.SetInteger("Direction", 3); // Left
        }
    }

    public void ChangeDirection()
    {
        directionIndex += Random.Range(0, 2) * 2 - 1;
        int clampedIndex = directionIndex % directions.Length;
        if (clampedIndex < 0)
        {
            clampedIndex = directions.Length + clampedIndex;
        }

        rb.velocity = Vector2.zero;
        currentDir = directions[clampedIndex];

        // Update animation after changing direction
        UpdateAnimation();
    }

    public void StopMovement()
    {
        rb.velocity = Vector2.zero;
        isMoving = false;
        UpdateAnimation();
    }

    void StartMovement()
    {
        isMoving = true;
        UpdateAnimation();
    }

    void UpdateAnimation()
    {
        // Update the isWalking parameter in the animator
        animator.SetBool(IsWalking, isMoving);
    }

    void FixedUpdate()
    {
        rb.velocity = currentDir.normalized * speed * Time.deltaTime;

        // Check if we're moving or not based on velocity
        bool wasMoving = isMoving;
        isMoving = rb.velocity.sqrMagnitude > 0.01f;

        // Only update the animator if the moving state changed
        if (wasMoving != isMoving)
        {
            UpdateAnimation();
        }
    }
}
