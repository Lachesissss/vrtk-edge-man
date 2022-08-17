using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FusedVR.VRStreaming;

public class HandTrackingAnimation : MonoBehaviour
{
    [SerializeField] public Vector3 LeftHandPosOffset;
    [SerializeField] public Vector3 RightHandPosOffset;
    [SerializeField] public Quaternion HandRotOffset;
    public Vector3[] LeftHandPos = new Vector3[25];
    public Vector3[] RightHandPos = new Vector3[25];
    public Quaternion[] LeftHandRot = new Quaternion[25];
    public Quaternion[] RightHandRot = new Quaternion[25];
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
        else
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
        LeftHandThumb1 = LeftHand.transform.Find("LeftHandThumb1");
        LeftHandPos[0] = LeftHandThumb1.position;
        LeftHandRot[0] = LeftHandThumb1.rotation;
        LeftHandThumb2 = LeftHandThumb1.transform.Find("LeftHandThumb2");
        LeftHandPos[1] = LeftHandThumb2.position;
        LeftHandRot[1] = LeftHandThumb2.rotation;
        LeftHandThumb3 = LeftHandThumb2.transform.Find("LeftHandThumb3");
        LeftHandPos[2] = LeftHandThumb3.position;
        LeftHandRot[2] = LeftHandThumb3.rotation;
        LeftHandThumb4 = LeftHandThumb3.transform.Find("LeftHandThumb4");
        LeftHandPos[3] = LeftHandThumb4.position;
        LeftHandRot[3] = LeftHandThumb4.rotation;

        LeftHandIndex1 = LeftHand.transform.Find("LeftHandIndex1");
        LeftHandPos[5] = LeftHandIndex1.position;
        LeftHandRot[5] = LeftHandIndex1.rotation;
        LeftHandIndex2 = LeftHandIndex1.transform.Find("LeftHandIndex2");
        LeftHandPos[6] = LeftHandIndex2.position;
        LeftHandRot[6] = LeftHandIndex2.rotation;
        LeftHandIndex3 = LeftHandIndex2.transform.Find("LeftHandIndex3");
        LeftHandPos[7] = LeftHandIndex3.position;
        LeftHandRot[7] = LeftHandIndex3.rotation;
        LeftHandIndex4 = LeftHandIndex3.transform.Find("LeftHandIndex4");
        LeftHandPos[8] = LeftHandIndex4.position;
        LeftHandRot[8] = LeftHandIndex4.rotation;

        LeftHandMiddle1 = LeftHand.transform.Find("LeftHandMiddle1");
        LeftHandPos[10] = LeftHandMiddle1.position;
        LeftHandRot[10] = LeftHandMiddle1.rotation;
        LeftHandMiddle2 = LeftHandMiddle1.transform.Find("LeftHandMiddle2");
        LeftHandPos[11] = LeftHandMiddle2.position;
        LeftHandRot[11] = LeftHandMiddle2.rotation;
        LeftHandMiddle3 = LeftHandMiddle2.transform.Find("LeftHandMiddle3");
        LeftHandPos[12] = LeftHandMiddle3.position;
        LeftHandRot[12] = LeftHandMiddle3.rotation;
        LeftHandMiddle4 = LeftHandMiddle3.transform.Find("LeftHandMiddle4");
        LeftHandPos[13] = LeftHandMiddle4.position;
        LeftHandRot[13] = LeftHandMiddle4.rotation;

        LeftHandRing1 = LeftHand.transform.Find("LeftHandRing1");
        LeftHandPos[15] = LeftHandRing1.position;
        LeftHandRot[15] = LeftHandRing1.rotation;
        LeftHandRing2 = LeftHandRing1.transform.Find("LeftHandRing2");
        LeftHandPos[16] = LeftHandRing2.position;
        LeftHandRot[16] = LeftHandRing2.rotation;
        LeftHandRing3 = LeftHandRing2.transform.Find("LeftHandRing3");
        LeftHandPos[17] = LeftHandRing3.position;
        LeftHandRot[17] = LeftHandRing3.rotation;
        LeftHandRing4 = LeftHandRing3.transform.Find("LeftHandRing4");
        LeftHandPos[18] = LeftHandRing4.position;
        LeftHandRot[18] = LeftHandRing4.rotation;

