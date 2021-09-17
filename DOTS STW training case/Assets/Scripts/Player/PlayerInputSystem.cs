using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Unity.Transforms;

public class PlayerInputSystem : SystemBase
{
    Entity player;
    protected override void OnCreate()
    {
        RequireSingletonForUpdate<PlayerInputData>();
    }

    protected override void OnStartRunning()
    {
        player = GetSingletonEntity<PlayerInputData>();
    }

    protected override void OnUpdate()
    {
        var dirInput = new float2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        var pos = GetComponentDataFromEntity<Translation>(true)[player].Value;
        var charPos = Camera.main.WorldToScreenPoint(pos);
        var isAttacking = Input.GetAxis("Fire1") == 0 ? false : true;

        Entities.ForEach((ref PlayerInputData input) =>
        {
            input.moveDirection = new float2(dirInput);
            input.pointDirection = math.normalize(new float2(Input.mousePosition.x - charPos.x, Input.mousePosition.y - charPos.y));
            input.attack = isAttacking;

        }).Run();
    }
}
