using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;

    public float speed = 6f;
    public float jumpForce = 7f;

    public Transform cameraTransform;

    // حط هنا Animator حق الشخصية
    public Animator animator;

    bool isGrounded = true;

    void Start()
    {
        rb.freezeRotation = true;
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = forward * vertical + right * horizontal;

        Vector3 velocity = moveDirection * speed;
        velocity.y = rb.linearVelocity.y;

        rb.linearVelocity = velocity;

        // تشغيل Run / Idle
        float moveAmount = new Vector3(horizontal, 0, vertical).magnitude;
        animator.SetFloat("Speed", moveAmount);
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            // تشغيل انميشن القفز
            animator.SetTrigger("Jump");

            isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isGrounded = true;
        }
    }
}