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

    NetworkPlayer FindPlayer(GameObject[] players, GameObject rig)
    {
        NetworkPlayer networkPlayer = null;
        Transform leftHandRigTrans = rig.transform.Find("Armature/Hips/Spine/Spine1/Spine2/LeftShoulder/LeftArm/LeftForeArm/LeftHand");
        Transform rightHandRigTrans = rig.transform.Find("Armature/Hips/Spine/Spine1/Spine2/RightShoulder/RightArm/RightForeArm/RightHand");

        foreach (var player in players)
        {
            Transform leftHand = player.transform.Find("Armature/Hips/Spine/Spine1/Spine2/LeftShoulder/LeftArm/LeftForeArm/LeftHand");
            Transform rightHand = player.transform.Find("Armature/Hips/Spine/Spine1/Spine2/RightShoulder/RightArm/RightForeArm/RightHand");
            if (
                MyEquals(leftHand.position, leftHandRigTrans.position) &&
                MyEquals(rightHand.position, rightHandRigTrans.position)
                )
            {
                networkPlayer = player.GetComponent<NetworkPlayer>();
                break;
            }
        }
        return networkPlayer;
    }

    NetworkAvatar FindPlayer2(GameObject[] players)
    {
        NetworkAvatar networkAvatar = null;
        foreach(var player in players)
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
        var player = FindPlayer2(players);
        if (player != null)
        {
            Debug.Log("player found");
            player.OnSelectGrabbable(identity);
        }
        else
        {
            Debug.Log("player == null");
        }
    }
}
