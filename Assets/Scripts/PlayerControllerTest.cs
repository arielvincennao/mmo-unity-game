using UnityEngine;
using Unity.Netcode;

public class PlayerControllerTest : NetworkBehaviour
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

    // 🔁 Variable de red para sincronizar la posición
    private NetworkVariable<Vector3> networkedPosition = new NetworkVariable<Vector3>(
        writePerm: NetworkVariableWritePermission.Owner
    );

    void Start()
    {
        // Solo el dueño usa la cámara principal
        if (IsOwner && cam == null)
            cam = Camera.main.transform;

        if (controller == null)
            controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (IsOwner)
        {
            // Movimiento local
            isGrounded = controller.isGrounded;

            if (isGrounded && velocity.y < 0)
                velocity.y = -2f;

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

            if (isGrounded && Input.GetButtonDown("Jump"))
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);

            // 🔁 Actualizar posición de red
            networkedPosition.Value = transform.position;
        }
        else
        {
            // 🔄 Reproducir posición sincronizada
            transform.position = networkedPosition.Value;
        }
    }
}
