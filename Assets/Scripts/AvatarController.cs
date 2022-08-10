using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]  // 可序列化

public class MapTransforms
{
    public Transform vrTarget;
    public Transform ikTarget;

    public Vector3 trackingPostionOffset;
    public Vector3 trackingRotationOffset;

    public void VRMapping()
    {
        ikTarget.position = vrTarget.TransformPoint(trackingPostionOffset);
        //Debug.Log("ikTarget.position: " + ikTarget.position.ToString());
        //Debug.Log("VRTarget.position: " + vrTarget.position.ToString());
        ikTarget.rotation = vrTarget.rotation * Quaternion.Euler(trackingRotationOffset);
    }
}

public class AvatarController : MonoBehaviour
{
    [SerializeField] private MapTransforms head;
    [SerializeField] private MapTransforms leftHand;
    [SerializeField] private MapTransforms rightHand;

    [SerializeField] private float turnSmoothness;  // 插值频率
    [SerializeField] Transform ikHead;
    [SerializeField] Vector3 headBodyOffset;

    private void LateUpdate()
    {
        transform.position = ikHead.position + headBodyOffset;
        //Debug.Log(transform.position.ToString());
        // 上半身保持直立，不然会出现奇怪的动画,并且加入线性插值机制
        transform.forward = Vector3.Lerp(transform.forward,Vector3.ProjectOnPlane(ikHead.forward, Vector3.up).normalized,Time.deltaTime * turnSmoothness);

        head.VRMapping();
        leftHand.VRMapping();
        rightHand.VRMapping();
    }
}
