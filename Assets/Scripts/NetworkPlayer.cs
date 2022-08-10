using Mirror;
using UnityEngine;
using UnityEngine.XR;
using Oculus.Avatar2;
using Oculus.Platform;
using Unity.XR.CoreUtils;
using System.Collections;

public class NetworkPlayer : NetworkBehaviour
{
    public Transform head;
    public Transform leftHand;
    public Transform rightHand;
    public Animator leftHandAnimator;
    public Animator rightHandAnimator;

    private GameObject rig = null;
    private Transform headRigTrans;
    private Transform leftHandRigTrans;
    private Transform rightHandRigTrans;

    // Start is called before the first frame update
    void Start()
    {
        while (rig == null)
        {
            rig = GameObject.FindGameObjectWithTag("camera");
        }
        headRigTrans = rig.transform.Find("Camera Offset/HeadCamera");
        leftHandRigTrans = rig.transform.Find("LeftHand");
        rightHandRigTrans = rig.transform.Find("RightHand");

        if (isLocalPlayer)
        {
            foreach (var item in GetComponentsInChildren<Renderer>())
            {
                item.enabled = false;
            }
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
        GameObject.FindGameObjectWithTag("Player").GetComponent<NetworkTransform>().Reset();
        GameObject.FindGameObjectWithTag("Player").GetComponent<NetworkTransformChild>().Reset();
        GameObject.FindGameObjectWithTag("NetworkObject").GetComponent<NetworkTransform>().Reset();
    }


    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer)
        {
            MapTransform(head, headRigTrans);
            MapTransform(leftHand, leftHandRigTrans);
            MapTransform(rightHand, rightHandRigTrans);
            UpdateHandAnimation(InputDevices.GetDeviceAtXRNode(XRNode.LeftHand), leftHandAnimator);
            UpdateHandAnimation(InputDevices.GetDeviceAtXRNode(XRNode.RightHand), rightHandAnimator);
        }
    }
    public void MapTransform(Transform target, Transform rig)
    {
        if (rig != null)
        {
            target.SetPositionAndRotation(rig.position, rig.rotation);
        }
    }
    private void UpdateHandAnimation(InputDevice targetDevice, Animator handAnimator)
    {
        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            handAnimator.SetFloat("Trigger", triggerValue);
        }
        else
        {
            handAnimator.SetFloat("Trigger", 0);
        }

        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            handAnimator.SetFloat("Grip", gripValue);
        }
        else
        {
            handAnimator.SetFloat("Grip", 0);
        }
    }
}
