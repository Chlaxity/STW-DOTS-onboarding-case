using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

public class TimerSystem : SystemBase
{
    protected override void OnUpdate()
    {
        var dt = Time.DeltaTime;

        Entities.ForEach((ref TimerData timer) => {
            if (timer.value > 0)
                timer.value -= dt;

        }).Schedule();

        Entities.ForEach((DynamicBuffer<AbilityContainerData> timer) =>
        {
            if (timer[0].cooldown.value > 0)
            {
                var t = timer[0];
                t.cooldown.value -= dt;
                timer[0] = t;
            }
        }).Schedule();
    }
}
