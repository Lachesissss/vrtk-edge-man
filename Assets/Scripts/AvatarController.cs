using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]  // �����л�

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

    [SerializeField] private float turnSmoothness;  // ��ֵƵ��
    [SerializeField] Transform ikHead;
    [SerializeField] Vector3 headBodyOffset;

    private void LateUpdate()
    {
        transform.position = ikHead.position + headBodyOffset;
        //Debug.Log(transform.position.ToString());
        // �ϰ�����ֱ������Ȼ�������ֵĶ���,���Ҽ������Բ�ֵ����
        transform.forward = Vector3.Lerp(transform.forward,Vector3.ProjectOnPlane(ikHead.forward, Vector3.up).normalized,Time.deltaTime * turnSmoothness);

        head.VRMapping();
        leftHand.VRMapping();
        rightHand.VRMapping();
    }
}
