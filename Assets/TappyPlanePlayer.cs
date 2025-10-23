using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TappyPlanePlayer : MonoBehaviour
{
    [SerializeField] float       jumpForce;
    private          Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = Vector2.up * jumpForce;
        }
    }
}