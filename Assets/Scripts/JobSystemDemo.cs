using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Unity.Collections;
using Unity.Jobs;  //注意：使用JobSystem不能用C#自带的数组，需要使用Unity容器

public class JobSystemDemo : MonoBehaviour
{
    public Camera mainCamera;

    public int instanceCount;
    public Vector3Int instanceExtents = new Vector3Int(500, 500, 500);
    public float randomMaxScaleValue = 5;

    public NativeArray<float4x4> inputs; // 一个容器，用于存储4×4矩阵代表每个对象的变换矩阵,每创建一个JobSystem容器，都需要手动管理声明周期 

    private Bounds meshBounds; //包围盒
    private float4[] FrustumPlanesArray = new float4[6]; // 一个数组储存6个视锥平面，4维向量代表一个平面
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
        inputs = new NativeArray<float4x4>(instanceCount, Allocator.Persistent); // 初始化容器，内存分配策略为持久，需要在OnDestory里手动释放
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
            inputs[i] = Matrix4x4.TRS(pos, rot, scale);  // 基于pos，rot，scale构造4×4矩阵
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDestroy()
    {
        if (inputs.IsCreated) // 手动释放inputs容器
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
