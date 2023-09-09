using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class CellComponent : �alculate, IPointerClickHandler
{   
    public bool checkWhiteChipStay = false; //��������� ����� �� ����� ����� �� ������
    public bool checkBlackChipStay = false; //��������� ����� �� ������ ����� �� ������
    public bool noBodyStay = true; //��������� ������ �� ������
    public bool isMoving = false; // ��������� ����������� ��������
    private float speed = 2f; //�������� ������������ ��������� �����
    public Action OnMoveTargetPosition; //������� ������ ������ ��� ��������  


    private void Update()
    {
        if (isMoving)
        {
            �hessBoard.currentSelectionChip.transform.position = Vector3.MoveTowards(�hessBoard.currentSelectionChip.transform.position, �hessBoard.targetPosition, speed * Time.deltaTime); //����������� ��������� �����
            if (�hessBoard.currentSelectionChip.transform.position.z == 7.5f && �hessBoard.motionWhite == true) Debug.Log("������ �����"); //������� ������ �����
            if (�hessBoard.currentSelectionChip.transform.position.z == 0.5f && �hessBoard.motionWhite == false) Debug.Log("������ ������"); //������� ������ ������
            if (�hessBoard.currentSelectionChip.transform.position == �hessBoard.targetPosition) //������� �������� ���� � ����������
            {
                StatusCell();
                isMoving = false;                
                �hessBoard.motionWhite = !�hessBoard.motionWhite;
                �hessBoard.isRotate = !�hessBoard.isRotate;
            }            
        }
    }

    private void ChangeTargetPosition(GameObject a) //��������� ���������� ������� � ������� �������� ��������� �����
    {
        �hessBoard.targetPosition = a.transform.position + new Vector3(0,0.2f,0); 
        isMoving = true;
    }

    public void OnPointerClick(PointerEventData eventData) //��������� ������� ����� �� ������������ ������� ������
    {
        Renderer _renderer = gameObject.GetComponent<Renderer>();
        if(�hessBoard.gameFieldList.Contains(gameObject) && _renderer.material.color == Color.blue)
        {
            ChangeTargetPosition(gameObject);
            OnMoveTargetPosition?.Invoke();
        }              
    }
}