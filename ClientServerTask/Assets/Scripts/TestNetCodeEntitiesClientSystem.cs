using Unity.Burst;
using Unity.Entities;
using UnityEngine;
using Unity.NetCode;    

[WorldSystemFilter(WorldSystemFilterFlags.ClientSimulation)]
partial struct TestNetCodeEntitiesClientSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        
    }

   // [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
         Entity rpcEntity  =  state.EntityManager.CreateEntity();
            state.EntityManager.AddComponentData(rpcEntity, new SimpleRPC
            {
                value = Random.Range(0, 100)
            });

            state.EntityManager.AddComponentData(rpcEntity, new SendRpcCommandRequest()); // send rpc to server
            Debug.Log($"Sent RPC with value: {state.EntityManager.GetComponentData<SimpleRPC>(rpcEntity).value}");
        }
    }

    [BurstCompile]
    public void OnDestroy(ref SystemState state)
    {
        
    }
}
