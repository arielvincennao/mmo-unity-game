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

    [Header("Configuración")]
    public string playerName = "Jugador1"; // nombre del usuario
    public int autoScrollThreshold = 5;    // umbral mínimo de mensajes para activar el auto scroll

    void Start()
    {
        inputField.onSubmit.AddListener(SendMessage); // Enviar con Enter
    }

    public void OnSendButtonClicked()
    {
        SendMessage(inputField.text);
    }

    void SendMessage(string message)
    {
        if (string.IsNullOrWhiteSpace(message)) return;

        // Instanciar el mensaje en el contenedor
        var newMsg = Instantiate(messagePrefab, messageContainer);
        newMsg.text = $"[{playerName}]: {message}";

        // Limpiar y reactivar el campo de entrada
        inputField.text = "";
        inputField.ActivateInputField();

        // Si la cantidad de mensajes supera el umbral, auto-scroll al final
        if (messageContainer.childCount >= autoScrollThreshold)
        {
            Canvas.ForceUpdateCanvases();
            scrollRect.verticalNormalizedPosition = 0f; 
            Canvas.ForceUpdateCanvases();
        }
    }
}
