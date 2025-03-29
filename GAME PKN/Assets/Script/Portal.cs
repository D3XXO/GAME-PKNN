using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private Transform destination; // tujuan
    public bool isOrange;
    public float distance = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        if (isOrange == false)
        {
            destination = GameObject.FindGameObjectWithTag("Orange portal").GetComponent<Transform>();
        }
        else
        {
            destination = GameObject.FindGameObjectWithTag("Blue portal").GetComponent<Transform>();
        }

    }

    /// <summary>
    /// Sent when another object enters a trigger collider attatched to this
    /// object (2D Physics only)
    /// </summary>
    /// <param name="other"> </param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (Vector2.Distance(transform.position, other.transform.position) > distance)
        {
            other.transform.position = new Vector2(destination.position.x, destination.position.y);
        }

    }
}
