
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Collections;
using UnityEngine;


[UpdateInGroup(typeof(PresentationSystemGroup), OrderFirst = true)]
public partial struct PlayerAnimateSystem : ISystem
{

    public void OnUpdate(ref SystemState state)
    {
        EntityCommandBuffer entityCommandBuffer = new EntityCommandBuffer(Allocator.Temp);
     
        foreach (var (playerGameObjectPrefab, entity) in SystemAPI.Query<PlayerGameObjectPrefab>().WithNone<PlayerAnimatorReference>().WithEntityAccess())
        {
            GameObject newCompanionGameObject = Object.Instantiate(playerGameObjectPrefab.Value);
            newCompanionGameObject.GetComponent<RenderersProvider>().RandomizeColorOfRenderers();

            PlayerAnimatorReference newAnimatorReference = new PlayerAnimatorReference
            {
                Value = newCompanionGameObject.GetComponent<Animator>()
            };
            entityCommandBuffer.AddComponent(entity, newAnimatorReference);
        }

        foreach (var (transform, animatorReference, moveInput) in SystemAPI.Query<LocalTransform, PlayerAnimatorReference, NetcodePlayerInput>())
        {
            animatorReference.Value.SetBool("Walk", math.length(moveInput.inputVector) > 0f);
            animatorReference.Value.transform.position = transform.Position;
            animatorReference.Value.transform.rotation = transform.Rotation;
            if (moveInput.shoot.IsSet)
                animatorReference.Value.SetTrigger("Attack");
        }

        foreach (var (animatorReference, entity) in SystemAPI.Query<PlayerAnimatorReference>().WithNone<PlayerGameObjectPrefab, LocalTransform>().WithEntityAccess())
        {
            Object.Destroy(animatorReference.Value.gameObject);
            entityCommandBuffer.RemoveComponent<PlayerAnimatorReference>(entity);
        }

        entityCommandBuffer.Playback(state.EntityManager);
        entityCommandBuffer.Dispose();
    }
}

