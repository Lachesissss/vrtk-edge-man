using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Unity.Collections;
using Unity.Jobs;  //ע�⣺ʹ��JobSystem������C#�Դ������飬��Ҫʹ��Unity����

public class JobSystemDemo : MonoBehaviour
{
    public Camera mainCamera;

    public int instanceCount;
    public Vector3Int instanceExtents = new Vector3Int(500, 500, 500);
    public float randomMaxScaleValue = 5;

    public NativeArray<float4x4> inputs; // һ�����������ڴ洢4��4�������ÿ������ı任����,ÿ����һ��JobSystem����������Ҫ�ֶ������������� 

    private Bounds meshBounds; //��Χ��
    private float4[] FrustumPlanesArray = new float4[6]; // һ�����鴢��6����׶ƽ�棬4ά��������һ��ƽ��
    private NativeArray<float4x4> FrustumPlanes = new NativeArray<float4x4>();
    private NativeArray<int> outputCount;
    private NativeArray<float4x4> outputs;

    // Start is called before the first frame update
    void Start()
    {
        RandomGeneratedInstances(instanceCount, instanceExtents, randomMaxScaleValue);
    }

    private void RandomGeneratedInstances(int instanceCount, Vector3Int instanceExtents, float maxScale)
    {
        inputs = new NativeArray<float4x4>(instanceCount, Allocator.Persistent); // ��ʼ���������ڴ�������Ϊ�־ã���Ҫ��OnDestory���ֶ��ͷ�
        Vector3 cameraPos = mainCamera.transform.position;
        for (int i = 0; i < instanceCount; i++)
        {
            Vector3 pos = new Vector3(UnityEngine.Random.Range(-instanceExtents.x, instanceExtents.x),
                                      UnityEngine.Random.Range(-instanceExtents.y, instanceExtents.y),
                                      UnityEngine.Random.Range(-instanceExtents.z, instanceExtents.z));
            Quaternion rot = Quaternion.Euler(UnityEngine.Random.Range(0, 180),
                                              UnityEngine.Random.Range(0, 180),
                                              UnityEngine.Random.Range(0, 180));
            Vector3 scale = new Vector3(UnityEngine.Random.Range(0.1f, maxScale),
                                        UnityEngine.Random.Range(0.1f, maxScale),
                                        UnityEngine.Random.Range(0.1f, maxScale));
            inputs[i] = Matrix4x4.TRS(pos, rot, scale);  // ����pos��rot��scale����4��4����
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDestroy()
    {
        if (inputs.IsCreated) // �ֶ��ͷ�inputs����
        {
            inputs.Dispose();
        }
        if (FrustumPlanes.IsCreated)
        {
            FrustumPlanes.Dispose();
        }
        if (outputCount.IsCreated)
        {
            outputCount.Dispose();
        }
        if (outputs.IsCreated)
        {
            outputs.Dispose();
        }
    }
}
