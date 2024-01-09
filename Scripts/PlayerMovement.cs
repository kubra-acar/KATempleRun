using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    bool alive = true;

    public float speed = 5;
    public float jumpForce = 15;  // Yeni eklenen zıplama kuvveti

    [SerializeField] Rigidbody rb;

    float horizontalInput;
    [SerializeField] float horizontalMultiplier = 2;

    public float speedIncreasePerPoint = 0.1f;

    public GameManager gameManager;

    private void FixedUpdate()
    {
        if (!alive) return;

        Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;
        Vector3 horizontalMove = transform.right * horizontalInput * speed * Time.fixedDeltaTime * horizontalMultiplier;
        rb.MovePosition(rb.position + forwardMove + horizontalMove);
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();  // "Space" tuşuna basıldığında zıplama fonksiyonunu çağır
        }

        if (transform.position.y < -5)
        {
            Die();
        }
    }

    void Jump()
    {
        if (Mathf.Abs(rb.velocity.y) < 0.001f)  // Yalnızca yerdeyken zıplamaya izin ver
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    public void Die()
    {
        alive = false;
        gameManager.GameFinished();
    }
}
