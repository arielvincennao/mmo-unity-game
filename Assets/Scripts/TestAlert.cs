using UnityEngine;
//Script para testear el funcionamiento de X boton en consola
public class TestAlert : MonoBehaviour
{
    public void ShowAlert(string message)
    {
        Debug.Log("Show > " + message);
    }
}

