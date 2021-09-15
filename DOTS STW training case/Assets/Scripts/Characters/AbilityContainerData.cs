using Unity.Entities;

public struct AbilityContainerData : IBufferElementData
{
    public Entity attack;
    public TimerData cooldown;

}
