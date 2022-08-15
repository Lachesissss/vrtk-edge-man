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
                if (position.x == double.NaN || position.y == double.NaN || position.z == double.NaN || rotation.w == double.NaN || rotation.x == double.NaN)
                    return;
                transform.localPosition = new Vector3(position.x, position.y, -position.z); //note that z data is reversed on WebXR
                transform.localRotation = rotation; //apply rotation - note coordinate system change was already applied
            }
        }
    }
}

