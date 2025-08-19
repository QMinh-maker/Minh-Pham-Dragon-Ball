using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
    [SerializeField] private float flySpeed = 10f;        // tốc độ bay
    [SerializeField] private float movementSmoothing = .05f;
    [SerializeField] private Transform enemy;             // tham chiếu tới kẻ địch

    private Rigidbody2D rb;
    private Vector3 velocity = Vector3.zero;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; // tắt trọng lực để không tự rơi
    }

    public void Move(float moveX, float moveY)
    {
        // bay theo input
        Vector3 targetVelocity = new Vector2(moveX * flySpeed, moveY * flySpeed);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, movementSmoothing);

        // luôn hướng về phía kẻ địch
        if (enemy != null)
        {
            if (enemy.position.x > transform.position.x)
                FaceRight();
            else
                FaceLeft();
        }
    }

    private void FaceRight()
    {
        Vector3 theScale = transform.localScale;
        theScale.x = Mathf.Abs(theScale.x);
        transform.localScale = theScale;
    }

    private void FaceLeft()
    {
        Vector3 theScale = transform.localScale;
        theScale.x = -Mathf.Abs(theScale.x);
        transform.localScale = theScale;
    }
}
