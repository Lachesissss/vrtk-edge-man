using UnityEngine;

public class LowerBodyController : MonoBehaviour
{
    [SerializeField] Animator animator;  // ���л�һ�����������ȡIK����Ϣ

    [SerializeField] [Range(0, 1)] private float leftFootPositionWeight;  // 0,1������������ı��̶ֳȣ�1������ȫ����
    [SerializeField] [Range(0, 1)] private float rightFootPositionWeight;
    [SerializeField] [Range(0, 1)] private float leftFootRotationWeight;  
    [SerializeField] [Range(0, 1)] private float rightFootRotationWeight;

    [SerializeField] private Vector3 footOffset; //�����ڱ༭�������ֶ�����һ�½Ų�λ��

    [SerializeField] private Vector3 raycastLeftOffset;  //�����ľ���
    [SerializeField] private Vector3 raycastRightOffset;

    private void OnAnimatorIK(int layerIndex)   //IK�����������˶�ѧ���Ļص�����
    {
        Vector3 leftFootPosition = animator.GetIKPosition(AvatarIKGoal.LeftFoot);  // ��ȡ��ʱ��ŵ�IK����
        Vector3 RightFootPosition = animator.GetIKPosition(AvatarIKGoal.RightFoot);

        RaycastHit hitLeftFoot;  //���߼�⣺���Ƿ��̤�ڵ�����  ���Ǵӽ����·��������߲��������ľ���
        RaycastHit hitRightFoot;

        bool isLeftFootDown = Physics.Raycast(leftFootPosition + raycastLeftOffset, Vector3.down, out hitLeftFoot); // out�ؼ��֣���������ظ�ĳ���б���
        bool isRightFootDown = Physics.Raycast(RightFootPosition + raycastRightOffset, Vector3.down, out hitRightFoot);
        //Debug.Log("isRightFootDown: "+isRightFootDown.ToString());
        //Debug.Log("isLeftFootDown: " + isLeftFootDown.ToString());
        //����IK����Ӱ��λ��ƫ�Ƶı���ֵ��0��С1���
        CalculateLeftFoot(isLeftFootDown,hitLeftFoot);
        CalculateRightFoot(isRightFootDown, hitRightFoot);
    }

    void CalculateLeftFoot(bool isLeftFootDown, RaycastHit hitLeftFoot)
    {
        if (isLeftFootDown)
        {
            animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, leftFootRotationWeight);   // ���IK����ƽ���ر�Ϊ����õ�������
            animator.SetIKPosition(AvatarIKGoal.LeftFoot, hitLeftFoot.point + footOffset);
      
            //Debug.Log("LeftFootPos: " + hitLeftFoot.point.ToString());
            // �������Ŀ�곯�򣬲�ƽ���ر仯
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
            animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, rightFootRotationWeight);   // ���IK����ƽ���ر�Ϊ����õ�������
            animator.SetIKPosition(AvatarIKGoal.RightFoot, hitRightFoot.point + footOffset);

            // �������Ŀ�곯�򣬲�ƽ���ر仯
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
