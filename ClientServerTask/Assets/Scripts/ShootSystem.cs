using Unity.Burst;
using Unity.Entities;
using Unity.NetCode;
using Unity.Transforms;
[UpdateInGroup(typeof(PredictedSimulationSystemGroup))]
partial struct ShootSystem : ISystem
{
  
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        NetworkTime networkTime = SystemAPI.GetSingleton<NetworkTime>();
        EntitiesReference entitiesReference = SystemAPI.GetSingleton<EntitiesReference>();

        EntityCommandBuffer entityCommandBuffer = new EntityCommandBuffer(Unity.Collections.Allocator.Temp);
        foreach ((
                RefRO<NetcodePlayerInput> netcodePlayerInput,
                RefRO<LocalTransform> localTransform,
                RefRO < GhostOwner > ghostOwner) 
            in SystemAPI.Query<RefRO<NetcodePlayerInput>, RefRO<LocalTransform>, RefRO<GhostOwner>>().WithAll<Simulate>())
        {
            if( networkTime.IsFirstTimeFullyPredictingTick)
            {
                if (netcodePlayerInput.ValueRO.shoot.IsSet)
                {

                    UnityEngine.Debug.Log("Shoot action triggered" + state.World);
                    Entity bulletEntity = entityCommandBuffer.Instantiate(entitiesReference.bulletPrefabEntity);
                    entityCommandBuffer.SetComponent(bulletEntity, LocalTransform.FromPosition(localTransform.ValueRO.Position));
                    entityCommandBuffer.SetComponent(bulletEntity, new GhostOwner { NetworkId = ghostOwner.ValueRO.NetworkId });
                }
            }           
        }
        entityCommandBuffer.Playback(state.EntityManager);
    }

}
