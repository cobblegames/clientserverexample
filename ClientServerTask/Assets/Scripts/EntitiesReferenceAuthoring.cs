using UnityEngine;
using Unity.Entities;
public class EntitiesReferenceAuthoring : MonoBehaviour
{
    public GameObject playerPrefabGameObject;
    public GameObject bulletPrefabGameObject;
    public class Baker : Baker<EntitiesReferenceAuthoring>
    {
        public override void Bake(EntitiesReferenceAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new EntitiesReference
            {
                playerPrefabEntity = GetEntity(authoring.playerPrefabGameObject, TransformUsageFlags.Dynamic),
                bulletPrefabEntity = GetEntity(authoring.bulletPrefabGameObject, TransformUsageFlags.Dynamic)
            });
        }
    }
}

public struct EntitiesReference : IComponentData
{
    public Entity playerPrefabEntity;
    public Entity bulletPrefabEntity;
}
