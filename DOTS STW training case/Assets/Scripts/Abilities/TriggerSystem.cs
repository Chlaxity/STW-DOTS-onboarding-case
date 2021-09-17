using Unity.Entities;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Mathematics;
using Unity.Burst;
using Unity.Jobs;
using Unity.Collections;

public class TriggerSystem : JobComponentSystem
{


     private StepPhysicsWorld stepPhysicsWorld;

     private EndSimulationEntityCommandBufferSystem commandBufferSystem;

     protected override void OnCreate()
     {
         base.OnCreate();
         stepPhysicsWorld = World.GetOrCreateSystem<StepPhysicsWorld>();
     }
    
     protected override JobHandle OnUpdate(JobHandle inputDeps)
     {
        //Assign values to the fields within the job
        var job = new AttackHitSystem()
        {
            targetBuffer = GetBufferFromEntity<TargetBuffer>(false),
            damageBuffer = GetBufferFromEntity<DamageBuffer>(false)
        };

        JobHandle jobHandle = job.Schedule(stepPhysicsWorld.Simulation,
             inputDeps);
        
        jobHandle.Complete();
         return jobHandle;
     }

     [BurstCompile]
     struct AttackHitSystem : ITriggerEventsJob
     {       
        //Define the fields we need for the job, such as the targetbuffer
         public BufferFromEntity<TargetBuffer> targetBuffer;
        public BufferFromEntity<DamageBuffer> damageBuffer;
         public void Execute(TriggerEvent triggerEvent)
         {
            //1. Determine which of the entites are the attack and which is the target. 
             Entity entityA = triggerEvent.EntityA;
             Entity entityB = triggerEvent.EntityB;

             Entity attack = targetBuffer.HasComponent(entityA) ? entityA :
                 targetBuffer.HasComponent(entityB) ? entityB : Entity.Null;

             Entity target = targetBuffer.HasComponent(entityA) ? entityB :
                 targetBuffer.HasComponent(entityB) ? entityA : Entity.Null;
             
            if (target == Entity.Null)
                 return;

            //2. Get a native array of the target buffer, and check if the target is already in the buffer. If it is, we can early out.
             var currentTargets = targetBuffer[attack].ToNativeArray(Allocator.Temp).Reinterpret<Entity>();
             if (currentTargets.Contains(target))
            {
                return;
            }

            //3. Add the target to the targetbuffers
            targetBuffer[attack].Add(new TargetBuffer { targets = target });
            damageBuffer[target].Add(new DamageBuffer { value = 10 });
             currentTargets.Dispose();


         }
     }
}
