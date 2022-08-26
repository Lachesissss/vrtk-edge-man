using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.XR;


public class NetworkAvatar : NetworkBehaviour
{
    public Transform NetworkAvatarTransform;
    public Transform NetworkIKHead;
    public Transform NetworkIKLeftTarget;
    public Transform NetworkIKRightTarget;
    public Transform NetworkIKLeftFoot;
    public Transform NetworkIKRightFoot;
    public Animator NetworkAnimator;

    private Transform LocalAvatarTransform;
    private Transform LocalIKHead;
    private Transform LocalIKLeftTarget;
    private Transform LocalIKRightTarget;
    private Transform LocalIKLeftFoot;
    private Transform LocalIKRightFoot;
    private Animator LocalAnimator;

    private GameObject LocalAvatar;

    const int JOINTS_NUM = 20;
    private HandJoint[] LocalLeftHandJoints = new HandJoint[JOINTS_NUM];
    private HandJoint[] LocalRightHandJoints = new HandJoint[JOINTS_NUM];
    private HandJoint[] NetworkLeftHandJoints = new HandJoint[JOINTS_NUM];
    private HandJoint[] NetworkRightHandJoints = new HandJoint[JOINTS_NUM];



    public void MapTransform(Transform target, Transform rig)
    {
        if (rig != null)
        {
            target.SetPositionAndRotation(rig.position, rig.rotation);
        }
    }

    void Start()
    {
        LocalAvatar = GameObject.FindGameObjectWithTag("LocalAvatar");
        LocalAvatarTransform = LocalAvatar.transform;
        LocalAnimator = LocalAvatar.GetComponent<Animator>();
        LocalIKHead = LocalAvatarTransform.Find("Armature/Hips/Spine/Spine1/Spine2/Neck/Head");
        LocalIKLeftTarget = LocalAvatarTransform.Find("Armature/Hips/Spine/Spine1/Spine2/LeftShoulder/LeftArm/LeftForeArm/LeftHand");
        LocalIKRightTarget = LocalAvatarTransform.Find("Armature/Hips/Spine/Spine1/Spine2/RightShoulder/RightArm/RightForeArm/RightHand");
        LocalIKLeftFoot = LocalAvatarTransform.Find("Armature/Hips/LeftUpLeg/LeftLeg/LeftFoot");
        LocalIKRightFoot = LocalAvatarTransform.Find("Armature/Hips/RightUpLeg/RightLeg/RightFoot");

        Transform currentLeft = LocalIKLeftTarget;
        for (int i = 0; i < 5; i++)
        {
            currentLeft = LocalIKLeftTarget.GetChild(i);
            LocalLeftHandJoints[i * 4] = currentLeft.GetComponent<HandJoint>();
            for (int j = 1; j < 4; j++)
            {
                currentLeft = currentLeft.GetChild(0);
                int num = i * 4 + j;
                LocalLeftHandJoints[num] = currentLeft.GetComponent<HandJoint>();
            }

        }
        Transform currentRight = LocalIKRightTarget;
        for (int i = 0; i < 5; i++)
        {
            currentRight = LocalIKRightTarget.GetChild(i);
            LocalRightHandJoints[i * 4] = currentRight.GetComponent<HandJoint>();
            for (int j = 1; j < 4; j++)
            {
                currentRight = currentRight.GetChild(0);
                int num = i * 4 + j;
                LocalRightHandJoints[num] = currentRight.GetComponent<HandJoint>();
            }

        }

        NetworkAnimator = GetComponent<Animator>();
        Transform NetworkLeftHand = transform.Find("Armature/Hips/Spine/Spine1/Spine2/LeftShoulder/LeftArm/LeftForeArm/LeftHand");
        Transform NetworkRightHand = transform.Find("Armature/Hips/Spine/Spine1/Spine2/RightShoulder/RightArm/RightForeArm/RightHand");

        currentLeft = NetworkLeftHand;
        for (int i = 0; i < 5; i++)
        {
            currentLeft = NetworkLeftHand.GetChild(i);
            NetworkLeftHandJoints[i * 4] = currentLeft.GetComponent<HandJoint>();
            for (int j = 1; j < 4; j++)
            {
                currentLeft = currentLeft.GetChild(0);
                int num = i * 4 + j;
                NetworkLeftHandJoints[num] = currentLeft.GetComponent<HandJoint>();
            }

        }
        currentRight = NetworkRightHand;
        for (int i = 0; i < 5; i++)
        {
            currentRight = NetworkRightHand.GetChild(i);
            NetworkRightHandJoints[i * 4] = currentRight.GetComponent<HandJoint>();
            for (int j = 1; j < 4; j++)
            {
                currentRight = currentRight.GetChild(0);
                int num = i * 4 + j;
                NetworkRightHandJoints[num] = currentRight.GetComponent<HandJoint>();
            }

        }

        if (isLocalPlayer)
        {
            foreach (var item in GetComponentsInChildren<Renderer>())
            {
                item.enabled = false;
            }
        }
    }

