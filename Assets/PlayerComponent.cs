using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;

[GenerateAuthoringComponent]
public struct PlayerComponent : IComponentData
{ 
 


   public float posX;
   public float posY;
   public Translation trans;

   
}
