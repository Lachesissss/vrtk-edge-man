using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FusedVR.VRStreaming
{
    
    public class ApplyHandTrackingData : MonoBehaviour
    {
        public VRInputManager.Source handType;
        public VRInputManager.HandTrackingSource handTrackingType;

        public void ApplyHandTracking(VRInputManager.Source handID,VRInputManager.HandTrackingSource boneID, Vector3 position, Quaternion rotation)
        {
            if (handType == handID && handTrackingType==boneID) //check if the data is from the correct source
            {
                //Debug.Log(handID.ToString() + ' ' + boneID.ToString());
                transform.localPosition = new Vector3(position.x, position.y, -position.z);//note that z data is reversed on WebXR
                transform.localRotation = rotation; //apply rotation - note coordinate system change was already applied
            }
        }
    }
}