    void LateUpdate()
    {
        if (isLocalPlayer)
        {
            MapTransform(NetworkIKHead, LocalIKHead);
            MapTransform(NetworkIKLeftTarget, LocalIKLeftTarget);
            MapTransform(NetworkIKRightTarget, LocalIKRightTarget);
            MapTransform(NetworkIKLeftFoot, LocalIKLeftFoot);
            MapTransform(NetworkIKRightFoot, LocalIKRightFoot);

            for (int i = 0; i < 20; i++)
            {
                MapTransform(NetworkLeftHandJoints[i].GetTransform(), LocalLeftHandJoints[i].GetTransform());
            }
            for (int i = 0; i < 20; i++)
            {
                MapTransform(NetworkRightHandJoints[i].GetTransform(), LocalRightHandJoints[i].GetTransform());
            }

            transform.position = LocalAvatar.transform.position;
            transform.forward = LocalAvatar.transform.forward;

            syncAnimator(NetworkAnimator, LocalAnimator);
        }
    }

    public void OnSelectGrabbable(NetworkIdentity networkIdentity)
    {
        if (networkIdentity != null && !networkIdentity.hasAuthority)
        {
            CmdRequestAuthority(networkIdentity);
        }
    }

    [Command]
    public void CmdRequestAuthority(NetworkIdentity identity)
    {
        ResetTrans();
        identity.RemoveClientAuthority();
        identity.AssignClientAuthority(connectionToClient);
        Debug.Log("Assign finished!");
    }

    [ClientRpc]
    public void ResetTrans()
    {
        GameObject.FindGameObjectWithTag("NetworkAvatar").GetComponent<NetworkTransform>().Reset();
        GameObject.FindGameObjectWithTag("NetworkAvatar").GetComponent<NetworkTransformChild>().Reset();
        GameObject.FindGameObjectWithTag("NetworkObject").GetComponent<NetworkTransform>().Reset();
    }

    private void syncAnimator(Animator NetworkAnimator, Animator LocalAnimator)  //将LocalAvatar动画同步到NetworkAvatar
    {
        NetworkAnimator.enabled = LocalAnimator.enabled;
        NetworkAnimator.SetBool("isWalking", LocalAnimator.GetBool("isWalking"));
        NetworkAnimator.SetFloat("animSpeed", LocalAnimator.GetFloat("animSpeed"));
        NetworkAnimator.SetFloat("Trigger_Left", LocalAnimator.GetFloat("Trigger_Left"));
        NetworkAnimator.SetFloat("Trigger_Right", LocalAnimator.GetFloat("Trigger_Right"));
        NetworkAnimator.SetFloat("Grip_Left", LocalAnimator.GetFloat("Grip_Left"));
        NetworkAnimator.SetFloat("Grip_Right", LocalAnimator.GetFloat("Grip_Right"));

    }

}
