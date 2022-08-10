using UnityEngine;

public class LowerBodyController : MonoBehaviour
{
    [SerializeField] Animator animator;  // 序列化一个动画对象获取IK的信息

    [SerializeField] [Range(0, 1)] private float leftFootPositionWeight;  // 0,1代表骨骼动画的表现程度，1代表完全蹲下
    [SerializeField] [Range(0, 1)] private float rightFootPositionWeight;
    [SerializeField] [Range(0, 1)] private float leftFootRotationWeight;  
    [SerializeField] [Range(0, 1)] private float rightFootRotationWeight;

    [SerializeField] private Vector3 footOffset; //用于在编辑器里面手动调整一下脚步位置

    [SerializeField] private Vector3 raycastLeftOffset;  //离地面的距离
    [SerializeField] private Vector3 raycastRightOffset;

    private void OnAnimatorIK(int layerIndex)   //IK动画（反向运动学）的回调函数
    {
        Vector3 leftFootPosition = animator.GetIKPosition(AvatarIKGoal.LeftFoot);  // 获取此时左脚的IK坐标
        Vector3 RightFootPosition = animator.GetIKPosition(AvatarIKGoal.RightFoot);

        RaycastHit hitLeftFoot;  //射线检测：脚是否踩踏在地面上  我们从脚向下方发射射线测量与地面的距离
        RaycastHit hitRightFoot;

        bool isLeftFootDown = Physics.Raycast(leftFootPosition + raycastLeftOffset, Vector3.down, out hitLeftFoot); // out关键字：将结果返回给某已有变量
        bool isRightFootDown = Physics.Raycast(RightFootPosition + raycastRightOffset, Vector3.down, out hitRightFoot);
        //Debug.Log("isRightFootDown: "+isRightFootDown.ToString());
        //Debug.Log("isLeftFootDown: " + isLeftFootDown.ToString());
        //设置IK动画影响位置偏移的比重值，0最小1最大
        CalculateLeftFoot(isLeftFootDown,hitLeftFoot);
        CalculateRightFoot(isRightFootDown, hitRightFoot);
    }

    void CalculateLeftFoot(bool isLeftFootDown, RaycastHit hitLeftFoot)
    {
        if (isLeftFootDown)
        {
            animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, leftFootRotationWeight);   // 左脚IK坐标平滑地变为计算得到的坐标
            animator.SetIKPosition(AvatarIKGoal.LeftFoot, hitLeftFoot.point + footOffset);
      
            //Debug.Log("LeftFootPos: " + hitLeftFoot.point.ToString());
            // 计算左脚目标朝向，并平滑地变化
            Quaternion leftFootRotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(transform.forward, hitLeftFoot.normal), hitLeftFoot.normal);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, leftFootRotationWeight);
            animator.SetIKRotation(AvatarIKGoal.LeftFoot, leftFootRotation);
        }
        else
        {
            //Debug.Log("LeftFootPos: " + hitLeftFoot.point.ToString());
            animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 0);
        }
    }

    void CalculateRightFoot(bool isRightFootDown, RaycastHit hitRightFoot)
    {
        if (isRightFootDown)
        {
            animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, rightFootRotationWeight);   // 左脚IK坐标平滑地变为计算得到的坐标
            animator.SetIKPosition(AvatarIKGoal.RightFoot, hitRightFoot.point + footOffset);

            // 计算左脚目标朝向，并平滑地变化
            Quaternion rightFootRotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(transform.forward, hitRightFoot.normal), hitRightFoot.normal);
            animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, rightFootRotationWeight);
            animator.SetIKRotation(AvatarIKGoal.RightFoot, rightFootRotation);
        }
        else
        {
            animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 0);
        }
    }
}
