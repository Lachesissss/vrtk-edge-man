using UnityEngine;
using System;
using Mirror;
using Tilia.Interactions.Interactables.Interactables;

public class MyInteractableFacade : InteractableFacade
{
    public NetworkIdentity identity;
    private GameObject rig;
    private GameObject[] players;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    NetworkAvatar FindPlayer(GameObject[] players)
    {
        NetworkAvatar networkAvatar = null;
        foreach (var player in players)
        {
            if (player.GetComponent<NetworkAvatar>().isLocalPlayer)
            {
                networkAvatar = player.GetComponent<NetworkAvatar>();
                break;
            }
        }
        return networkAvatar;
    }

    public void OnGrab()
    {
        rig = GameObject.FindGameObjectWithTag("LocalAvatar");
        players = GameObject.FindGameObjectsWithTag("NetworkAvatar");
        //var player = FindPlayer(players, rig);
        var player = FindPlayer(players);
        if (player != null)
        {
            Debug.Log("player found");
            player.OnRequestAuthority(identity);
        }
        else
        {
            Debug.Log("player == null");
        }
    }
}
