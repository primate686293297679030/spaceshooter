using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

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
        }) ;

        em.AddComponentData(playerEntity, new PlayerComponent()
       {
          entityHandle = playerEntity,
       });
       em.AddComponentData(playerEntity, new ShootingComponent()
       {
           
       });
    }

    protected override void OnUpdate()
    {
        Entities.WithoutBurst().ForEach((ref Translation translation, ref PlayerComponent playerComponent) =>
        {
           
            translation.Value.x += Input.GetAxisRaw("Horizontal") * Time.DeltaTime*3;
            translation.Value.y += Input.GetAxisRaw("Vertical") * Time.DeltaTime*3;
            playerComponent.position = translation.Value;

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