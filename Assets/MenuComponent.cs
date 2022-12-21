using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MenuComponent : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    Button Playbutton;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    
    }

  public void OnPlayButton()
    {
        SceneManager.LoadScene("scene", LoadSceneMode.Single);


    }
}
