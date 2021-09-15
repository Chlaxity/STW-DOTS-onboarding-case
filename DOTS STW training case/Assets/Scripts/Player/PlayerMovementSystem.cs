using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;
public class PlayerMovementSystem : SystemBase
{
    protected override void OnUpdate()
    {
        
        //TODO: Remove and make a task



        var dt = Time.DeltaTime;
        Entities.ForEach((Entity player, ref Translation trans, ref Rotation rot, in PlayerInputData input, in MovementData speed) =>
        {
            
            if (math.lengthsq(input.moveDirection) != 0)
            {
                float2 dir = math.normalize(input.moveDirection);
                trans.Value.xz += dir * dt * speed.speed;
            }
            rot.Value = quaternion.LookRotation(new float3(input.pointDirection.x,0,input.pointDirection.y), new float3(0, 1, 0));

        }).Schedule();
    }
}
