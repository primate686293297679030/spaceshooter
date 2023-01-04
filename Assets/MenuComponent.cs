using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MenuComponent : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    Button playAgainButton;

    [SerializeField]
    Button exitButton;

    [SerializeField]
    private GameObject DefeatHandle;

    [SerializeField]
    private GameObject pauseHandle;

    [SerializeField]
    private GameObject MenuHandle;

    private Entity player;
    private Entity gameManager;

    private List<World> worlds= new List<World>();
    private EntityManager _entityManager;
    private List<Entity> players;
    bool pause=true;
    void Start()
    {
        MenuHandle.SetActive(true);
        _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
    }

    // Update is called once per frame
    void Update()
    {

        gameManager = _entityManager.World.GetExistingSystem<GameHandler>().GameHandlerEntity;
        player = _entityManager.World.GetExistingSystem<GameHandler>().playerEntity;
        if (_entityManager.HasComponent<isDeadTag>(player))
        {
           
            DefeatHandle.SetActive(true);
            Debug.Log("Interaction established");

        }

        if (Input.GetKeyDown(KeyCode.Escape)){

            OnPause();

        }
    }
    public void OnPause()
    {
        pauseHandle.SetActive(!pauseHandle.activeSelf);
        pause = !pause;
        _entityManager.World.GetExistingSystem<InputSystem>().Enabled = pause;
        _entityManager.World.GetExistingSystem<EnemySystem>().Enabled = pause;
        _entityManager.World.GetExistingSystem<EntitySpawnerSystem>().Enabled = pause;
        _entityManager.World.GetExistingSystem<ShootSystem>().Enabled = pause;
    }
    public void OnPlayAgainButton()
    {
        DefeatHandle.SetActive(false);
        _entityManager.DestroyEntity(player);
        List<Entity> enemies = _entityManager.World.GetExistingSystem<EntitySpawnerSystem>().entities;
        List<Entity> ent = _entityManager.World.GetExistingSystem<ShootSystem>().EntitiesList;
        for (int i = enemies.Count - 1; i > 0; i--)
        {
            _entityManager.DestroyEntity(enemies[i]);
        }
        for (int i = ent.Count-1; i > 0; i-- )
        {
            _entityManager.DestroyEntity(ent[i]);
        }
       // _entityManager.DestroyEntity(_entityManager.World.GetExistingSystem<GameHandler>().GameHandlerEntity);
       
        _entityManager.World.GetExistingSystem<GameHandler>().Enabled = true;
        _entityManager.World.GetExistingSystem<InputSystem>().Enabled = true;
     _entityManager.World.GetExistingSystem<EnemySystem>().Enabled = true;
     _entityManager.World.GetExistingSystem<EntitySpawnerSystem>().Enabled = true;
     _entityManager.World.GetExistingSystem<ShootSystem>().Enabled = true;

        //  _entityManager.RemoveComponent<isDeadTag>(player);
        //_entityManager.SetEnabled();

        //SceneManager.LoadScene("scene", LoadSceneMode.Single);

    }




    public void OnQuitButton()
    {
        DefeatHandle.SetActive(false);
        MenuHandle.SetActive(true);
        List<Entity> enemies = _entityManager.World.GetExistingSystem<EntitySpawnerSystem>().entities;
        List<Entity> ent = _entityManager.World.GetExistingSystem<ShootSystem>().EntitiesList;
        _entityManager.DestroyEntity(_entityManager.World.GetExistingSystem<GameHandler>().GameHandlerEntity);

        _entityManager.DestroyEntity(player);
        for (int i = enemies.Count - 1; i > 0; i--)
        {
            _entityManager.DestroyEntity(enemies[i]);
        }
        for (int i = ent.Count - 1; i > 0; i--)
        {
            _entityManager.DestroyEntity(ent[i]);
        }

        pauseHandle.SetActive(false);
        DefeatHandle.SetActive(false);
    }

    public void OnPlayButton()
    {
        MenuHandle.SetActive(false);
        _entityManager.World.GetExistingSystem<InputSystem>().Enabled = true;
        _entityManager.World.GetExistingSystem<EnemySystem>().Enabled = true;
        _entityManager.World.GetExistingSystem<EntitySpawnerSystem>().Enabled = true;
        _entityManager.World.GetExistingSystem<ShootSystem>().Enabled = true;
        _entityManager.World.GetExistingSystem<GameHandler>().Enabled = true;
    }
    public void OnExitButton()
    {
        Application.Quit();
        
    }



    
}
