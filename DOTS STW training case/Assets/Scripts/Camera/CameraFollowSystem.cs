using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public class CameraFollowSystem : SystemBase
{
    Entity player;

    protected override void OnCreate()
    {
        //We want to ensure this system only runs if we have an instance of the PlayerInputData component.
        RequireSingletonForUpdate<PlayerInputData>();
    }

    protected override void OnStartRunning()
    {
        //We need to get the player entity here. Since we only have one player in this game, we can get it as singleton based on a player-only component
        player = GetSingletonEntity<PlayerInputData>();
    }

    protected override void OnUpdate()
    {
        //We want to continously get the translation from the player entity and update the CameraMovement's target to be the player's position.
        var translations = GetComponentDataFromEntity<Translation>(true);
        CameraMovement.instance.SetTarget(translations[player].Value);
    }
}
