using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine.PlayerLoop;
using MeshCollider = UnityEngine.MeshCollider;

public partial class ShootSystem : SystemBase
{
    Entity spawnedEntity;
   public List<Entity> EntitiesList=new List<Entity>();
 
    private PlayerComponent playerComponent;
  
    private Entity playerEntity;
    private bool shooting;
    private float spawnTimer;
    protected override void OnCreate()
    {
        Enabled = false;
    }
    protected override void OnStartRunning()
    {
      

      
    }
    
    protected override void OnUpdate()
    {
        ProjectilePrefab projectilePrefab = GetSingleton<ProjectilePrefab>();




        bool SpawnProjectile = EntityManager.World.GetExistingSystem<InputSystem>().shooting;
        if (SpawnProjectile)
        {

            spawnTimer -= Time.DeltaTime;
            
            if (spawnTimer <= 0f)
            {
              float3 playerTranslation=  EntityManager.GetComponentData<Translation>(World.GetExistingSystem<GameHandler>().playerEntity).Value;
               // GetComponentDataFromEntity<Translation>().TryGetComponent(World.GetExistingSystem<MoveSystem>().playerEntity, out Translation componentData);


                spawnTimer = .1f;
                spawnedEntity = EntityManager.Instantiate(projectilePrefab.prefabEntity);
                EntityManager.AddComponentData(spawnedEntity, new ProjectileComponent()
                {
                    position = playerTranslation,
                    velocity = new Vector3(Input.GetAxisRaw("Fire2"), Input.GetAxisRaw("Fire1"), 0),
                    
                    
                    damage = 10,
                    entity = spawnedEntity
                });
                EntityManager.AddComponentData(spawnedEntity, new Translation()
                {

                });

               
                EntitiesList.Add(spawnedEntity);

            }
        }
       
        
        Entities.WithoutBurst().ForEach((ref ProjectileComponent projectile,ref Translation translation) =>
        {

            projectile.position += Vector3.Normalize( projectile.velocity )* Time.DeltaTime*6;
            translation.Value = projectile.position;
            EntityManager.SetComponentData(projectile.entity, projectile);
            

        }).Run();

        if (HasComponent<isDeadTag>(World.GetExistingSystem<GameHandler>().playerEntity))
        {
          
            Enabled = false;

        }
    






    }
}
