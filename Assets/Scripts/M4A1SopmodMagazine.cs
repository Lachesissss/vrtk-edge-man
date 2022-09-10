using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class M4A1SopmodMagazine : MonoBehaviour
{
    public Transform follow;
    public NetworkIdentity identity;

    private bool isGrabbing = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isGrabbing)
        {
            transform.position = follow.position;
            transform.rotation = follow.rotation;
        }
    }

    public void OnMagazineGrabbed()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("NetworkAvatar");
        var player = FindPlayer(players);
        if (player)
        {
            player.OnRequestAuthority(identity);  //请求服务器将Authority分配给该player
        }
        else
        {
            Debug.LogWarning("player == null");
        }
        isGrabbing = true;
    }

    public void OnMagazineUnGrabbed()
    {
        isGrabbing = false;
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    private NetworkAvatar FindPlayer(GameObject[] players)
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
}
