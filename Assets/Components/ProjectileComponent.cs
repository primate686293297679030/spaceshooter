using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

[GenerateAuthoringComponent]
public struct ProjectileComponent : IComponentData
{
    public Vector3 position;
    public Vector3 velocity;
    public Entity entity;
}