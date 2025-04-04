using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    public Transform target;                 // El personaje a seguir
    public float distance = 5f;              // Distancia desde el personaje
    public float sensitivity = 3f;           // Sensibilidad de movimiento
    public float minY = -20f, maxY = 80f;    // Límites verticales
    public Vector3 offset = new Vector3(0, 1.25f, 0); // Altura de la cámara

    private float rotX = 0f;
    private float rotY = 15f;

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        rotX = angles.y;
        rotY = angles.x;

        if (target == null)
        {
            Debug.LogError("OrbitCamera: No target assigned!");
        }
    }

    void LateUpdate()
    {
        if (target == null) return;

        float inputX = 0f, inputY = 0f;
        bool isRotating = false;

#if UNITY_EDITOR || UNITY_STANDALONE
        if (Input.GetMouseButton(1)) // click derecho
        {
            isRotating = true;
            inputX = Input.GetAxis("Mouse X") * sensitivity;
            inputY = Input.GetAxis("Mouse Y") * sensitivity;
        }
#elif UNITY_ANDROID || UNITY_IOS
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                isRotating = true;
                inputX = touch.deltaPosition.x * sensitivity * 0.02f;
                inputY = touch.deltaPosition.y * sensitivity * 0.02f;
            }
        }
#endif

        if (isRotating)
        {
            rotX += inputX;
            rotY -= inputY;
            rotY = Mathf.Clamp(rotY, minY, maxY);
        }

        Quaternion rotation = Quaternion.Euler(rotY, rotX, 0);
        Vector3 focusPoint = target.position + offset; // centro real de rotación
        Vector3 cameraPosition = focusPoint - (rotation * Vector3.forward * distance);

        transform.position = cameraPosition;
        transform.LookAt(focusPoint);
    }
}
