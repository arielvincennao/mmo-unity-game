using TMPro;
using UnityEngine;

public class PlayerNameDisplay : MonoBehaviour
{
    public TextMeshProUGUI nameText;

    public void SetName(string playerName)
    {
        nameText.text = playerName;
    }

    void LateUpdate()
    {
        if (Camera.main == null) return;

        Vector3 cameraPosition = Camera.main.transform.position;
        Vector3 lookDirection = transform.position - cameraPosition;
        lookDirection.y = 0; // No girar en el eje vertical

        if (lookDirection.sqrMagnitude > 0.001f)
        {
            transform.rotation = Quaternion.LookRotation(lookDirection);
            // transform.Rotate(0, 180f, 0); // Descomentar si el texto se ve dado vuelta
        }
    }
}
