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
    }
}
