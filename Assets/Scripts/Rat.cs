using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float speed;

    private Rigidbody2D rb;
    private int waypointIndex = 0;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, waypoints[waypointIndex].position) < 0.5f)
            waypointIndex++;
        if (waypointIndex == waypoints.Length)
            waypointIndex = 0;

        rb.velocity = new Vector2(speed * transform.position.x < waypoints[waypointIndex].position.x ? 1f : -1f, 0f);
    }
}
