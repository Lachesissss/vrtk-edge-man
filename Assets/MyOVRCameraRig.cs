#if USING_XR_MANAGEMENT && (USING_XR_SDK_OCULUS || USING_XR_SDK_OPENXR)
#define USING_XR_SDK
#endif
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Node = UnityEngine.XR.XRNode;

public class MyOVRCameraRig : OVRCameraRig
{
	public Transform Root;
	public Transform HeadTransform;
	public Transform LeftHandTransfrom;
	public Transform RightHandTransfrom;
	public Transform LeftEyeTransfrom;
	public Transform RightEyeTransfrom;
	// Start is called before the first frame update
	protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void UpdateAnchors(bool updateEyeAnchors, bool updateHandAnchors)
    {
		//Debug.Log(HeadTransform.localPosition.x.ToString()+","+ HeadTransform.localPosition.y.ToString() + "," + HeadTransform.localPosition.z.ToString());
		//if (!OVRManager.OVRManagerinitialized)
			//return;

		EnsureGameObjectIntegrity();

		//if (!Application.isPlaying)
		//return;
		
		if (_skipUpdate)
		{
			centerEyeAnchor.FromOVRPose(OVRPose.identity, true);
			leftEyeAnchor.FromOVRPose(OVRPose.identity, true);
			rightEyeAnchor.FromOVRPose(OVRPose.identity, true);

			return;
		}
		//Debug.Log("here");
		OVRPose tracker = OVRPose.identity;

		trackerAnchor.localRotation = tracker.orientation;

		//Quaternion emulatedRotation = Quaternion.Euler(-HeadTransform.localRotation.x, -HeadTransform.localRotation.y, HeadTransform.localRotation.z);

		//Note: in the below code, when using UnityEngine's API, we only update anchor transforms if we have a new, fresh value this frame.
		//If we don't, it could mean that tracking is lost, etc. so the pose should not change in the virtual world.
		//This can be thought of as similar to calling InputTracking GetLocalPosition and Rotation, but only for doing so when the pose is valid.
		//If false is returned for any of these calls, then a new pose is not valid and thus should not be updated.
		if (updateEyeAnchors)
		{
			centerEyeAnchor.localRotation = HeadTransform.localRotation;
			centerEyeAnchor.localPosition = HeadTransform.localPosition;
			leftEyeAnchor.localPosition = LeftEyeTransfrom.localPosition+centerEyeAnchor.localPosition;
			rightEyeAnchor.localPosition = RightEyeTransfrom.localPosition+centerEyeAnchor.localPosition;
			leftEyeAnchor.localRotation = HeadTransform.localRotation;
			rightEyeAnchor.localRotation = HeadTransform.localRotation;
		}

		if (updateHandAnchors)
		{
			//Need this for controller offset because if we're on OpenVR, we want to set the local poses as specified by Unity, but if we're not, OVRInput local position is the right anchor
			leftHandAnchor.localPosition = LeftHandTransfrom.localPosition;
			rightHandAnchor.localPosition = RightHandTransfrom.localPosition;
			leftHandAnchor.localRotation = LeftHandTransfrom.localRotation;
			rightHandAnchor.localRotation = RightHandTransfrom.localRotation;


			trackerAnchor.localPosition = tracker.position;

			OVRPose leftOffsetPose = OVRPose.identity;
			OVRPose rightOffsetPose = OVRPose.identity;
			rightControllerAnchor.localPosition = rightOffsetPose.position;
			rightControllerAnchor.localRotation = rightOffsetPose.orientation;
			leftControllerAnchor.localPosition = leftOffsetPose.position;
			leftControllerAnchor.localRotation = leftOffsetPose.orientation;
			//Debug.Log("hand!");
		}

#if USING_XR_SDK
#if UNITY_2020_3_OR_NEWER
		if (OVRManager.instance.LateLatching)
		{
			XRDisplaySubsystem displaySubsystem = OVRManager.GetCurrentDisplaySubsystem();
			if (displaySubsystem != null)
			{
				displaySubsystem.MarkTransformLateLatched(centerEyeAnchor.transform, XRDisplaySubsystem.LateLatchNode.Head);
				displaySubsystem.MarkTransformLateLatched(leftHandAnchor, XRDisplaySubsystem.LateLatchNode.LeftHand);
				displaySubsystem.MarkTransformLateLatched(rightHandAnchor, XRDisplaySubsystem.LateLatchNode.RightHand);
			}
		}
#endif
#endif
		RaiseUpdatedAnchorsEvent();
	}
}
