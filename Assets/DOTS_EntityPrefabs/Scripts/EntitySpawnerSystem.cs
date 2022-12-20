using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public partial class EntitySpawnerSystem : SystemBase {

    private float spawnTimer;
    private Random random;
    private float3[] spawnPoints = new float3[4]
    {
        math.float3(10,0,0),
        math.float3(-10,0,0),
        math.float3(0,10,0),
        math.float3(0,-10,0)

    };
    protected override void OnCreate() {
        random = new Random(56);
    }

    protected override void OnUpdate() {
        spawnTimer -= Time.DeltaTime;

        if (spawnTimer <= 0f) {
            spawnTimer = .5f;
            // Spawn!
            PrefabEntityComponent prefabEntityComponent = GetSingleton<PrefabEntityComponent>();

            Entity spawnedEntity = EntityManager.Instantiate(prefabEntityComponent.prefabEntity);

            EntityManager.SetComponentData(spawnedEntity,
                new Translation { Value =spawnPoints[random.NextInt(0,4)]
             }
            );
            EntityManager.AddComponentData(spawnedEntity, new EnemyComponent());
            EntityManager.SetName(spawnedEntity, "Enemy");
        }
    }

}
