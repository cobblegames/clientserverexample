using Unity.Entities;
using UnityEngine;

public class BulletAuthoring : MonoBehaviour
{
   public class Baker: Baker<BulletAuthoring>
    {
        public override void Bake(BulletAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new Bullet {Timer = 0.5f });
        }
    }
}

public struct Bullet : IComponentData
{
    public float Timer;
}
