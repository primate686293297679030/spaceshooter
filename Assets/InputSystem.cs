using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;



public partial class InputSystem : SystemBase
{
    public bool shooting;
    Vector3 previous;
    Vector3 current;
    bool[] inputs;
    Vector4 _inputs;
    protected override void OnCreate()
    {
        Enabled = false;
    }
    protected override void OnUpdate()
    {
        Entities.WithoutBurst().ForEach((ref Translation translation, ref PlayerComponent playerComponent) =>
        {
           
            translation.Value.x += Input.GetAxisRaw("Horizontal") * Time.DeltaTime*3;
            translation.Value.y += Input.GetAxisRaw("Vertical") * Time.DeltaTime*3;
            translation.Value.z = 0;
            playerComponent.position = translation.Value;

        }).Run();


        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                shooting = false;
                _inputs.    
            }
            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                
                shooting = false;
            }
            if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                shooting = false;
            }
            if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                shooting = false;
            }

        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                shooting = true;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                shooting = true;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                shooting = true;
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                shooting = true;
            }
        }

     

      

        if (HasComponent<isDeadTag>(World.GetExistingSystem<GameHandler>().playerEntity))
        {
         
            Enabled = false;
          
        }
        
        




    }
}

public partial class WaveManager : SystemBase
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref LevelComponent lvlcomponent) =>
        {

        }).Run();
    }
}