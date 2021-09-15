using Unity.Entities;
using UnityEngine;
using System;
using System.Collections.Generic;

public class AbilityAuthoring : MonoBehaviour, IConvertGameObjectToEntity, IDeclareReferencedPrefabs
{
    [Serializable]
    public struct ability
    {
        public GameObject abilityPrefab;
        public float cooldown;
    }

    public ability[] abilities;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        var buffer = dstManager.AddBuffer<AbilityContainerData>(entity);

        foreach (var item in abilities)
        {
            buffer.Add(new AbilityContainerData { attack = conversionSystem.GetPrimaryEntity(item.abilityPrefab), cooldown = new TimerData { time = item.cooldown, value = 0 } });
        }
    }

    public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
    {
        foreach (var item in abilities)
        {
            referencedPrefabs.Add(item.abilityPrefab);
        }
    }
}
