using UnityEngine;
using Mirror;

public class MyNetworkTransformChild : MyNetworkTransformBase
{
    public Transform target;
    protected override Transform targetComponent => target;
}
