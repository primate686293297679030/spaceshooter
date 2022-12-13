using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;



public class MoveSystem : ComponentSystem
{
  
    protected override void OnUpdate()
    {

        Entities.ForEach((ref Translation translation,ref PlayerComponent playerComponent) =>
        {
            translation.Value.x += Input.GetAxisRaw("Horizontal")*Time.DeltaTime;
            translation.Value.y += Input.GetAxisRaw("Vertical")*Time.DeltaTime;

        });
        
            }
}
