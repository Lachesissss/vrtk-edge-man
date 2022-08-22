using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandJoint : MonoBehaviour
{
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

    public void UpdateRelativeTransfrom(Vector3 jointOriginPos, Vector3 ChildjointOriginPos, Vector3 ParentJointOriginPos, Vector3 Horizon, Vector3 playerPos, Quaternion playerRot, Vector3 handPosDelta, bool isLeft)
    {
        transform.localPosition = transform.parent.InverseTransformPoint(jointOriginPos + playerPos + handPosDelta);

        if (isLeft)
        {
            if (GetComponentsInChildren<Transform>(true).Length > 1)
            {
                Vector3 posTemp = transform.GetChild(0).position;
                Quaternion rotTemp = transform.GetChild(0).rotation;
                Vector3 deltaPos = ChildjointOriginPos - jointOriginPos;
                Vector3 forward = Vector3.Cross(Horizon, deltaPos);
                Quaternion rotation = Quaternion.LookRotation(forward, deltaPos);
                transform.rotation = rotation;
                //Vector3 Finalforward = Vector3.Cross(transform.forward, transform.up);
                //rotation = Quaternion.LookRotation(Finalforward, transform.up);
                //transform.rotation = rotation;
                //transform.up = posTemp - transform.position;
                transform.GetChild(0).rotation = rotTemp;
                transform.GetChild(0).position = posTemp;
            }
            else
            {
                Vector3 deltaPos = jointOriginPos - ParentJointOriginPos;
                Vector3 forward = Vector3.Cross(Horizon, deltaPos);
                Quaternion rotation = Quaternion.LookRotation(forward, deltaPos);
                transform.rotation = rotation;
                //Vector3 Finalforward = Vector3.Cross(transform.forward, transform.up);
                //rotation = Quaternion.LookRotation(Finalforward, transform.up);
                //transform.rotation = rotation;
            }
        }
        else
        {
            if (GetComponentsInChildren<Transform>(true).Length > 1)
            {
                Vector3 posTemp = transform.GetChild(0).position;
                Quaternion rotTemp = transform.GetChild(0).rotation;
                Vector3 deltaPos = ChildjointOriginPos - jointOriginPos;
                Vector3 forward = Vector3.Cross(deltaPos, Horizon);
                Quaternion rotation = Quaternion.LookRotation(forward, deltaPos);
                transform.rotation = rotation;
                //Vector3 Finalforward = Vector3.Cross(transform.forward, transform.up);
                //rotation = Quaternion.LookRotation(Finalforward, transform.up);
                //transform.rotation = rotation;
                //transform.up = posTemp - transform.position;
                transform.GetChild(0).rotation = rotTemp;
                transform.GetChild(0).position = posTemp;
            }
            else
            {
                Vector3 deltaPos = jointOriginPos - ParentJointOriginPos;
                Vector3 forward = Vector3.Cross(deltaPos, Horizon);
                Quaternion rotation = Quaternion.LookRotation(forward, deltaPos);
                transform.rotation = rotation;
            }
        }



        //transform.rotation = Quaternion.identity;
        //transform.rotate(euler1 - euler2, Space.World);

        /*
        if(transform.GetChild(0))
            transform.up = transform.position - transform.GetChild(0).position;*/

        //transform.localRotation = jointOriginRot * Quaternion.Inverse(transform.parent.transform.rotation) *  playerRot;
        //transform.rotation *= handRotDelta;
    }

}
