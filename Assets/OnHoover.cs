using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class OnHoover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

  public Color origin = new Color(0.306f, 0.153f, 0.490f, 1.000f);
    Color rgbTextColor;
  public TextMeshProUGUI tmPro;
    float h= 267, s= 69, v= 49;


    private void Awake()
    {
        
    }

    private void Start()
    {
        tmPro = GetComponentInChildren<TextMeshProUGUI>();
    }
    
    //Detect if the Cursor starts to pass over the GameObject
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        
    
       // Color.RGBToHSV(textColor, out h, out s,out v);
      // rgbTextColor= Color.HSVToRGB(h, s, v);
       
      
       // origin = tmPro.color;
        tmPro.color =Color.green;

        //Output to console the GameObject's name and the following message
        Debug.Log("Cursor Entering " + name + " GameObject");
    }
    private void Update()
    {
       
    }


    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {

        tmPro.color = origin;
      
        //Output the following message with the GameObject's name
        Debug.Log("Cursor Exiting " + name + " GameObject");



    }
}
