using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(NetworkManager))]
public class NetworkConnector : MonoBehaviour
{
    private string serverAddress = "bupt.wanl5.top";
    NetworkManager manager;

    private void Awake()
    {
        manager = GetComponent<NetworkManager>();
        manager.networkAddress = serverAddress;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!NetworkClient.active)
        {
            manager.StartClient();
            Debug.Log("Connecting to server " + serverAddress + " via port " + Transport.activeTransport);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (NetworkClient.isConnected && !NetworkClient.ready)
        {
            Debug.Log("Connected to " + serverAddress + " via port " + Transport.activeTransport);
            NetworkClient.Ready();
            if (NetworkClient.localPlayer == null)
            {
                NetworkClient.AddPlayer();
            }
        }
    }
}
