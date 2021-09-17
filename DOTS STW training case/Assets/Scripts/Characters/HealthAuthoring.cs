using Unity.Entities;
using UnityEngine;

public class HealthAuthoring : MonoBehaviour, IConvertGameObjectToEntity
{
    public float health = 50;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddBuffer<DamageBuffer>(entity);
        dstManager.AddComponentData(entity, new HealthData { value = health });
    }
}

public struct HealthData : IComponentData
{
    public float value;
}

public struct DamageBuffer : IBufferElementData
{
    public float value;
}
