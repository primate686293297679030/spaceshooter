using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[GenerateAuthoringComponent]
public struct PlayerPrefabComponent : IComponentData
{
    public Entity PlayerPrefab;
}
