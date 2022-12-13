using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.VisualScripting;
using UnityEngine;
[GenerateAuthoringComponent]
public struct EnemyComponent : IComponentData
{
    public float health;
    public float xPos;
    public float yPos;

    
    public Entity prefab;



}
