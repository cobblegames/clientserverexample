using UnityEngine;
using Unity.Entities;


public class PlayerAuthoring : MonoBehaviour
{

    public GameObject PlayerGameObjectPrefab;
    public class Baker : Baker<PlayerAuthoring>
    {      

        public override void Bake(PlayerAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponentObject(entity, new PlayerGameObjectPrefab {Value = authoring.PlayerGameObjectPrefab });
         
        }
    }
}
public class PlayerGameObjectPrefab : IComponentData
{
    public GameObject Value;
}

public class PlayerAnimatorReference : ICleanupComponentData
{
    public Animator Value;
}
