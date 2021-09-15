using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct PlayerInputData : IComponentData
{
    public float2 moveDirection;
    public float2 pointDirection;
    public bool attack;
}
