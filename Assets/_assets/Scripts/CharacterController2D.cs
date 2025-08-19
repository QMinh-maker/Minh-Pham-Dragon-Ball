using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
    [SerializeField] private float m_FlyForce = 10f;          // lực bay / di chuyển
    [SerializeField] private float m_MovementSmoothing = .05f;
    [SerializeField] private Transform enemy;                 // tham chiếu tới kẻ địch
    private Rigidbody2D m_Rigidbody2D;
    private Vector3 m_Velocity = Vector3.zero;

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Move(float moveX, float moveY)
    {
        // Cho phép bay tự do: moveX và moveY do input quyết định
        Vector3 targetVelocity = new Vector2(moveX * m_FlyForce, moveY * m_FlyForce);
        m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

        // Luôn hướng về phía kẻ địch
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
