using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Physics;

public class AttackSystem : SystemBase
{

    EndSimulationEntityCommandBufferSystem ECBSystem;
    protected override void OnCreate()
    {
        ECBSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
    }

    protected override void OnUpdate()
    {
        var ecb = ECBSystem.CreateCommandBuffer();

        Entities.ForEach((Entity character, ref DynamicBuffer <AbilityContainerData> ability, ref PlayerInputData input, in Translation pos, in Rotation rot) =>
        {
            if (input.attack)
            {
                if (ability[0].cooldown.value > 0)
                {
                    //UnityEngine.Debug.Log("Still on cooldown!");
                    return;
                }
                var a = ability[0];
                a.cooldown.value = 0.6f;
                //Check cooldown
                ability[0] = a;
                
                var attack = ecb.Instantiate(ability[0].attack);
                ecb.SetComponent(attack, new Translation { Value = pos.Value });
                ecb.SetComponent(attack, new Rotation { Value = rot.Value });
                
            }
        }).Schedule();
        CompleteDependency();
        //Spawn effect
        //Find targets
        //Apply effect to targets
        
        

    }
}
