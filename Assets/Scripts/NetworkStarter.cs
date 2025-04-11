using Unity.Netcode;
using UnityEngine;

public class NetworkStarter : MonoBehaviour
{
    void OnGUI()
    {
         GUI.Label(new Rect(10, 160, 300, 30), $"IsServer: {NetworkManager.Singleton.IsServer}, IsClient: {NetworkManager.Singleton.IsClient}, IsHost: {NetworkManager.Singleton.IsHost}");
        if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
        {
            if (GUI.Button(new Rect(10, 10, 150, 40), "Start Host"))
            {
                NetworkManager.Singleton.StartHost();
            }
            if (GUI.Button(new Rect(10, 60, 150, 40), "Start Client"))
            {
                NetworkManager.Singleton.StartClient();
            }
            if (GUI.Button(new Rect(10, 110, 150, 40), "Start Server"))
            {
                NetworkManager.Singleton.StartServer();
            }
        }
    }
}
