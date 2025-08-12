using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public float jumpHeight = 2f;      // Độ cao muốn nhảy
    public float gravityScale = 3f;    // Trọng lực nhân vật
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = gravityScale;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && Mathf.Abs(rb.velocity.y) < 0.01f)
        {
            float gravity = Mathf.Abs(Physics2D.gravity.y * rb.gravityScale);
            float jumpVelocity = Mathf.Sqrt(2 * gravity * jumpHeight);
            rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);
        }
    }
}
