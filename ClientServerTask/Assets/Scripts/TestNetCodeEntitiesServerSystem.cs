
using Unity.Burst;
using Unity.Entities;
using Unity.NetCode;

[WorldSystemFilter(WorldSystemFilterFlags.ServerSimulation)]
partial struct TestNetCodeEntitiesServerSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        
    }

 //   [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {

        EntityCommandBuffer entityCommandBuffer = new EntityCommandBuffer(Unity.Collections.Allocator.Temp);
        foreach ((
            RefRO<SimpleRPC> simpleRPC, 
            RefRO<ReceiveRpcCommandRequest> receiveRpcCommandRequest,
            Entity entity
            ) 
            in SystemAPI.Query<
                RefRO<SimpleRPC>, 
                RefRO<ReceiveRpcCommandRequest>>().WithEntityAccess()) 
        {
         
        //   UnityEngine.Debug.Log($"Received RPC with value: {simpleRPC.ValueRO.value}");
           entityCommandBuffer.DestroyEntity(entity); // Destroy the entity after processing the RPC
        }

        entityCommandBuffer.Playback(state.EntityManager);




    }

    [BurstCompile]
    public void OnDestroy(ref SystemState state)
    {
        
    }
}
