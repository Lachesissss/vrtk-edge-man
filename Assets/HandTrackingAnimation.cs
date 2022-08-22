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
    public Vector3[] RelativeLeftHandPos = new Vector3[JOINTS_NUM];
    public Vector3[] RelativeRightHandPos = new Vector3[JOINTS_NUM];
    public Vector3[] RelativeLeftHandAngle = new Vector3[JOINTS_NUM];
    public Vector3[] RelativeRightHandAngle = new Vector3[JOINTS_NUM];
    private HandJoint[] LeftHandJoints = new HandJoint[JOINTS_NUM];
    private HandJoint[] RightHandJoints = new HandJoint[JOINTS_NUM];
    public Transform LeftHandCtrl;
    public Transform RightHandCtrl;
    private Transform LeftHand;
    private Transform RightHand;
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
            /*
            for (int i = 0; i < 20; i++)
            {
                if (i % 4 == 0)
                {
                    RelativeLeftHandPos[i] = LeftHandPos[i] - LeftHandPos[JOINTS_NUM];
                    RelativeLeftHandAngle[i] = LeftHandRot[i].eulerAngles - LeftHandRot[JOINTS_NUM].eulerAngles;
                    RelativeRightHandPos[i] = RightHandPos[i] - RightHandPos[JOINTS_NUM];
                    RelativeRightHandAngle[i] = RightHandRot[i].eulerAngles - RightHandRot[JOINTS_NUM].eulerAngles;
                }
                else
                {
                    RelativeLeftHandPos[i] = LeftHandPos[i] - LeftHandPos[i - 1];
                    RelativeLeftHandAngle[i] = LeftHandRot[i].eulerAngles - LeftHandRot[i - 1].eulerAngles;
                    RelativeRightHandPos[i] = RightHandPos[i] - RightHandPos[JOINTS_NUM];
                    RelativeRightHandAngle[i] = RightHandRot[i].eulerAngles - RightHandRot[i - 1].eulerAngles;
                }
            }*/
            Vector3 playerPos = playerCoordinate.position;
            Quaternion playerRot = playerCoordinate.rotation;
            LeftHandCtrl.localPosition = LeftHandPos[20];
            LeftHandCtrl.localRotation = LeftHandRot[20];
            RightHandCtrl.localPosition = RightHandPos[20];
            RightHandCtrl.localRotation = RightHandRot[20];
            Vector3 LeftHandDelta = LeftHandJoints[0].GetTransform().parent.transform.position - LeftHandCtrl.position;
            Vector3 LeftHandHorizon = LeftHandPos[4] - LeftHandPos[16];
            Vector3 RightHandHorizon = RightHandPos[4] - RightHandPos[16];
            Vector3 RightHandDelta = RightHandJoints[0].GetTransform().parent.transform.position - RightHandCtrl.position; ;

            //LeftHandCtrl.position += LeftHandDelta;
            //LeftHandCtrl.localRotation = LeftHandRot[20];
            //RightHandCtrl.position += RightHandDelta;
            //RightHandCtrl.localRotation = RightHandRot[20];
            Vector3 LeftWristToMiddle = LeftHandPos[8] - LeftHandPos[20];
            Vector3 RightWristToMiddle = RightHandPos[8] - RightHandPos[20];
            Vector3 LeftWristForward = Vector3.Cross(LeftHandHorizon, LeftWristToMiddle);
            Vector3 RightWristForward = Vector3.Cross(RightWristToMiddle, RightHandHorizon);
            Quaternion rotation1 = Quaternion.LookRotation(LeftWristForward, LeftHand.up);
            LeftHand.rotation = rotation1;
            Quaternion rotation2 = Quaternion.LookRotation(RightWristForward, RightHand.up);
            RightHand.rotation = rotation2;
            //Vector3 forward = Vector3.Cross(transform.right, deltaPos);
            //Quaternion rotation = Quaternion.LookRotation(forward, deltaPos);
            //LeftHand.rotation = rotation;

            for (int i = 0; i < 20; i++)
            {
                if (i != 0)
                {
                    LeftHandJoints[i].UpdateRelativeTransfrom(LeftHandPos[i], LeftHandPos[i + 1], LeftHandPos[i - 1], LeftHandHorizon, playerPos, playerRot, LeftHandDelta, true);
                }
                else
                {
                    LeftHandJoints[i].UpdateRelativeTransfrom(LeftHandPos[i], LeftHandPos[i + 1], LeftHandPos[i], LeftHandHorizon, playerPos, playerRot, LeftHandDelta, true);
                }

            }

            for (int i = 0; i < 20; i++)
            {
                if (i != 0)
                {
                    RightHandJoints[i].UpdateRelativeTransfrom(RightHandPos[i], RightHandPos[i + 1], LeftHandPos[i - 1], RightHandHorizon, playerPos, playerRot, RightHandDelta, false);
                }
                else
                {
                    RightHandJoints[i].UpdateRelativeTransfrom(RightHandPos[i], RightHandPos[i + 1], LeftHandPos[i], RightHandHorizon, playerPos, playerRot, RightHandDelta, false);
                }
            }
        }

    }

}

