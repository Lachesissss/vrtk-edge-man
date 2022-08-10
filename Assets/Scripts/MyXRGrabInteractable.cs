using Mirror;
using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;

public class MyXRGrabInteractable : XRGrabInteractable
{
    public NetworkIdentity identity;
    private GameObject[] players;
    private XROrigin rig;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    bool MyEquals(Vector3 pos1, Vector3 pos2)
    {
        var x1 = Math.Round(pos1.x, 2) * 100;
        var y1 = Math.Round(pos1.y, 2) * 100;
        var z1 = Math.Round(pos1.z, 2) * 100;

        var x2 = Math.Round(pos2.x, 2) * 100;
        var y2 = Math.Round(pos2.y, 2) * 100;
        var z2 = Math.Round(pos2.z, 2) * 100;

        Debug.Log("(" + x1.ToString() + "," + y1.ToString() + "," + z1.ToString() + ")"
            + "(" + x2.ToString() + "," + y2.ToString() + "," + z2.ToString() + ")");
        bool equalx = Math.Abs(x1 - x2) <= 3;
        bool equaly = Math.Abs(y1 - y2) <= 3;
        bool equalz = Math.Abs(z1 - z2) <= 3;
        bool isEqual = equalx && equaly && equalz;

        return isEqual;
    }

    NetworkPlayer FindPlayer(GameObject[] players, XROrigin rig)
    {
        NetworkPlayer networkPlayer = null;
        Transform leftHandRigTrans = rig.transform.Find("Camera Offset/LeftHand Controller");
        Transform rightHandRigTrans = rig.transform.Find("Camera Offset/RightHand Controller");

        foreach (var player in players)
        {
            Transform leftHand = player.transform.Find("LeftHand");
            Transform rightHand = player.transform.Find("RightHand");
            if (
                MyEquals(leftHand.position, leftHandRigTrans.position) &&
                MyEquals(rightHand.position, rightHandRigTrans.position)
                )
            {
                networkPlayer = player.GetComponent<NetworkPlayer>();
                Debug.Log("player found.");
                break;
            }
        }
        return networkPlayer;
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        rig = FindObjectOfType<XROrigin>();
        players = GameObject.FindGameObjectsWithTag("Player");
        var player = FindPlayer(players, rig);
        if (player != null)
        {
            player.OnSelectGrabbable(identity);
        }
        else
        {
            Debug.Log("player == null");
        }
        base.OnSelectEntered(args);
    }
}
