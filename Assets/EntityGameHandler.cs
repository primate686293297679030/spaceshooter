using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using Unity.Burst;
using Unity.Jobs;
using Unity.Physics;

[GenerateAuthoringComponent]
public struct EntityGameHandler : IComponentData
{

    public Entity GameHandlerPrefab;

}

public class GameManager:MonoBehaviour
{
    EntityManager em = World.DefaultGameObjectInjectionWorld.EntityManager;
    Entity gameHandlerEntity;
    private void Start()
    {
      
     

    }
    private void Update()
    {



       

    }


}
