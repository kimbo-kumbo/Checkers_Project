using System;
using UnityEngine;
using UnityEngine.EventSystems;


public class ChipComponent : MonoBehaviour , IPointerEnterHandler , IPointerExitHandler , IPointerClickHandler
{   
    public Action<GameObject> OnColorChange; //событие смены цвета
    public Action<GameObject> OnColorReturn; //событие возврата цвета
    public Action<GameObject> OnСhoiceChip; //событие выбора фишки 

    public void OnPointerEnter(PointerEventData eventData) //наведение мышки
    { 
      OnColorChange?.Invoke(gameObject);
    }    
    public void OnPointerExit(PointerEventData eventData) //отведение мышки
    {           
      OnColorReturn?.Invoke(gameObject);        
    }
    public void OnPointerClick(PointerEventData eventData) //клик мышкой
    {
      OnСhoiceChip?.Invoke(gameObject);        
    }
}