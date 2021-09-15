using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

[DisallowMultipleComponent]
public class EffectAuthoring : MonoBehaviour, IConvertGameObjectToEntity
{
    public float timer = 1;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new TimerData { time = timer, value = timer });
        dstManager.AddComponent<DestroyOnTimerTag>(entity);
    }
}
