using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Unity.Transforms;

public class PlayerInputSystem : SystemBase
{
    protected override void OnUpdate()
    {
        Entities.ForEach((Entity player, ref PlayerInputData input, in Translation pos) =>
        {
            input.moveDirection = new float2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            var charPos = Camera.main.WorldToScreenPoint(pos.Value);
            input.pointDirection = math.normalize(new float2(Input.mousePosition.x - charPos.x, Input.mousePosition.y - charPos.y));
            input.attack = Input.GetAxis("Fire1") == 0 ? false : true;

        }).Run();
    }
}
