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

     protected override void OnCreate()
     {
         base.OnCreate();
         stepPhysicsWorld = World.GetOrCreateSystem<StepPhysicsWorld>();
     }
    
     protected override JobHandle OnUpdate(JobHandle inputDeps)
     {
        var job = new AttackHitSystem()
        {
            //Assign values to the fields within the job
        };

        JobHandle jobHandle = job.Schedule(stepPhysicsWorld.Simulation,
             inputDeps);
        
        jobHandle.Complete();
         return jobHandle;
     }

     [BurstCompile]
     struct AttackHitSystem : ITriggerEventsJob
     {       
        //1Define the fields we need for the job, such as the targetbuffer

         public void Execute(TriggerEvent triggerEvent)
         {
            //2. Determine which of the entites are the attack and which is the target. 

            //3. Get a native array of the target buffer, and check if the target is already in the buffer. If it is, we can early out.

            //4. Add the target to the targetbuffers

         }
     }
}
