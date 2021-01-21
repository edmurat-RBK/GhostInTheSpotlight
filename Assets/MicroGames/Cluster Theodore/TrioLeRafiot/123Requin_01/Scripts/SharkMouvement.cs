using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkMouvement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = Vector2.right * speed * Time.fixedDeltaTime;
    }
}
