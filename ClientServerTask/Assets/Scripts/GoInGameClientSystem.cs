using System.Diagnostics;
using Unity.Burst;
using Unity.Entities;
using Unity.NetCode;


[WorldSystemFilter(WorldSystemFilterFlags.ClientSimulation)]
partial struct GoInGameClientSystem : ISystem
{

    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<EntitiesReference>();
        state.RequireForUpdate<NetworkId>();
    }
    public void OnUpdate(ref SystemState state)
    {
        EntityCommandBuffer entityCommandBuffer = new EntityCommandBuffer(Unity.Collections.Allocator.Temp);

        foreach ((RefRO<NetworkId> networkId, Entity entity) in  SystemAPI.Query<RefRO<NetworkId>>().WithNone<NetworkStreamInGame>().WithEntityAccess())
        {


            entityCommandBuffer.AddComponent<NetworkStreamInGame>(entity); // Add NetworkStreamInGame component to the entity
            UnityEngine.Debug.Log("Setting client as InGame");

            Entity rpcEntity = entityCommandBuffer.CreateEntity(); // Create a new entity for the RPC
            entityCommandBuffer.AddComponent(rpcEntity, new GoInGameRequestRPC());
            entityCommandBuffer.AddComponent(rpcEntity, new SendRpcCommandRequest());

        }

        entityCommandBuffer.Playback(state.EntityManager); // Apply the changes to the EntityManager
     
    }   
}

public struct  GoInGameRequestRPC : IRpcCommand
{
    
}
