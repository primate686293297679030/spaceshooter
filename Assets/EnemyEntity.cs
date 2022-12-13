using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Collections;
using Unity.Rendering;
using Unity.Mathematics;

public class EnemyEntity : MonoBehaviour
{
    [SerializeField]
    Material material;
    public float width = 1;
    public float height = 1;
    private void Start()
    {
 
        Mesh mesh = SquareMesh();

        EntityManager eM = World.DefaultGameObjectInjectionWorld.EntityManager;
        EntityArchetype playerArchetype = eM.CreateArchetype(typeof(PlayerComponent), typeof(Translation),typeof(RenderMesh), typeof(LocalToWorld));
        
        
        
        
        
        EntityArchetype enemyArchetype = eM.CreateArchetype
        (typeof(EnemyComponent), typeof(Translation), typeof(RenderMesh), typeof(LocalToWorld));
        Entity PlayerEntity=eM.CreateEntity(playerArchetype);
        Entity Enemyentity = eM.CreateEntity(enemyArchetype);
        NativeArray<Entity> ArrEntities = new NativeArray<Entity>(1, Allocator.Temp);

        eM.CreateEntity(enemyArchetype, ArrEntities);

        for (int i = 0; i < ArrEntities.Length; i++)
        {
            Entity ent = ArrEntities[i];
            eM.SetComponentData(ent, new EnemyComponent { health = 1 });

         

        }
        eM.SetSharedComponentData(PlayerEntity, new RenderMesh
        {
            mesh = mesh,
            material = material,
        });


        
        


        ArrEntities.Dispose();


        
    }





    private Mesh SquareMesh()
    {





        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();

        meshRenderer.sharedMaterial = new Material(Shader.Find("Standard"));

        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();

        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[4]
        {
            new Vector3(0, 0, 0),
            new Vector3(width, 0, 0),
            new Vector3(0, height, 0),
            new Vector3(width, height, 0)
        };
        mesh.vertices = vertices;

        int[] tris = new int[6]
        {
            // lower left triangle
            0, 2, 1,
            // upper right triangle
            2, 3, 1
        };
        mesh.triangles = tris;

        Vector3[] normals = new Vector3[4]
        {
            -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward
        };
        mesh.normals = normals;

        Vector2[] uv = new Vector2[4]
        {
            new Vector2(0, 0),
            new Vector2(1, 0),
            new Vector2(0, 1),
            new Vector2(1, 1)
        };
        mesh.uv = uv;

        meshFilter.mesh = mesh;

        return mesh;

    }
}