        LeftHandPinky1 = LeftHand.transform.Find("LeftHandPinky1");
        LeftHandPos[20] = LeftHandPinky1.position;
        LeftHandRot[20] = LeftHandPinky1.rotation;
        LeftHandPinky2 = LeftHandPinky1.transform.Find("LeftHandPinky2");
        LeftHandPos[21] = LeftHandPinky2.position;
        LeftHandRot[21] = LeftHandPinky2.rotation;
        LeftHandPinky3 = LeftHandPinky2.transform.Find("LeftHandPinky3");
        LeftHandPos[22] = LeftHandPinky3.position;
        LeftHandRot[22] = LeftHandPinky3.rotation;
        LeftHandPinky4 = LeftHandPinky3.transform.Find("LeftHandPinky4");
        LeftHandPos[23] = LeftHandPinky4.position;
        LeftHandRot[23] = LeftHandPinky4.rotation;


        RightHandThumb1 = RightHand.transform.Find("RightHandThumb1");
        RightHandPos[0] = RightHandThumb1.position;
        RightHandRot[0] = RightHandThumb1.rotation;
        RightHandThumb2 = RightHandThumb1.transform.Find("RightHandThumb2");
        RightHandPos[1] = RightHandThumb2.position;
        RightHandRot[1] = RightHandThumb2.rotation;
        RightHandThumb3 = RightHandThumb2.transform.Find("RightHandThumb3");
        RightHandPos[2] = RightHandThumb3.position;
        RightHandRot[2] = RightHandThumb3.rotation;
        RightHandThumb4 = RightHandThumb3.transform.Find("RightHandThumb4");
        RightHandPos[3] = RightHandThumb4.position;
        RightHandRot[3] = RightHandThumb4.rotation;

        RightHandIndex1 = RightHand.transform.Find("RightHandIndex1");
        RightHandPos[5] = RightHandIndex1.position;
        RightHandRot[5] = RightHandIndex1.rotation;
        RightHandIndex2 = RightHandIndex1.transform.Find("RightHandIndex2");
        RightHandPos[6] = RightHandIndex2.position;
        RightHandRot[6] = RightHandIndex2.rotation;
        RightHandIndex3 = RightHandIndex2.transform.Find("RightHandIndex3");
        RightHandPos[7] = RightHandIndex3.position;
        RightHandRot[7] = RightHandIndex3.rotation;
        RightHandIndex4 = RightHandIndex3.transform.Find("RightHandIndex4");
        RightHandPos[8] = RightHandIndex4.position;
        RightHandRot[8] = RightHandIndex4.rotation;

        RightHandMiddle1 = RightHand.transform.Find("RightHandMiddle1");
        RightHandPos[10] = RightHandMiddle1.position;
        RightHandRot[10] = RightHandMiddle1.rotation;
        RightHandMiddle2 = RightHandMiddle1.transform.Find("RightHandMiddle2");
        RightHandPos[11] = RightHandMiddle2.position;
        RightHandRot[11] = RightHandMiddle2.rotation;
        RightHandMiddle3 = RightHandMiddle2.transform.Find("RightHandMiddle3");
        RightHandPos[12] = RightHandMiddle3.position;
        RightHandRot[12] = RightHandMiddle3.rotation;
        RightHandMiddle4 = RightHandMiddle3.transform.Find("RightHandMiddle4");
        RightHandPos[13] = RightHandMiddle4.position;
        RightHandRot[13] = RightHandMiddle4.rotation;

