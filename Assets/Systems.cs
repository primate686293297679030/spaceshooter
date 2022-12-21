using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor.Build.Content;

public partial class MoveSystem : SystemBase
{
    public Entity playerEntity;
    public Entity GameHandlerEntity;
    protected override void OnStartRunning()
    {
        EntityManager em = World.DefaultGameObjectInjectionWorld.EntityManager;
         playerEntity =EntityManager.Instantiate( GetSingleton<PlayerPrefabComponent>().PlayerPrefab);
       

        GameHandlerEntity = EntityManager.Instantiate(GetSingleton<EntityGameHandler>().GameHandlerPrefab);
        em.AddComponentData(GameHandlerEntity, new LevelComponent
        {
            wave = 1,
            enemies=10,
        });

        em.AddComponentData(playerEntity, new PlayerComponent()
       {
          entityHandle = playerEntity,
       });
       em.AddComponentData(playerEntity, new ShootingComponent()
       {
           
       });
        em.SetComponentData(playerEntity, new Translation
        {
            Value = math.float3(0, 1, 0)
        });
        
        em.SetComponentData(playerEntity, new LocalToWorld 
        {
            Value=         math.float4x4(0,0,0,0)  
        }) ;
    }

    protected override void OnUpdate()
    {
        Entities.WithoutBurst().ForEach((ref Translation translation, ref PlayerComponent playerComponent) =>
        {
           
            translation.Value.x += Input.GetAxisRaw("Horizontal") * Time.DeltaTime*3;
            translation.Value.y += Input.GetAxisRaw("Vertical") * Time.DeltaTime*3;
            translation.Value.z = 0;
            playerComponent.position = translation.Value;

        }).Run();

        Entities.WithoutBurst().ForEach((PlayerComponent playerComponent, isDeadTag ded) =>
        {



        }).Run();

    }
}

public partial class WaveManager : SystemBase
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref LevelComponent lvlcomponent) =>
        {

        }).Run();
    }
}