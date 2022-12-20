using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

[GenerateAuthoringComponent]
public struct PlayerComponent : IComponentData
{
    public Entity entityHandle;
    public float3 position;

   
  

}

[GenerateAuthoringComponent]
public struct ProjectileComponent : IComponentData
{

    public Vector3 position;
    public Vector3 velocity;
    public int damage;
    public Entity entity;



}

[GenerateAuthoringComponent]
public struct EnemyComponent : IComponentData
{
    public float health;
    public float xPos;
    public float yPos;


    public Entity prefab;



}

[GenerateAuthoringComponent]
public struct ShootingComponent : IComponentData
{
    public Vector3 dir;
    
  


}
[GenerateAuthoringComponent]
public struct LevelComponent : IComponentData
{
    public int wave;
    public int enemies; 




}




