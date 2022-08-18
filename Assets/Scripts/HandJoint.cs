using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandJoint : MonoBehaviour
{
    public Transform HandJointOffset;
    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public Quaternion GetRotation()
    {
        return transform.rotation;
    }

    public Transform GetParent()
    {
        return transform.parent;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public void UpdateRelativeTransfrom(Vector3 jointOriginPos, Quaternion jointOriginRot, Vector3 playerPos, Quaternion playerRot, Vector3 handPosDelta, Quaternion handRotDelta, bool isThird)
    {
        transform.localPosition = transform.parent.InverseTransformPoint(jointOriginPos + playerPos + handPosDelta) + HandJointOffset.localPosition;
        //transform.localRotation = jointOriginRot * Quaternion.Inverse(transform.parent.transform.rotation) *  playerRot;
        //transform.rotation *= handRotDelta;
        if (isThird)
        {
            Debug.Log("Third!!!!!");
            transform.rotation = transform.parent.rotation;
        }

    }

}
