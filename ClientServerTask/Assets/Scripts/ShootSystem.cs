using Unity.Burst;
using Unity.Entities;
using Unity.NetCode;
[UpdateInGroup(typeof(PredictedSimulationSystemGroup))]
partial struct ShootSystem : ISystem
{
  
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        NetworkTime networkTime = SystemAPI.GetSingleton<NetworkTime>();

        foreach (RefRW<NetcodePlayerInput> netcodePlayerInput in SystemAPI.Query<RefRW<NetcodePlayerInput>>().WithAll<GhostOwnerIsLocal>().WithAll<Simulate>())
        {
            if( networkTime.IsFirstTimeFullyPredictingTick)
            {
                if (netcodePlayerInput.ValueRW.shoot.IsSet)
                {

                    UnityEngine.Debug.Log("Shoot action triggered" + state.World);
                }
            }
           
        }

    }

}
