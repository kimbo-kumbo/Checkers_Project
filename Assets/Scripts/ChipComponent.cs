using System;
using UnityEngine;
using UnityEngine.EventSystems;


public class ChipComponent : MonoBehaviour , IPointerEnterHandler , IPointerExitHandler , IPointerClickHandler
{   
    public Action<GameObject> OnColorChange; //������� ����� �����
    public Action<GameObject> OnColorReturn; //������� �������� �����
    public Action<GameObject> On�hoiceChip; //������� ������ ����� 

    public void OnPointerEnter(PointerEventData eventData) //��������� �����
    { 
      OnColorChange?.Invoke(gameObject);
    }    
    public void OnPointerExit(PointerEventData eventData) //��������� �����
    {           
      OnColorReturn?.Invoke(gameObject);        
    }
    public void OnPointerClick(PointerEventData eventData) //���� ������
    {
      On�hoiceChip?.Invoke(gameObject);        
    }
}