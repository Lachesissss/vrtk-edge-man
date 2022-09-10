using UnityEngine;
using Unity.Jobs;
using Unity.Collections;
using Unity.Mathematics;
using Unity.Burst;

[BurstCompile(CompileSynchronously = true)]
public struct FustumCulling : IJobParallelFor  //IJobParallelFor 支持并发
{
    [ReadOnly] public NativeArray<float4x4> inputs;
    [ReadOnly] public NativeArray<float4x4> cameraPlanes;
    [ReadOnly] public float3 boxCenter;
    [ReadOnly] public float3 boxExents;

    public NativeArray<int> outputCount;
    public NativeArray<float4x4> outputs;

    public void Execute(int index)
    {

    }
}
