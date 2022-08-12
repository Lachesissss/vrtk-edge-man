using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using FusedVR.VRStreaming;


public class Move : MonoBehaviour
{
    public float ThumbStickMoveX=0f;
    public float ThumbStickMoveY=0f;
    public Transform LocalAvatar;
    public LayerMask groundLayer;
    public float gravity = -9.81f;
    private float fallingSpeed;
    private CharacterController character;
    public float moveSpeed = 1f;
    [SerializeField]  public float oculusHeightOffset;
    private Vector3 direction = Vector3.zero;
    [SerializeField] private Vector3 footOffset; //用于在编辑器里面手动调整一下脚步位置


    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>();
        
    }

    // Update is called once per frame

    public void ApplyData(ControllerInputManager.Hand id, ControllerInputManager.Button handID, float x, float y)
    {
        ThumbStickMoveX = x;
        ThumbStickMoveY = y;
        //Debug.Log(x.ToString()+' '+ y.ToString());
    }

    private void Update()
    {
        
    }
    private void FixedUpdate()
    {
        CapsuleFollowHeadSet();
        //Debug.Log(direction.x.ToString() + ' ' + direction.y.ToString());

        //Vector3 leftFootPosition = LeftFoot.position;
        //Vector3 rightFootPosition = RightFoot.position;
        //RaycastHit hitLeftFoot;  //射线检测：脚是否踩踏在地面上  我们从脚向下方发射射线测量与地面的距离
        //RaycastHit hitRightFoot;
        //bool isLeftFootDown = (leftFootPosition.y+ raycastLeftOffset.y) <= 0;
        //bool isRightFootDown = (rightFootPosition.y + raycastRightOffset.y) <= 0;
        //Debug.Log(isRightFootDown.ToString() + ' ' + isLeftFootDown.ToString());

        Transform HeadCamera = transform.Find("Head");
        Quaternion headYaw = Quaternion.Euler(0, HeadCamera.eulerAngles.y, 0);

        direction = headYaw * new Vector3(ThumbStickMoveX, 0f, -ThumbStickMoveY);
        /*
        if (LocalAvatar.position.y+footOffset.y > 0.017)
        {
            direction.y -= gravity * Time.deltaTime;
        }
        else
        {
            direction.y = 0f;
        }*/
        //Move方法需要自己写重力效果
        character.Move(direction * Time.fixedDeltaTime * moveSpeed);

        //if (LocalAvatar.position.y< 0.017)
        //{
            //Debug.Log("y<0.017");
            //character.Move(new Vector3(0f, oculusHeightOffset, 0f) * Time.fixedDeltaTime * moveSpeed);
        //}
            

        //gravity
        bool isGrounded = CheckIfGrounded();
        if (isGrounded)
        {
            Debug.Log("isGround");
            fallingSpeed = 0;
        }
        else
        {
            Debug.Log("NotGround");
            fallingSpeed += gravity * Time.fixedDeltaTime;
        }
        character.Move(Vector3.up * fallingSpeed * Time.fixedDeltaTime);
    }

    void CapsuleFollowHeadSet()
    {
        Vector3 capsuleCenter = transform.InverseTransformPoint(transform.Find("Head").position);
        character.center = new Vector3(capsuleCenter.x, character.height/2 + character.skinWidth, capsuleCenter.z);
        character.transform.position = transform.position;
    }

    bool CheckIfGrounded()
    {
        Vector3 rayStart = transform.TransformPoint(character.center);
        float rayLength = character.center.y + 0.01f;
        bool hasHit = Physics.SphereCast(rayStart, character.radius, Vector3.down, out RaycastHit hitInfo, rayLength, groundLayer);
        Debug.Log(hitInfo.point.ToString());
        return hasHit;
    }

}
