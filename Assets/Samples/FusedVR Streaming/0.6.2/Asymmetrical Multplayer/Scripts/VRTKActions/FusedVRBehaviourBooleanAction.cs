namespace FusedVR.VRStreaming.VRTK
{
    using UnityEngine;
    using Zinnia.Action;
    using static FusedVR.VRStreaming.ControllerInputManager;
    using static FusedVR.VRStreaming.VRInputManager;

    /// <summary>
    /// Listens for the linked boolean behavior and emits the appropriate action.
    /// </summary>
    public class FusedVRBehaviourBooleanAction : BooleanAction
    {
        [Tooltip("Manager to Listen for Input")]
        public ControllerInputManager controllerinputManager;


        [Tooltip("Which hand should this behavior listen to")]
        public Hand myHand;


        [Tooltip("Which button should this behavior listen to")]
        public Button myButton;

        [Tooltip("Which HandTrackingBoolEvent should this behavior listen to")]
        public HandTrackingBoolEventType myHandTrackingBoolEventType;


        private new void Start()
        {
            controllerinputManager.VRButtonEvent.AddListener(OnRemoteUpdateFromController);
            controllerinputManager.VRHandTrackingBoolEvent.AddListener(OnRemoteUpdateFromHandTracking);
        }

        private void OnDestroy()
        {
            controllerinputManager.VRButtonEvent.RemoveListener(OnRemoteUpdateFromController);
            controllerinputManager.VRHandTrackingBoolEvent.RemoveListener(OnRemoteUpdateFromHandTracking);
        }

        public void OnRemoteUpdateFromController(Hand hand, Button button, bool pressed, bool touched)
        {
            if (hand == myHand && button == myButton)
            {
                Receive(pressed); // if my hand & button is pressed, then send data to VRTK
            }
        }

        public void OnRemoteUpdateFromHandTracking(Hand hand, HandTrackingBoolEventType Event, bool Enabled)
        {
            Debug.Log(hand.ToString() + ' ' + Event.ToString());
            if (hand == myHand && Event == myHandTrackingBoolEventType)
            {
                Receive(Enabled); // if my hand & button is pressed, then send data to VRTK
                Debug.Log("Receive Fist:" + Enabled);
            }
        }
    }
}