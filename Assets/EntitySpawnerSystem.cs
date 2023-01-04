using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;
using Random = Unity.Mathematics.Random;
using System.Collections.Generic;
using System.Linq;

public partial class EntitySpawnerSystem : SystemBase {

   

    private float spawnTimer;
    private Random random;
   public List<Entity> entities;
    private Entity gameHandlerEntity;

    LevelComponent lvl;
    private float3[] spawnPoints = new float3[4]
    {
        math.float3(10,0,0),
        math.float3(-10,0,0),
        math.float3(0,7,0),
        math.float3(0,-7,0)

    };

    protected override void OnCreate()
    {
        Enabled = false;
    }
    protected override void OnStartRunning() {
      
        random = new Random(56);
        entities = new List<Entity>();
    }

 
    protected override void OnUpdate() {
        spawnTimer -= Time.DeltaTime;

        if (spawnTimer <= 0f) {

          
            spawnTimer = .5f;
            // Spawn!
            PrefabEntityComponent prefabEntityComponent = GetSingleton<PrefabEntityComponent>();

            Entity spawnedEntity = EntityManager.Instantiate(prefabEntityComponent.prefabEntity);
            entities.Add(spawnedEntity);
            EntityManager.SetComponentData(spawnedEntity,
                new Translation { Value =spawnPoints[random.NextInt(0,4)]
             }
            );
            EntityManager.AddComponentData(spawnedEntity, new EnemyComponent());
          
         

           
        }

        
        if (HasComponent<isDeadTag>(World.GetExistingSystem<GameHandler>().playerEntity))
        {
          
            Enabled = false;

        }


    }

}
