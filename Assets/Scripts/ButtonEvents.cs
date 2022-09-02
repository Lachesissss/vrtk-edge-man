using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ButtonEvents : MonoBehaviour
{
    public enum ButtonId
    {
        LeftButton,
        RightButton,
    };

    public static Vector3 leftPuckInitPos = new Vector3(-14.5895996f, -1.25760007f, 49.4532013f);
    public static Vector3 rightPuckInitPos = new Vector3(-16.6760006f, -1.25760007f, 49.4840012f);
    public static Quaternion puckInitRot = Quaternion.Euler(0, 236.227707f, 0);

    private Color myGreen = new Color(0.1859f, 0.9150f, 0.1007f, 1f);

    public void OnLeftButtonOfIceBallGameTouched()
    {
        OnTouched(ButtonId.LeftButton);
    }

    public void OnLeftButtonOfIceBallGameUnTouched()
    {
        OnUntouched();
    }

    public void OnRightButtonOfIceBallGameTouched()
    {
        OnTouched(ButtonId.RightButton);
    }

    public void OnRightButtonOfIceBallGameUnTouched()
    {
        OnUntouched();
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

    private bool RequestButtonMove(GameObject puck, ButtonId buttonId)
    {

        GameObject[] players = GameObject.FindGameObjectsWithTag("NetworkAvatar");
        var player = FindPlayer(players);
        if (player)
        {
            bool hasAuthority = player.OnRequestButtonMove(puck, buttonId);
            return hasAuthority;
        }
        else
        {
            Debug.LogWarning("player == null");
        }
        return false;
    }

    private void OnTouched(ButtonId buttonId)
    {
        GameObject puck = GameObject.Find("Puck(Clone)");
        if (puck)
        {
            bool hasAuthority = RequestButtonMove(puck, buttonId);
            if (hasAuthority)
            {
                Rigidbody rb = puck.GetComponent<Rigidbody>();

                if (buttonId == ButtonId.LeftButton)
                {
                    rb.MovePosition(leftPuckInitPos);
                }
                else
                {
                    rb.MovePosition(rightPuckInitPos);
                }
                rb.MoveRotation(puckInitRot);
            }
        }
        else
        {
            Debug.Log("Puck == Null");
        }

        Transform button = transform.GetChild(0).GetChild(1);
        MeshRenderer meshRenderer = button.GetComponent<MeshRenderer>();
        meshRenderer.material.color = myGreen;
    }

    private void OnUntouched()
    {
        Transform button = transform.GetChild(0).GetChild(1);
        MeshRenderer meshRenderer = button.GetComponent<MeshRenderer>();
        meshRenderer.material.color = Color.white;
    }
}
