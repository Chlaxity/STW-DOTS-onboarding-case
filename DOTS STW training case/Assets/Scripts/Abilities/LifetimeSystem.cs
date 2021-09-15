using Unity.Entities;

public class LifetimeSystem : SystemBase
{
    EndSimulationEntityCommandBufferSystem ecbSystem;

    protected override void OnCreate()
    {
        ecbSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
    }


    protected override void OnUpdate()
    {
        var ecb = ecbSystem.CreateCommandBuffer().AsParallelWriter();

        Entities.WithAll<DestroyOnTimerTag>().ForEach((Entity e, int entityInQueryIndex ,  in TimerData timer) =>
        {
            if (timer.value <= 0)
                ecb.DestroyEntity(entityInQueryIndex, e);
        }).ScheduleParallel();
        CompleteDependency();
    }
}
