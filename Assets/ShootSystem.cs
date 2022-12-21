using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine.PlayerLoop;
using MeshCollider = UnityEngine.MeshCollider;

public partial class ShootSystem : SystemBase
{
    Entity spawnedEntity;
    List<Entity> EntitiesList=new List<Entity>();
    private Translation playerTranslation;
    private PlayerComponent playerComponent;
    private ShootingComponent shootingComponent;
    private Entity playerEntity;
    private bool shooting;
    private float spawnTimer;
    protected override void OnStartRunning()
    {
        Entities.WithoutBurst().ForEach((ref PlayerComponent player) =>
        {
            playerEntity=player.entityHandle;
            playerComponent = player;
       

        }).Run();

        Entity player;
        PlayerPrefabComponent playerprefab = GetSingleton<PlayerPrefabComponent>();
        player = playerprefab.PlayerPrefab;
    }
    
    protected override void OnUpdate()
    {
        ProjectilePrefab projectilePrefab = GetSingleton<ProjectilePrefab>();
        Entities.WithoutBurst().ForEach((ref PlayerComponent player, ref Translation trans) =>
      {
          playerComponent.position = trans.Value;
      


      }).Run();

 

     
        

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            shooting = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow)|| Input.GetKeyUp(KeyCode.RightArrow)|| Input.GetKeyUp(KeyCode.UpArrow)|| Input.GetKeyUp(KeyCode.DownArrow))
        {
            shooting = false;
        }
      

        if (shooting)
        {

            spawnTimer -= Time.DeltaTime;

            if (spawnTimer <= 0f)
            {


                spawnTimer = .2f;
                spawnedEntity = EntityManager.Instantiate(projectilePrefab.prefabEntity);
                EntityManager.AddComponentData(spawnedEntity, new ProjectileComponent()
                {
                    position = playerComponent.position,
                    velocity = new Vector3(Input.GetAxisRaw("Fire2"), Input.GetAxisRaw("Fire1"), 0),


                    damage = 10,
                    entity = spawnedEntity
                });
                EntityManager.AddComponentData(spawnedEntity, new Translation()
                {

                });

                EntityManager.SetName(spawnedEntity, "Projectile");
                EntitiesList.Add(spawnedEntity);

            }
        }


        Entities.WithoutBurst().ForEach((ref ProjectileComponent projectile,ref Translation translation) =>
        {
            projectile.position += projectile.velocity * Time.DeltaTime*3;
            translation.Value = projectile.position;
            EntityManager.SetComponentData(projectile.entity, projectile);
        }).Run();








    }
}
