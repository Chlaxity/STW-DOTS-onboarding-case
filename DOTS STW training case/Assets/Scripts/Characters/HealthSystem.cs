using Unity.Entities;
using UnityEngine;

public class HealthSystem : SystemBase
{
    EndSimulationEntityCommandBufferSystem ecbSystem;
    protected override void OnCreate()
    {
        ecbSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
    }

    protected override void OnUpdate()
    {
        var ecb = ecbSystem.CreateCommandBuffer().AsParallelWriter();
        
        Entities.ForEach((Entity entity, int entityInQueryIndex, DynamicBuffer<DamageBuffer> damageBuffer, ref HealthData health) =>
        {
            if(health.value <= 0)
            {
                Debug.Log($"entity{entity.Index } died");
                ecb.DestroyEntity(entityInQueryIndex, entity);
            }

            float damage = 0;
            for (int i = 0; i < damageBuffer.Length; i++)
            {
                damage += damageBuffer[i].value;
            }
            if (damage > 0)
            {
                Debug.Log($"entity{entity.Index } took {damage} damage");
                health.value -= damage;
            }

            damageBuffer.Clear();
        }).ScheduleParallel();
        
    }
}
