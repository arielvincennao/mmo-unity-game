using UnityEngine;

public class PlayerControllerTest : MonoBehaviour
{
    [Header("Movimiento")]
    public float speed = 5f;
    public float turnSmoothTime = 0.1f;

    [Header("Salto y Gravedad")]
    public float jumpHeight = 2f;
    public float gravity = -20f;

    [Header("Referencias")]
    public CharacterController controller;
    public Transform cam;

    private float turnSmoothVelocity;
    private Vector3 velocity;
    private bool isGrounded;

    void Start()
    {
        if (controller == null)
            controller = GetComponent<CharacterController>();

        if (cam == null)
            cam = Camera.main.transform;
    }

    void Update()
    {
        // Comprobar si estamos tocando el suelo
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f; // mantenernos pegados al suelo

        // Movimiento con cámara
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }

        // Saltar
        if (isGrounded && Input.GetButtonDown("Jump"))
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        // Aplicar gravedad
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}

