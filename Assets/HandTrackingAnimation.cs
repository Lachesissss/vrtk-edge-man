using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FusedVR.VRStreaming;

public class HandTrackingAnimation : MonoBehaviour
{
    const int JOINTS_NUM = 20;
    public Vector3[] LeftHandPos = new Vector3[JOINTS_NUM + 1];
    public Vector3[] RightHandPos = new Vector3[JOINTS_NUM + 1];
    public Quaternion[] LeftHandRot = new Quaternion[JOINTS_NUM + 1];
    public Quaternion[] RightHandRot = new Quaternion[JOINTS_NUM + 1];
    private HandJoint[] LeftHandJoints = new HandJoint[JOINTS_NUM];
    private HandJoint[] RightHandJoints = new HandJoint[JOINTS_NUM];
    public Transform LeftHandCtrl;
    public Transform RightHandCtrl;
    private Transform LeftHand;
    private Transform RightHand;
    private Transform LeftHandThumb1;
    private Transform LeftHandThumb2;
    private Transform LeftHandThumb3;
    private Transform LeftHandThumb4;
    private Transform LeftHandIndex1;
    private Transform LeftHandIndex2;
    private Transform LeftHandIndex3;
    private Transform LeftHandIndex4;
    private Transform LeftHandMiddle1;
    private Transform LeftHandMiddle2;
    private Transform LeftHandMiddle3;
    private Transform LeftHandMiddle4;
    private Transform LeftHandRing1;
    private Transform LeftHandRing2;
    private Transform LeftHandRing3;
    private Transform LeftHandRing4;
    private Transform LeftHandPinky1;
    private Transform LeftHandPinky2;
    private Transform LeftHandPinky3;
    private Transform LeftHandPinky4;

    private Transform RightHandThumb1;
    private Transform RightHandThumb2;
    private Transform RightHandThumb3;
    private Transform RightHandThumb4;
    private Transform RightHandIndex1;
    private Transform RightHandIndex2;
    private Transform RightHandIndex3;
    private Transform RightHandIndex4;
    private Transform RightHandMiddle1;
    private Transform RightHandMiddle2;
    private Transform RightHandMiddle3;
    private Transform RightHandMiddle4;
    private Transform RightHandRing1;
    private Transform RightHandRing2;
    private Transform RightHandRing3;
    private Transform RightHandRing4;
    private Transform RightHandPinky1;
    private Transform RightHandPinky2;
    private Transform RightHandPinky3;
    private Transform RightHandPinky4;
    private Transform playerCoordinate;
    private bool handTrackingReady = false;


    public void UpdateHandTrackingMsg(VRInputManager.Source handID, VRInputManager.HandTrackingSource boneID, Vector3 position, Quaternion rotation)
    {
        if (handID == VRInputManager.Source.Left)
        {
            handTrackingReady = true;
            LeftHandPos[(int)boneID] = new Vector3(position.x, position.y, -position.z);
            LeftHandRot[(int)boneID] = rotation;
        }
        else if ((handID == VRInputManager.Source.Right))
        {
            handTrackingReady = true;
            RightHandPos[(int)boneID] = new Vector3(position.x, position.y, -position.z);
            RightHandRot[(int)boneID] = rotation;
        }

    }

