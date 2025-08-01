using UnityEngine;
using Unity.NetCode;
using Unity.Mathematics;
using Unity.Entities;

public class NetcodePlayerInputAuthoring : MonoBehaviour
{
    public class Baker : Baker<NetcodePlayerInputAuthoring>
    {
        public override void Bake(NetcodePlayerInputAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new NetcodePlayerInput());
        }
    }
}

public struct NetcodePlayerInput : IInputComponentData
{
    public Quaternion rotation;
    public float2 inputVector;
    public InputEvent shoot;
}