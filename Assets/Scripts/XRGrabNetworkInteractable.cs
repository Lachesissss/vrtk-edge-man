using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.XR.Interaction.Toolkit;

public class XRGrabNetworkInteractable : NetworkBehaviour
{
    private NetworkIdentity identity;
    private MyNetworkManager manager;
    // Start is called before the first frame update
    void Start()
    {
        identity = GetComponent<NetworkIdentity>();
        manager = GameObject.FindGameObjectWithTag("manager").GetComponent<MyNetworkManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