    public void Start()
    {
        playerCoordinate = GameObject.FindGameObjectWithTag("camera").transform;
        LeftHand = transform.Find("Armature/Hips/Spine/Spine1/Spine2/LeftShoulder/LeftArm/LeftForeArm/LeftHand");
        RightHand = transform.Find("Armature/Hips/Spine/Spine1/Spine2/RightShoulder/RightArm/RightForeArm/RightHand");
        Transform currentLeft = LeftHand;
        for (int i = 0; i < 5; i++)
        {
            currentLeft = LeftHand.GetChild(i);
            LeftHandJoints[i * 4] = currentLeft.GetComponent<HandJoint>();
            LeftHandPos[i * 4] = LeftHandJoints[i * 4].GetPosition();
            LeftHandRot[i * 4] = LeftHandJoints[i * 4].GetRotation();
            for (int j = 1; j < 4; j++)
            {
                currentLeft = currentLeft.GetChild(0);
                int num = i * 4 + j;
                LeftHandJoints[num] = currentLeft.GetComponent<HandJoint>();
                LeftHandPos[num] = LeftHandJoints[num].GetPosition();
                LeftHandRot[num] = LeftHandJoints[num].GetRotation();
            }

        }
        Transform currentRight = RightHand;
        for (int i = 0; i < 5; i++)
        {
            currentRight = RightHand.GetChild(i);
            RightHandJoints[i * 4] = currentRight.GetComponent<HandJoint>();
            RightHandPos[i * 4] = RightHandJoints[i * 4].GetPosition();
            RightHandRot[i * 4] = RightHandJoints[i * 4].GetRotation();
            for (int j = 1; j < 4; j++)
            {
                currentRight = currentRight.GetChild(0);
                int num = i * 4 + j;
                RightHandJoints[num] = currentRight.GetComponent<HandJoint>();
                RightHandPos[num] = RightHandJoints[num].GetPosition();
                RightHandRot[num] = RightHandJoints[num].GetRotation();
            }

        }
    }

    public Transform JointOperation(Transform joint, Vector3 JointOrigin, Vector3 playerPos, Vector3 HandPosDelta, Quaternion HandRotDelta)
    {
        joint.localPosition = joint.parent.InverseTransformPoint(JointOrigin + playerPos + HandPosDelta);
        joint.rotation *= HandRotDelta;
        return joint;
    }

    public void LateUpdate()
    {
        if (handTrackingReady)
        {
            Vector3 playerPos = playerCoordinate.position;
            Quaternion playerRot = playerCoordinate.rotation;
            LeftHandCtrl.localPosition = LeftHandPos[20];
            LeftHandCtrl.localRotation = LeftHandRot[20];
            RightHandCtrl.localPosition = RightHandPos[20];
            RightHandCtrl.localRotation = RightHandRot[20];
            Vector3 LeftHandDelta = LeftHandJoints[0].GetTransform().parent.transform.position - LeftHandCtrl.position;
            Quaternion LeftHandRotDelta = LeftHandJoints[0].GetTransform().parent.transform.rotation * Quaternion.Inverse(LeftHandCtrl.rotation);
            Vector3 RightHandDelta = RightHandJoints[0].GetTransform().parent.transform.position - RightHandCtrl.position; ;
            Quaternion RightHandRotDelta = RightHandJoints[0].GetTransform().parent.transform.rotation * Quaternion.Inverse(RightHandCtrl.rotation);
            int start = 2;
            for (int i = 0; i < 20; i++)
            {
                if (i == start)
                {
                    start += 4;
                    LeftHandJoints[i].UpdateRelativeTransfrom(LeftHandPos[i], LeftHandRot[i], playerPos, playerRot, LeftHandDelta, LeftHandRotDelta, true);
                }
                else
                {
                    LeftHandJoints[i].UpdateRelativeTransfrom(LeftHandPos[i], LeftHandRot[i], playerPos, playerRot, LeftHandDelta, LeftHandRotDelta, false);
                }

            }
            start = 2;
            for (int i = 0; i < 20; i++)
            {
                if (i == start)
                {
                    start += 4;
                    RightHandJoints[i].UpdateRelativeTransfrom(RightHandPos[i], RightHandRot[i], playerPos, playerRot, RightHandDelta, RightHandRotDelta, true);
                }
                else
                {
                    RightHandJoints[i].UpdateRelativeTransfrom(RightHandPos[i], RightHandRot[i], playerPos, playerRot, RightHandDelta, RightHandRotDelta, false);
                }
            }
        }

    }

}

