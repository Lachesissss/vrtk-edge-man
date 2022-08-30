using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodyRestriction : MonoBehaviour
{
    private int playerCounter;
    private Transform followPoint;
    private Vector3 DeltaPos;
    private bool stay;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        stay = false;
        rb = GetComponent<Rigidbody>();
    }



    private void OnCollisionEnter(Collision collision)
    {
        GameObject target = collision.gameObject;
        if (target.layer == 8 || target.layer == 9)
        {
            playerCounter++;
            if (playerCounter >= 5)
            {
                rb.constraints = RigidbodyConstraints.FreezeAll;
                //rb.isKinematic = false;
                stay = true;
                /*
                Transform tem = target.transform;
                while (tem.gameObject.layer != 8 && tem.gameObject.layer != 9)
                {
                    tem = tem.parent;
                }
                followPoint = tem;*/
                followPoint = target.transform;
                DeltaPos = rb.position - followPoint.position;
            }
        }
    }

    private void FixedUpdate()
    {
        if (stay)
        {
            rb.MovePosition(followPoint.position + DeltaPos);
            rb.velocity = new Vector3(0, 0, 0);
        }
    }

    private void OnCollisionStay(Collision collision)
    {

    }

    private void OnCollisionExit(Collision collision)
    {
        GameObject target = collision.gameObject;
        if (target.layer == 8 || target.layer == 9)
        {
            playerCounter--;
            if (playerCounter < 5)
            {
                rb.constraints = RigidbodyConstraints.None;
                //rb.isKinematic = true;
                stay = false;
            }
        }
        rb.velocity = new Vector3(rb.velocity.x > 0.1f ? 0.1f : rb.velocity.x, rb.velocity.y > 0.1f ? 0.1f : rb.velocity.y, rb.velocity.z > 0.1f ? 0.1f : rb.velocity.z);
    }
}
