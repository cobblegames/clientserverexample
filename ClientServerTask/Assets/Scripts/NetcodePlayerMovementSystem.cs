using Unity.Burst;
using Unity.Entities;
using Unity.NetCode;
using Unity.Transforms;
using Unity.Mathematics;
using System.Numerics;

[UpdateInGroup(typeof(PredictedSimulationSystemGroup))]
internal partial struct NetcodePlayerMovementSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        foreach ((RefRO<NetcodePlayerInput> netcodePlayerInput, RefRW<LocalTransform> localTransform) in SystemAPI.Query<RefRO<NetcodePlayerInput>, RefRW<LocalTransform>>().WithAll<Simulate>())
        {
            float moveSpeed = 5f;
            float3 moveVector = new float3(netcodePlayerInput.ValueRO.inputVector.x, 0f, netcodePlayerInput.ValueRO.inputVector.y);

            localTransform.ValueRW.Position += moveVector * moveSpeed * SystemAPI.Time.DeltaTime;
            localTransform.ValueRW.Rotation = netcodePlayerInput.ValueRO.rotation;
        }
    }
}