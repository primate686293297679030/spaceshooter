using System.Collections;
using System.Collections.Generic;

using Unity.Burst;
using Unity.Collections;
using UnityEngine;
using Unity.Entities;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Jobs;
using Unity.Mathematics;
using Debug = UnityEngine.Debug;

public partial class CollisionTriggers : SystemBase
{
    private StepPhysicsWorld stepPhysicsWorld;
    private EntityCommandBufferSystem commandBufferSystem;

    private EntityManager em;
   


  

    protected override void OnCreate()
    {

     
       stepPhysicsWorld= World.GetOrCreateSystem< StepPhysicsWorld>();
       commandBufferSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
    }



    protected override void OnUpdate()
    {
        var job = new DestroyOnTriggerSystemJob
        {
            allProjectiles = GetComponentDataFromEntity<ProjectileComponent>(true),
            allPlayers = GetComponentDataFromEntity<PlayerComponent>(true),
            allEnemies = GetComponentDataFromEntity<EnemyComponent>(true),
            entityCommandBuffer = commandBufferSystem.CreateCommandBuffer()
        };
        Dependency = job.Schedule(stepPhysicsWorld.Simulation, Dependency);
        commandBufferSystem.AddJobHandleForProducer(Dependency);

        

    }

    [BurstCompile]
    struct DestroyOnTriggerSystemJob : ITriggerEventsJob
    {
        [ReadOnly] public ComponentDataFromEntity<ProjectileComponent> allProjectiles;
         [ReadOnly] public ComponentDataFromEntity<PlayerComponent> allPlayers;
        [ReadOnly] public ComponentDataFromEntity<EnemyComponent> allEnemies;

         public ComponentDataFromEntity<ProjectileComponent> allProjectiles2;
         public ComponentDataFromEntity<PlayerComponent> allPlayers2;
       public ComponentDataFromEntity<EnemyComponent> allEnemies2;
        public EntityCommandBuffer entityCommandBuffer;

        public void Execute(TriggerEvent triggerEvent)
        {
            Debug.Log("Collision triggered");


            Entity entityA = triggerEvent.EntityA;
            Entity entityB = triggerEvent.EntityB;
            if (allProjectiles.HasComponent(entityA) && allProjectiles.HasComponent(entityB))
                return;
            if (allProjectiles.HasComponent(entityA) && allPlayers.HasComponent(entityB))
                return;
            if (allPlayers.HasComponent(entityA) && allProjectiles.HasComponent(entityB))
                return;

            if (allProjectiles.HasComponent(entityA) && allEnemies.HasComponent(entityB))
            {
                Debug.Log("projectile destroyed");
                entityCommandBuffer.DestroyEntity(entityA);
                entityCommandBuffer.DestroyEntity(entityB);
                
        }


        else if (allEnemies.HasComponent(entityA) && allProjectiles.HasComponent(entityB))
            {
                Debug.Log("projectile destroyed2");
                entityCommandBuffer.DestroyEntity(entityB);
                entityCommandBuffer.DestroyEntity(entityA);

            }


        }
    }

  






}
