using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

public class ShootSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        ProjectilePrefab projectilePrefab = GetSingleton<ProjectilePrefab>();


        Entity spawnedEntity = EntityManager.Instantiate(projectilePrefab.prefabEntity);



        EntityManager.SetComponentData(spawnedEntity,
            new Translation
            {
                //mouseposition
                Value = new float3(1, 1, 0)
            }
        );

    }
}
