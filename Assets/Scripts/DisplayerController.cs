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
    private Vector3 rotTemp;

    void Start()
    {

    }

    // Update is called once per frame
    public void FixedUpdate()
    {

        if (!isRotationDraggerDragging)
        {
            transform.localRotation = Quaternion.Euler(rotSpeed.x, rotSpeed.y, rotSpeed.z) * transform.localRotation;
        }
        else
        {
            float rotValue = (rotationDragger.localPosition.x + 0.5f) * 360;
            transform.localRotation = Quaternion.Euler(rotTemp.x, (rotTemp.y + rotValue) % 360, rotTemp.z);
        }
        float scaleValue = (scaleDragger.localPosition.x + 1f);
        transform.localScale = new Vector3(scaleValue, scaleValue, scaleValue);
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
