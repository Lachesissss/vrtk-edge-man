using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PuckInitController : NetworkBehaviour
{
    public Transform puck;
    public Transform leftButton;
    public Transform rightButton;
    private NetworkIdentity AirHockeyIdentity;

    public static Vector3 leftPuckInitPos = new Vector3(0f, 1.27f, 1.7f);
    public static Vector3 rightPuckInitPos = new Vector3(0f, 1.27f, -1.7f);
    public static Quaternion puckInitRot = Quaternion.Euler(0, 0, 0);

    private Color myGreen = new Color(0.1859f, 0.9150f, 0.1007f, 1f);

    private void Start()
    {
        AirHockeyIdentity = GetComponent<NetworkIdentity>();
    }

    public void OnLeftButtonTouched()
    {
        GetAuthorityForIdentity(AirHockeyIdentity);
        PuckInitOnNetwork(leftPuckInitPos, puckInitRot);
        ChangeColor(leftButton, myGreen);
    }

    public void OnLeftButtonUnTouched()
    {
        ChangeColor(leftButton, Color.white);
    }

    public void OnRightButtonTouched()
    {
        GetAuthorityForIdentity(AirHockeyIdentity);
        PuckInitOnNetwork(rightPuckInitPos, puckInitRot);
        ChangeColor(rightButton, myGreen);
    }

    public void OnRightButtonUnTouched()
    {
        ChangeColor(rightButton, Color.white);
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

    private void GetAuthorityForIdentity(NetworkIdentity identity)
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("NetworkAvatar");
        var player = FindPlayer(players);
        if (player)
        {
            player.OnRequestAuthority(identity);
        }
        else
        {
            Debug.LogWarning("player == null");
        }
    }

    private void PuckInitOnNetwork(Vector3 pos, Quaternion rot)  // 这是用来让server端也移动的，可能不需要这次调用？
    {                                                            // 空气墙是否是因为撞到了server端没有正常移动的Puck？
        puck.localPosition = pos;
        puck.localRotation = rot;
        CmdServerInitPuck(pos, rot);
    }

    [Command]
    private void CmdServerInitPuck(Vector3 pos, Quaternion rot)
    {
        Debug.Log("Command: CmdServerInitPuck");
        RpcResetTrans();
        RpcInitPuck(pos, rot);
    }

    [ClientRpc]
    public void RpcResetTrans()
    {
        Debug.Log("ClientRpc: RpcResetTrans");
        GetComponent<NetworkTransform>().Reset();
        NetworkTransformChild[] child = GetComponents<NetworkTransformChild>();
        foreach (NetworkTransformChild c in child)
        {
            c.Reset();
        }
    }

    [ClientRpc]
    public void RpcInitPuck(Vector3 pos, Quaternion rot)
    {
        Debug.Log("ClientRpc: RpcInitPuck");
        puck.localPosition = pos;
        puck.localRotation = rot;
    }

    private void ChangeColor(Transform button, Color color)
    {
        MeshRenderer meshRenderer = button.GetChild(0).GetChild(0).GetComponent<MeshRenderer>();
        meshRenderer.material.color = color;
    }
}
