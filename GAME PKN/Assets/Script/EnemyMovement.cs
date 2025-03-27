using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] float speed;

    Vector2 [] directions = {Vector2.up, Vector2.right, Vector2.down, Vector2.left};

    int directionIndex = 0;

    [SerializeField] Vector2 currentDir;
    [SerializeField] float rayDistance;
    [SerializeField] LayerMask rayLayer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentDir = directions[directionIndex];
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
    }

    public void ChangeDirection()
    {
        directionIndex += Random.Range(0,2) * 2 - 1;

        int clampedIndex = directionIndex % directions.Length;
        if (clampedIndex < 0)
        {
            clampedIndex = directions.Length + clampedIndex;
        }

        rb.velocity = Vector2.zero;
        currentDir = directions[clampedIndex];
    }

    public void StopMovement()
    {
    rb.velocity = Vector2.zero; // Berhenti sejenak sebelum mengubah arah
    }

    void FixedUpdate()
    {
    rb.velocity = currentDir.normalized * speed * Time.deltaTime; // Kecepatan konstan
    }
}
