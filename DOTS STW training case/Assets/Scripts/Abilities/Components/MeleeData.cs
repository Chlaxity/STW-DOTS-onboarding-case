using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct MeleeData : IComponentData
{
    public int damage;    
}
