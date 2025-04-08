using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatManager : MonoBehaviour
{
    [Header("Referencias UI")]
    public TMP_InputField inputField;
    public Transform messageContainer;
    public TextMeshProUGUI messagePrefab;
    public ScrollRect scrollRect;

    [Header("Jugador")]
    public string playerName = "Jugador1"; // Podés cambiar esto dinámicamente luego

    void Start()
    {
        inputField.onSubmit.AddListener(SendMessage); // Enter para enviar
    }

    public void OnSendButtonClicked()
    {
        SendMessage(inputField.text);
    }

    void SendMessage(string message)
    {
        if (string.IsNullOrWhiteSpace(message)) return;

        var newMsg = Instantiate(messagePrefab, messageContainer);
        newMsg.text = $"[{playerName}]: {message}";

        // Limpiar input y volver a enfocarlo
        inputField.text = "";
        inputField.ActivateInputField();

        // Auto scroll al final
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 1f;
        Canvas.ForceUpdateCanvases();
    }
}