        RightHandRing1 = RightHand.transform.Find("RightHandRing1");
        RightHandPos[15] = RightHandRing1.position;
        RightHandRot[15] = RightHandRing1.rotation;
        RightHandRing2 = RightHandRing1.transform.Find("RightHandRing2");
        RightHandPos[16] = RightHandRing2.position;
        RightHandRot[16] = RightHandRing2.rotation;
        RightHandRing3 = RightHandRing2.transform.Find("RightHandRing3");
        RightHandPos[17] = RightHandRing3.position;
        RightHandRot[17] = RightHandRing3.rotation;
        RightHandRing4 = RightHandRing3.transform.Find("RightHandRing4");
        RightHandPos[18] = RightHandRing4.position;
        RightHandRot[18] = RightHandRing4.rotation;

        RightHandPinky1 = RightHand.transform.Find("RightHandPinky1");
        RightHandPos[20] = RightHandPinky1.position;
        RightHandRot[20] = RightHandPinky1.rotation;
        RightHandPinky2 = RightHandPinky1.transform.Find("RightHandPinky2");
        RightHandPos[21] = RightHandPinky2.position;
        RightHandRot[21] = RightHandPinky2.rotation;
        RightHandPinky3 = RightHandPinky2.transform.Find("RightHandPinky3");
        RightHandPos[22] = RightHandPinky3.position;
        RightHandRot[22] = RightHandPinky3.rotation;
        RightHandPinky4 = RightHandPinky3.transform.Find("RightHandPinky4");
        RightHandPos[23] = RightHandPinky4.position;
        RightHandRot[23] = RightHandPinky4.rotation;
    }

    public void LateUpdate()
    {
        if (handTrackingReady)
        {
            Vector3 playerPos = playerCoordinate.position;
            Quaternion playerRot = playerCoordinate.rotation;
            LeftHandThumb1.localPosition = LeftHandThumb1.parent.InverseTransformPoint(LeftHandPos[0] + playerPos) + LeftHandPosOffset;
            Debug.Log(LeftHandThumb1.parent.transform.position.ToString() + ' ' + LeftHandPos[0].ToString());
            LeftHandThumb1.localRotation = Quaternion.Inverse(LeftHandThumb1.parent.transform.rotation) * LeftHandRot[0] * playerRot * HandRotOffset;
            LeftHandThumb2.localPosition = LeftHandThumb2.parent.InverseTransformPoint(LeftHandPos[1] + playerPos) + LeftHandPosOffset;
            LeftHandThumb2.localRotation = Quaternion.Inverse(LeftHandThumb2.parent.transform.rotation) * LeftHandRot[1] * playerRot * HandRotOffset;
            LeftHandThumb3.localPosition = LeftHandThumb3.parent.InverseTransformPoint(LeftHandPos[2] + playerPos) + LeftHandPosOffset;
            LeftHandThumb3.localRotation = Quaternion.Inverse(LeftHandThumb3.parent.transform.rotation) * LeftHandRot[2] * playerRot * HandRotOffset;
            LeftHandThumb4.localPosition = LeftHandThumb4.parent.InverseTransformPoint(LeftHandPos[3] + playerPos) + LeftHandPosOffset;
            LeftHandThumb4.localRotation = Quaternion.Inverse(LeftHandThumb4.parent.transform.rotation) * LeftHandRot[3] * playerRot * HandRotOffset;

            LeftHandIndex1.localPosition = LeftHandIndex1.parent.InverseTransformPoint(LeftHandPos[5] + playerPos) + LeftHandPosOffset;
            LeftHandIndex1.localRotation = Quaternion.Inverse(LeftHandIndex1.parent.transform.rotation) * LeftHandRot[5] * playerRot * HandRotOffset;
            LeftHandIndex2.localPosition = LeftHandIndex2.parent.InverseTransformPoint(LeftHandPos[6] + playerPos) + LeftHandPosOffset;
            LeftHandIndex2.localRotation = Quaternion.Inverse(LeftHandIndex2.parent.transform.rotation) * LeftHandRot[6] * playerRot * HandRotOffset;
            LeftHandIndex3.localPosition = LeftHandIndex3.parent.InverseTransformPoint(LeftHandPos[7] + playerPos) + LeftHandPosOffset;
            LeftHandIndex3.localRotation = Quaternion.Inverse(LeftHandIndex3.parent.transform.rotation) * LeftHandRot[7] * playerRot * HandRotOffset;
            LeftHandIndex4.localPosition = LeftHandIndex4.parent.InverseTransformPoint(LeftHandPos[8] + playerPos) + LeftHandPosOffset;
            LeftHandIndex4.localRotation = Quaternion.Inverse(LeftHandIndex4.parent.transform.rotation) * LeftHandRot[8] * playerRot * HandRotOffset;

            LeftHandMiddle1.localPosition = LeftHandMiddle1.parent.InverseTransformPoint(LeftHandPos[10] + playerPos) + LeftHandPosOffset;
            LeftHandMiddle1.localRotation = Quaternion.Inverse(LeftHandMiddle1.parent.transform.rotation) * LeftHandRot[10] * playerRot * HandRotOffset;
            LeftHandMiddle2.localPosition = LeftHandMiddle2.parent.InverseTransformPoint(LeftHandPos[11] + playerPos) + LeftHandPosOffset;
            LeftHandMiddle2.localRotation = Quaternion.Inverse(LeftHandMiddle2.parent.transform.rotation) * LeftHandRot[11] * playerRot * HandRotOffset;
            LeftHandMiddle3.localPosition = LeftHandMiddle3.parent.InverseTransformPoint(LeftHandPos[12] + playerPos) + LeftHandPosOffset;
            LeftHandMiddle3.localRotation = Quaternion.Inverse(LeftHandMiddle3.parent.transform.rotation) * LeftHandRot[12] * playerRot * HandRotOffset;
            LeftHandMiddle4.localPosition = LeftHandMiddle4.parent.InverseTransformPoint(LeftHandPos[13] + playerPos) + LeftHandPosOffset;
            LeftHandMiddle4.localRotation = Quaternion.Inverse(LeftHandMiddle4.parent.transform.rotation) * LeftHandRot[13] * playerRot * HandRotOffset;

            LeftHandRing1.localPosition = LeftHandRing1.parent.InverseTransformPoint(LeftHandPos[15] + playerPos) + LeftHandPosOffset;
            LeftHandRing1.localRotation = Quaternion.Inverse(LeftHandRing1.parent.transform.rotation) * LeftHandRot[15] * playerRot * HandRotOffset;
            LeftHandRing2.localPosition = LeftHandRing2.parent.InverseTransformPoint(LeftHandPos[16] + playerPos) + LeftHandPosOffset;
            LeftHandRing2.localRotation = Quaternion.Inverse(LeftHandRing2.parent.transform.rotation) * LeftHandRot[16] * playerRot * HandRotOffset;
            LeftHandRing3.localPosition = LeftHandRing3.parent.InverseTransformPoint(LeftHandPos[17] + playerPos) + LeftHandPosOffset;
            LeftHandRing3.localRotation = Quaternion.Inverse(LeftHandRing3.parent.transform.rotation) * LeftHandRot[17] * playerRot * HandRotOffset;
            LeftHandRing4.localPosition = LeftHandRing4.parent.InverseTransformPoint(LeftHandPos[18] + playerPos) + LeftHandPosOffset;
            LeftHandRing4.localRotation = Quaternion.Inverse(LeftHandRing4.parent.transform.rotation) * LeftHandRot[18] * playerRot * HandRotOffset;

            LeftHandPinky1.localPosition = LeftHandPinky1.parent.InverseTransformPoint(LeftHandPos[20] + playerPos) + LeftHandPosOffset;
            LeftHandPinky1.localRotation = Quaternion.Inverse(LeftHandPinky1.parent.transform.rotation) * LeftHandRot[20] * playerRot * HandRotOffset;
            LeftHandPinky2.localPosition = LeftHandPinky2.parent.InverseTransformPoint(LeftHandPos[21] + playerPos) + LeftHandPosOffset;
            LeftHandPinky2.localRotation = Quaternion.Inverse(LeftHandPinky2.parent.transform.rotation) * LeftHandRot[21] * playerRot * HandRotOffset;
            LeftHandPinky3.localPosition = LeftHandPinky3.parent.InverseTransformPoint(LeftHandPos[22] + playerPos) + LeftHandPosOffset;
            LeftHandPinky3.localRotation = Quaternion.Inverse(LeftHandPinky3.parent.transform.rotation) * LeftHandRot[22] * playerRot * HandRotOffset;
            LeftHandPinky4.localPosition = LeftHandPinky4.parent.InverseTransformPoint(LeftHandPos[23] + playerPos) + LeftHandPosOffset;
            LeftHandPinky4.localRotation = Quaternion.Inverse(LeftHandPinky4.parent.transform.rotation) * LeftHandRot[23] * playerRot * HandRotOffset;


            RightHandThumb1.localPosition = RightHandThumb1.parent.InverseTransformPoint(RightHandPos[0] + playerPos) + RightHandPosOffset;
            RightHandThumb1.localRotation = Quaternion.Inverse(RightHandThumb1.parent.transform.rotation) * RightHandRot[0] * playerRot * HandRotOffset;
            RightHandThumb2.localPosition = RightHandThumb2.parent.InverseTransformPoint(RightHandPos[1] + playerPos) + RightHandPosOffset;
            RightHandThumb2.localRotation = Quaternion.Inverse(RightHandThumb2.parent.transform.rotation) * RightHandRot[1] * playerRot * HandRotOffset;
            RightHandThumb3.localPosition = RightHandThumb3.parent.InverseTransformPoint(RightHandPos[2] + playerPos) + RightHandPosOffset;
            RightHandThumb3.localRotation = Quaternion.Inverse(RightHandThumb3.parent.transform.rotation) * RightHandRot[2] * playerRot * HandRotOffset;
            RightHandThumb4.localPosition = RightHandThumb4.parent.InverseTransformPoint(RightHandPos[3] + playerPos) + RightHandPosOffset;
            RightHandThumb4.localRotation = Quaternion.Inverse(RightHandThumb4.parent.transform.rotation) * RightHandRot[3] * playerRot * HandRotOffset;

            RightHandIndex1.localPosition = RightHandIndex1.parent.InverseTransformPoint(RightHandPos[5] + playerPos) + RightHandPosOffset;
            RightHandIndex1.localRotation = Quaternion.Inverse(RightHandIndex1.parent.transform.rotation) * RightHandRot[5] * playerRot * HandRotOffset;
            RightHandIndex2.localPosition = RightHandIndex2.parent.InverseTransformPoint(RightHandPos[6] + playerPos) + RightHandPosOffset;
            RightHandIndex2.localRotation = Quaternion.Inverse(RightHandIndex2.parent.transform.rotation) * RightHandRot[6] * playerRot * HandRotOffset;
            RightHandIndex3.localPosition = RightHandIndex3.parent.InverseTransformPoint(RightHandPos[7] + playerPos) + RightHandPosOffset;
            RightHandIndex3.localRotation = Quaternion.Inverse(RightHandIndex3.parent.transform.rotation) * RightHandRot[7] * playerRot * HandRotOffset;
            RightHandIndex4.localPosition = RightHandIndex4.parent.InverseTransformPoint(RightHandPos[8] + playerPos) + RightHandPosOffset;
            RightHandIndex4.localRotation = Quaternion.Inverse(RightHandIndex4.parent.transform.rotation) * RightHandRot[8] * playerRot * HandRotOffset;

            RightHandMiddle1.localPosition = RightHandMiddle1.parent.InverseTransformPoint(RightHandPos[10] + playerPos) + RightHandPosOffset;
            RightHandMiddle1.localRotation = Quaternion.Inverse(RightHandMiddle1.parent.transform.rotation) * RightHandRot[10] * playerRot * HandRotOffset;
            RightHandMiddle2.localPosition = RightHandMiddle2.parent.InverseTransformPoint(RightHandPos[11] + playerPos) + RightHandPosOffset;
            RightHandMiddle2.localRotation = Quaternion.Inverse(RightHandMiddle2.parent.transform.rotation) * RightHandRot[11] * playerRot * HandRotOffset;
            RightHandMiddle3.localPosition = RightHandMiddle3.parent.InverseTransformPoint(RightHandPos[12] + playerPos) + RightHandPosOffset;
            RightHandMiddle3.localRotation = Quaternion.Inverse(RightHandMiddle3.parent.transform.rotation) * RightHandRot[12] * playerRot * HandRotOffset;
            RightHandMiddle4.localPosition = RightHandMiddle4.parent.InverseTransformPoint(RightHandPos[13] + playerPos) + RightHandPosOffset;
            RightHandMiddle4.localRotation = Quaternion.Inverse(RightHandMiddle4.parent.transform.rotation) * RightHandRot[13] * playerRot * HandRotOffset;

            RightHandRing1.localPosition = RightHandRing1.parent.InverseTransformPoint(RightHandPos[15] + playerPos) + RightHandPosOffset;
            RightHandRing1.localRotation = Quaternion.Inverse(RightHandRing1.parent.transform.rotation) * RightHandRot[15] * playerRot * HandRotOffset;
            RightHandRing2.localPosition = RightHandRing2.parent.InverseTransformPoint(RightHandPos[16] + playerPos) + RightHandPosOffset;
            RightHandRing2.localRotation = Quaternion.Inverse(RightHandRing2.parent.transform.rotation) * RightHandRot[16] * playerRot * HandRotOffset;
            RightHandRing3.localPosition = RightHandRing3.parent.InverseTransformPoint(RightHandPos[17] + playerPos) + RightHandPosOffset;
            RightHandRing3.localRotation = Quaternion.Inverse(RightHandRing3.parent.transform.rotation) * RightHandRot[17] * playerRot * HandRotOffset;
            RightHandRing4.localPosition = RightHandRing4.parent.InverseTransformPoint(RightHandPos[18] + playerPos) + RightHandPosOffset;
            RightHandRing4.localRotation = Quaternion.Inverse(RightHandRing4.parent.transform.rotation) * RightHandRot[18] * playerRot * HandRotOffset;

            RightHandPinky1.localPosition = RightHandPinky1.parent.InverseTransformPoint(RightHandPos[20] + playerPos) + RightHandPosOffset;
            RightHandPinky1.localRotation = Quaternion.Inverse(RightHandPinky1.parent.transform.rotation) * RightHandRot[20] * playerRot * HandRotOffset;
            RightHandPinky2.localPosition = RightHandPinky2.parent.InverseTransformPoint(RightHandPos[21] + playerPos) + RightHandPosOffset;
            RightHandPinky2.localRotation = Quaternion.Inverse(RightHandPinky2.parent.transform.rotation) * RightHandRot[21] * playerRot * HandRotOffset;
            RightHandPinky3.localPosition = RightHandPinky3.parent.InverseTransformPoint(RightHandPos[22] + playerPos) + RightHandPosOffset;
            RightHandPinky3.localRotation = Quaternion.Inverse(RightHandPinky3.parent.transform.rotation) * RightHandRot[22] * playerRot * HandRotOffset;
            RightHandPinky4.localPosition = RightHandPinky4.parent.InverseTransformPoint(RightHandPos[23] + playerPos) + RightHandPosOffset;
            RightHandPinky4.localRotation = Quaternion.Inverse(RightHandPinky4.parent.transform.rotation) * RightHandRot[23] * playerRot * HandRotOffset;
        }
        
    }

}

