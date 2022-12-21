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
       
    }
    protected override void OnStartRunning()
    {
        playerEntity = World.GetExistingSystem<MoveSystem>().playerEntity;
        gameHandlerEntity= World.GetExistingSystem<MoveSystem>().GameHandlerEntity;
        lvl=EntityManager.GetComponentData<LevelComponent>(gameHandlerEntity);

    }

    protected override void OnUpdate()
    {
        //ref LocalToWorld ltw2
        //float3(ltw.Value.c0.x, ltw.Value.c0.y, ltw.Value.c0.z);

    


        float3 dest = EntityManager.GetComponentData<Translation>(playerEntity).Value;
        float dT = Time.DeltaTime;

    

        Entities.WithStructuralChanges().ForEach((Entity e , ref EnemyComponent ec, ref isDeadTag tag) =>
        {
          EntityManager.DestroyEntity(e);
            lvl.enemies -= 1;
            Debug.Log(lvl.enemies);

        }).Run();
        
        Entities.ForEach((ref Translation translation,ref EnemyComponent enemyComponent) =>
        {

           // posAVector3 = translation.Value;
           float3 origin = translation.Value;
           // float3 dot = math.dot(posA, posB);
           //float3 cross= math.cross(posA, posB);
            //float3 dist = math.distance(posA, posB); 
    
            // Vector3.RotateTowards(posAVector3, posBvVector3, dT * velocity, 0.0f);
            //  Debug.DrawLine(new Vector3(wtl.Value.c0.x, wtl.Value.c0.y, wtl.Value.c0.z),new Vector3(playerPos.x,playerPos.y,playerPos.z));
            //math.rotate(enemyQuat, math.normalize(playerPos))
            translation.Value +=math.normalize(dest- origin) * dT;

           // Debug.Log(origin + " Origin");
            //Debug.Log(dest + " Destination");

            //  posA = new float3(ltw2.Value.c0.x, ltw2.Value.c0.y, ltw2.Value.c0.z);

        }).Schedule();

       
    }
}
