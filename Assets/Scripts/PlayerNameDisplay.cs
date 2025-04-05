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
        if (Camera.main != null)
        {
            Vector3 dir = transform.position - Camera.main.transform.position;
            transform.rotation = Quaternion.LookRotation(dir);
        }
    }
}
