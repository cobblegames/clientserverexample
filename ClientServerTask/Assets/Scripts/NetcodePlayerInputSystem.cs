using Unity.Burst;
using Unity.Entities;
using Unity.NetCode;
using Unity.Mathematics;
using UnityEngine;

[UpdateInGroup(typeof(GhostInputSystemGroup))]
internal partial struct NetcodePlayerInputSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<NetworkStreamInGame>();
        state.RequireForUpdate<NetcodePlayerInput>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        foreach (RefRW<NetcodePlayerInput> netcodePlayerInput in SystemAPI.Query<RefRW<NetcodePlayerInput>>().WithAll<GhostOwnerIsLocal>())
        {
            float2 inputVector = new float2();
            Quaternion rotation = Quaternion.identity;
            if (UnityEngine.Input.GetKey(UnityEngine.KeyCode.W))
            {
                Debug.Log("W Key Pressed");
                inputVector.y += 1f;
                rotation = Quaternion.Euler(0, 0, 0);
            }
            if (UnityEngine.Input.GetKey(UnityEngine.KeyCode.S))
            {
                Debug.Log("S Key Pressed");
                inputVector.y -= 1f;
                rotation = Quaternion.Euler(0, 180, 0);
            }
            if (UnityEngine.Input.GetKey(UnityEngine.KeyCode.A))
            {
                Debug.Log("A Key Pressed");
                inputVector.x -= 1f;
                rotation = Quaternion.Euler(0, 270, 0);
            }
            if (UnityEngine.Input.GetKey(UnityEngine.KeyCode.D))
            {
                UnityEngine.Debug.Log("D Key Pressed");
                inputVector.x += 1f;
                rotation = Quaternion.Euler(0, 90, 0);
            }

            if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Space))
            {
                netcodePlayerInput.ValueRW.shoot.Set();
            }
            else
            {
                netcodePlayerInput.ValueRW.shoot = default;
            }

            netcodePlayerInput.ValueRW.inputVector = inputVector;
            netcodePlayerInput.ValueRW.rotation = rotation;
        }
    }
}