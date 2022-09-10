using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class DisplayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public NetworkIdentity itemDisplayerIdentity;

    public Vector3 rotSpeed;
    public Transform rotationDragger;
    public Transform scaleDragger;

    private bool isRotationDraggerDragging = false;
    private bool isItemGrabbing = false;
    private int grabbingHands = 0;
    private Vector3 rotTemp;  // 保存拖拽滑杆前的角度
    private Vector3 posTemp2; // 保存抓取前的位置和角度
    private Quaternion rotTemp2;
    private float rotValueTemp;

    void Start()
    {
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        if (!isItemGrabbing)
        {
            if (!isRotationDraggerDragging)
            {
                transform.localRotation = Quaternion.Euler(rotSpeed.x, rotSpeed.y, rotSpeed.z) * transform.localRotation;
            }
            else
            {
                float rotValue = (rotationDragger.localPosition.x + 0.5f) * 360;
                transform.localRotation = Quaternion.Euler(rotTemp.x, (rotTemp.y + rotValue - rotValueTemp) % 360, rotTemp.z);
            }
            float scaleValue = (scaleDragger.localPosition.x + 1f);
            transform.localScale = new Vector3(scaleValue, scaleValue, scaleValue);
        }
        //Debug.Log(transform.localRotation);
    }

    public void OnRotationDraggerDraged()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("NetworkAvatar");
        var player = FindPlayer(players);
        if (player)
        {
            player.OnRequestAuthority(itemDisplayerIdentity);
        }
        else
        {
            Debug.LogWarning("player == null");
        }


        isRotationDraggerDragging = true;
        rotTemp = transform.localEulerAngles;
        rotValueTemp = (rotationDragger.localPosition.x + 0.5f) * 360;
    }

    public void OnRotationDraggerUnDraged()
    {
        isRotationDraggerDragging = false;
    }

    public void OnScaleDraggerDraged()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("NetworkAvatar");
        var player = FindPlayer(players);
        if (player)
        {
            player.OnRequestAuthority(itemDisplayerIdentity);
        }
        else
        {
            Debug.LogWarning("player == null");
        }
    }

    public void OnItemGrabbed()
    {

        grabbingHands++;
        Debug.Log("hands = " + grabbingHands.ToString());
        if (grabbingHands == 1)
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("NetworkAvatar");
            var player = FindPlayer(players);
            if (player)
            {
                player.OnRequestAuthority(itemDisplayerIdentity);
            }
            else
            {
                Debug.LogWarning("player == null");
            }
            posTemp2 = transform.localPosition;
            rotTemp2 = transform.localRotation;
            isItemGrabbing = true;
        }
    }

    public void OnItemUngrabbed()
    {
        grabbingHands--;
        Debug.Log("hands = " + grabbingHands.ToString());
        if (grabbingHands == 0)
        {
            isItemGrabbing = false;
            transform.localPosition = posTemp2;
            transform.localRotation = rotTemp2;
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

    }

    private NetworkAvatar FindPlayer(GameObject[] players)
    {
        NetworkAvatar networkAvatar = null;
        foreach (var player in players)
        {
            if (player.GetComponent<NetworkAvatar>().isLocalPlayer)
            {
                networkAvatar = player.GetComponent<NetworkAvatar>();
                break;
            }
        }
        return networkAvatar;
    }
}
