using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[System.Serializable]  // �����л�

public class NetworkAvatarController : NetworkBehaviour
{
    [SerializeField] private MapTransforms head;
    [SerializeField] private MapTransforms leftHand;
    [SerializeField] private MapTransforms rightHand;

    [SerializeField] private float turnSmoothness;  // ��ֵƵ��
    [SerializeField] Transform ikHead;
    [SerializeField] Vector3 headBodyOffset;

    private GameObject LocalAvatar;

    private void Start()
    {
        if (isLocalPlayer)
        {
            LocalAvatar = GameObject.FindGameObjectWithTag("LocalAvatar");
            //LocalIKRightTarget = LocalAvatarTransform.Find("Armature/Hips/Spine/Spine1/Spine2/RightShoulder/RightArm/RightForeArm/RightHand");
            //LocalIKLeftTarget = LocalAvatarTransform.Find("Armature/Hips/Spine/Spine1/Spine2/LeftShoulder/LeftArm/LeftForeArm/LeftHand");
            //LocalIKHead = LocalAvatarTransform.Find("Armature/Hips/Spine/Spine1/Spine2/Neck/Head");
            //head.vrTarget = GameObject.FindGameObjectWithTag("MainCamera").transform;
            //leftHand.vrTarget = GameObject.FindGameObjectWithTag("LeftController").transform;
            //rightHand.vrTarget = GameObject.FindGameObjectWithTag("RightController").transform;
            head.vrTarget = LocalAvatar.transform.Find("Armature/Hips/Spine/Spine1/Spine2/Neck/Head");
            leftHand.vrTarget = LocalAvatar.transform.Find("Armature/Hips/Spine/Spine1/Spine2/LeftShoulder/LeftArm/LeftForeArm/LeftHand");
            rightHand.vrTarget = LocalAvatar.transform.Find("Armature/Hips/Spine/Spine1/Spine2/RightShoulder/RightArm/RightForeArm/RightHand");
        }
    }
    private void LateUpdate()
    {
        if (isLocalPlayer)
        {
            Debug.Log("is LocalPlayer");
            //transform.position = ikHead.position + headBodyOffset;
            transform.position = LocalAvatar.transform.position;
            //Debug.Log(transform.position.ToString());
            // �ϰ�����ֱ������Ȼ�������ֵĶ���,���Ҽ������Բ�ֵ����
            //transform.forward = Vector3.Lerp(transform.forward, Vector3.ProjectOnPlane(ikHead.forward, Vector3.up).normalized, Time.deltaTime * turnSmoothness);
            transform.forward = LocalAvatar.transform.forward;
            head.VRMapping();
            leftHand.VRMapping();
            rightHand.VRMapping();
        }

    }
}
