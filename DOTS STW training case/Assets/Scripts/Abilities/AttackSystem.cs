using Unity.Entities;

public class AttackSystem : SystemBase
{

    EndSimulationEntityCommandBufferSystem ECBSystem;
    protected override void OnCreate()
    {
        ECBSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
    }

    protected override void OnUpdate()
    {

        //1. Spawn effect when attack button is pressed.

        //2. Ensure the attack is only spawned when it is not on cooldown. (See task 5)     
     }
}
