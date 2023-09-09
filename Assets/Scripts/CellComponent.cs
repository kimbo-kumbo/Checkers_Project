using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class CellComponent : Сalculate, IPointerClickHandler
{   
    public bool checkWhiteChipStay = false; //проверяем стоит ли белая фишка на ячейке
    public bool checkBlackChipStay = false; //проверяем стоит ли черная фишка на ячейке
    public bool noBodyStay = true; //проверяем пустая ли ячейка
    public bool isMoving = false; // проверяем возможность движения
    private float speed = 2f; //скорость передвижения выбранной фишки
    public Action OnMoveTargetPosition; //событие выбора клетки для движения  


    private void Update()
    {
        if (isMoving)
        {
            СhessBoard.currentSelectionChip.transform.position = Vector3.MoveTowards(СhessBoard.currentSelectionChip.transform.position, СhessBoard.targetPosition, speed * Time.deltaTime); //передвигаем выбранную фишку
            if (СhessBoard.currentSelectionChip.transform.position.z == 7.5f && СhessBoard.motionWhite == true) Debug.Log("Победа белых"); //условие победы белых
            if (СhessBoard.currentSelectionChip.transform.position.z == 0.5f && СhessBoard.motionWhite == false) Debug.Log("Победа черных"); //условие победы черных
            if (СhessBoard.currentSelectionChip.transform.position == СhessBoard.targetPosition) //условие перехода хода к противнику
            {
                StatusCell();
                isMoving = false;                
                СhessBoard.motionWhite = !СhessBoard.motionWhite;
                СhessBoard.isRotate = !СhessBoard.isRotate;
            }            
        }
    }

    private void ChangeTargetPosition(GameObject a) //обновляем координаты позиции к которой движется выбранная фишка
    {
        СhessBoard.targetPosition = a.transform.position + new Vector3(0,0.2f,0); 
        isMoving = true;
    }

    public void OnPointerClick(PointerEventData eventData) //обработка события клика по подсвеченной игровой ячейке
    {
        Renderer _renderer = gameObject.GetComponent<Renderer>();
        if(СhessBoard.gameFieldList.Contains(gameObject) && _renderer.material.color == Color.blue)
        {
            ChangeTargetPosition(gameObject);
            OnMoveTargetPosition?.Invoke();
        }              
    }
}