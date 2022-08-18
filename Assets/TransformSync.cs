using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformSync : MonoBehaviour
{
    public Transform TargetSource;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {
        transform.position = TargetSource.position;
        transform.rotation = TargetSource.rotation;
    }
}
