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
    List<Entity> entities;
    private Entity gameHandlerEntity;
    LevelComponent lvl;
    private float3[] spawnPoints = new float3[4]
    {
        math.float3(10,0,0),
        math.float3(-10,0,0),
        math.float3(0,10,0),
        math.float3(0,-10,0)

    };
    protected override void OnStartRunning() {

        EntityManager em = World.DefaultGameObjectInjectionWorld.EntityManager;
      
        random = new Random(56);
        entities = new List<Entity>();


        gameHandlerEntity = World.GetExistingSystem<MoveSystem>().GameHandlerEntity;
        lvl = EntityManager.GetComponentData<LevelComponent>(gameHandlerEntity);

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
          
            EntityManager.SetName(spawnedEntity, "Enemy");

           
        }
    }

}
