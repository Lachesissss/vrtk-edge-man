using UnityEngine.InputSystem;
using UnityEngine;
using Zinnia.Action;
using System;
using FusedVR.VRStreaming;

public class AnimationController : MonoBehaviour
{
    //[SerializeField] private BooleanAction grip; 之后写个update函数，grip=true时就播放动画。。。  
    [SerializeField] private InputActionReference move;
    [SerializeField] private InputActionReference LeftHandTrigger;
    [SerializeField] private InputActionReference RightHandTrigger;
    [SerializeField] private InputActionReference LeftHandGrip;
    [SerializeField] private InputActionReference RightHandGrip;

    [SerializeField] private Animator animator;

    public float ThumbStickMoveX = 0f;
    public float ThumbStickMoveY = 0f;

    private bool StartLeftTrigger=false;
    private bool StartRightTrigger = false;
    private bool StartLeftGrip = false;
    private bool StartRightGrip = false;

    private void OnEnable()
    {
        move.action.started += Animatelegs;
        move.action.canceled += StopAnimation;
        LeftHandTrigger.action.started += LeftHandTriggerStart;
        LeftHandTrigger.action.canceled += LeftHandTriggerEnd;
        RightHandTrigger.action.started += RightHandTriggerStart;
        RightHandTrigger.action.canceled += RightHandTriggerEnd;
        LeftHandGrip.action.started += LeftHandGripStart;
        LeftHandGrip.action.canceled += LeftHandGripEnd;
        RightHandGrip.action.started += RightHandGripStart;
        RightHandGrip.action.canceled += RightHandGripEnd;

    }

    private void OnDisable()
    {
        move.action.started -= Animatelegs;
        move.action.canceled -= StopAnimation;
        LeftHandTrigger.action.started -= LeftHandTriggerStart;
        LeftHandTrigger.action.canceled -= LeftHandTriggerEnd;
        RightHandTrigger.action.started -= RightHandTriggerStart;
        RightHandTrigger.action.canceled += RightHandTriggerEnd;
        LeftHandGrip.action.started -= LeftHandGripStart;
        LeftHandGrip.action.canceled -= LeftHandGripEnd;
        RightHandGrip.action.started -= RightHandGripStart;
        RightHandGrip.action.canceled -= RightHandGripEnd;
    }

    private void RightHandGripStart(InputAction.CallbackContext obj)
    {
        StartRightGrip = true;
    }

    private void LeftHandGripStart(InputAction.CallbackContext obj)
    {
        StartLeftGrip = true;
    }

    private void RightHandTriggerStart(InputAction.CallbackContext obj)
    {
        StartRightTrigger = true;
    }

    private void LeftHandTriggerStart(InputAction.CallbackContext obj)
    {
        StartLeftTrigger = true;
    }
    private void RightHandGripEnd(InputAction.CallbackContext obj)
    {
        StartRightGrip = false;
    }

    private void LeftHandGripEnd(InputAction.CallbackContext obj)
    {
        StartLeftGrip = false;
    }

    private void RightHandTriggerEnd(InputAction.CallbackContext obj)
    {
        StartRightTrigger = false;
    }

    private void LeftHandTriggerEnd(InputAction.CallbackContext obj)
    {
        StartLeftTrigger = false;
    }

    private void Animatelegs(InputAction.CallbackContext obj)
    {
        bool isMovingForward = ThumbStickMoveY < 0;
        if (isMovingForward)
        {
            animator.SetBool("isWalking",true);
            animator.SetFloat("animSpeed", 1);
        }
        else
        {
            animator.SetBool("isWalking", true);
            animator.SetFloat("animSpeed", -1);
        }
    }

    private void StopAnimation(InputAction.CallbackContext obj)
    {
        animator.SetBool("isWalking", false);
        Debug.Log("False2");
        animator.SetFloat("animSpeed", 0);
    }

    private void Update()
    {
        if (StartLeftTrigger)
        {
            animator.SetFloat("Trigger_Left", LeftHandTrigger.action.ReadValue<float>());
        }
        if (StartRightTrigger)
        {
            animator.SetFloat("Trigger_Right", RightHandTrigger.action.ReadValue<float>());
        }
        if (StartLeftGrip)
        {
            animator.SetFloat("Grip_Left", LeftHandGrip.action.ReadValue<float>());
        }
        if (StartRightGrip)
        {
            animator.SetFloat("Grip_Right", RightHandGrip.action.ReadValue<float>());
        }

    }

    public void ApplyData(ControllerInputManager.Hand id, ControllerInputManager.Button handID, float x, float y)
    {
        //float ThumbStickMoveXTemp = ThumbStickMoveX;
        //float ThumbStickMoveYTemp = ThumbStickMoveY;
        ThumbStickMoveX = x;
        ThumbStickMoveY = y;
        Debug.Log(ThumbStickMoveX.ToString()+' '+ThumbStickMoveY.ToString());
        if (ThumbStickMoveY < 0)
        {
            animator.SetBool("isWalking", true);
            animator.SetFloat("animSpeed", 1);
        }
        else if (ThumbStickMoveY > 0)
        {
            animator.SetBool("isWalking", true);
            animator.SetFloat("animSpeed", -1);
        }
        else
        {
            //if(ThumbStickMoveYTemp == 0)
                animator.SetBool("isWalking", false);
        }
    }

    public void ApplyHandAnimation(ControllerInputManager.Hand HandID, ControllerInputManager.Button buttonID,bool pressed, bool touched)
    {
        //Debug.Log(pressed.ToString() + ' ' + touched.ToString());
        if (HandID == ControllerInputManager.Hand.Left)
        {
            if (buttonID == ControllerInputManager.Button.Trigger)
            {
                if (pressed)
                {
                    animator.SetFloat("Trigger_Left", 1f);
                }
                else if (touched)
                {
                    animator.SetFloat("Trigger_Left", 0.5f);
                }
                else
                {
                    animator.SetFloat("Trigger_Left", 0f);
                }
            }
            if (buttonID == ControllerInputManager.Button.Grip)
            {
                if (pressed)
                {
                    animator.SetFloat("Grip_Left", 1f);
                }
                else if (touched)
                {
                    animator.SetFloat("Grip_Left", 0.5f);
                }
                else
                {
                    animator.SetFloat("Grip_Left", 0f);
                }
            }
        }
        if (HandID == ControllerInputManager.Hand.Right)
        {
            if (buttonID == ControllerInputManager.Button.Trigger)
            {
                if (pressed)
                {
                    animator.SetFloat("Trigger_Right", 1f);
                }
                else if (touched)
                {
                    animator.SetFloat("Trigger_Right", 0.5f);
                }
                else
                {
                    animator.SetFloat("Trigger_Right", 0f);
                }
            }
            if (buttonID == ControllerInputManager.Button.Grip)
            {
                if (pressed)
                {
                    animator.SetFloat("Grip_Right", 1f);
                }
                else if (touched)
                {
                    animator.SetFloat("Grip_Right", 0.5f);
                }
                else
                {
                    animator.SetFloat("Grip_Right", 0f);
                }
            }
        }
    }
}
