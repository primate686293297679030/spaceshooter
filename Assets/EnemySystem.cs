using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial class EnemySystem : SystemBase
{

    private Vector3 posAVector3;
    private Vector3 posBvVector3;
    private float dT;
    private int velocity;
    private float3 playerPos;
    private Entity playerEntity;
        private Entity gameHandlerEntity;
    LevelComponent lvl;
    protected override void OnCreate()
    {
        Enabled = false;
    }
    protected override void OnStartRunning()
    {
       playerEntity = World.GetExistingSystem<GameHandler>().playerEntity;
       //gameHandlerEntity= World.GetExistingSystem<GameHandler>().GameHandlerEntity;
       //lvl=EntityManager.GetComponentData<LevelComponent>(gameHandlerEntity);

    }

    protected override void OnUpdate()
    {
        float3 dest = EntityManager.GetComponentData<Translation>(playerEntity).Value;
        float dT = Time.DeltaTime;

    
        //Destroys enemies that has collided with projectiles if they have the isDeadTag
        Entities.WithStructuralChanges().ForEach((Entity e , ref EnemyComponent ec, ref isDeadTag tag) =>
        {
          EntityManager.DestroyEntity(e);
            lvl.enemies -= 1;
           

        }).Run();

        // Moves Enemies Towards Player
        Entities.ForEach((ref Translation translation,ref EnemyComponent enemyComponent) =>
        {
           float3 origin = translation.Value;
            translation.Value +=math.normalize(dest- origin) * dT;

        }).Schedule();

  
        if (HasComponent<isDeadTag>(World.GetExistingSystem<GameHandler>().playerEntity))
        {
        
            Enabled = false;

        }
   
      
    }
}
