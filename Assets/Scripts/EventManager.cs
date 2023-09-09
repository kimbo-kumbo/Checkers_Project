using UnityEngine;

public class EventManager : Сalculate
{
    public ChipComponent chip;   
    private Renderer render;    
    private Color colorBlack = Color.black;
    private Color colorBlue = Color.blue;   

    private void OnEnable()
    {
        chip = GetComponent<ChipComponent>();        
        render = GetComponent<Renderer>();
        chip.OnColorChange += ColorChipChange;              
        chip.OnColorReturn += ColorChipReturn;
        chip.OnСhoiceChip += ChoiseChip;
       
    }

    private void OnDisable()
    {
        chip.OnColorChange -= ColorChipChange;
        chip.OnColorReturn -= ColorChipReturn;
        chip.OnСhoiceChip -= ChoiseChip;   
    }    


    private void ColorChipChange(GameObject obj) //подсветка фишки
    {
        if(СhessBoard.motionWhite && (СhessBoard.whiteChipList.Contains(obj))) //подсветка для белых
            render.material.color = Color.green; 

        if(!СhessBoard.motionWhite && (СhessBoard.blackChipList.Contains(obj))) //подсветка для черных
            render.material.color = Color.yellow;
    }
    private void ColorChipReturn(GameObject obj) //сброс подсветки фишек
    {
        if (СhessBoard.whiteChipList.Contains(obj))
            render.material.color = new Color32(255, 214, 130, 255);
        if (СhessBoard.blackChipList.Contains(obj))
            render.material.color = new Color32(38, 23, 23, 255);
    }

    private void ChoiseChip(GameObject obj) //подсветка возможного хода белых или черных
    {  
        if (СhessBoard.motionWhite && (СhessBoard.whiteChipList.Contains(obj))) 
        {
            float distanceZ = 1; 
            float distanceX = 1;
            СhessBoard.currentSelectionChip = obj;
            Checkdistance(obj, distanceZ, distanceX);
        } 
        
        if (!СhessBoard.motionWhite && (СhessBoard.blackChipList.Contains(obj)))
        {
            float distanceZ = -1;
            float distanceX = 1;
            СhessBoard.currentSelectionChip = obj;
            Checkdistance(obj, distanceZ, distanceX);            
        }       
    }

    private void Checkdistance(GameObject obj, float distanceZ, float distanceX) //расчёт подсветки возможного хода
    {       
        foreach (GameObject field in СhessBoard.gameFieldList) //подсвечиваем игровую ячейку под выбранной фишкой
        {
            if (ParitetXZ(field , obj))
                PaintCell(field, colorBlue);       
            else
                PaintCell(field, colorBlack);                             
        }

        foreach (GameObject field in СhessBoard.gameFieldList)
        {            
            if(DifferenceZ(field, obj, distanceZ) && DifferenceX(field, obj, distanceX)) //подсвечиваем правую диагональ по ходу движения
            {
                if (NoBodyStay(field))
                    PaintCell(field, colorBlue);                              

                if ((CheckBlackChipStay(field) && СhessBoard.motionWhite) || (CheckWhiteChipStay(field) && !СhessBoard.motionWhite))
                {
                    foreach (GameObject field2 in СhessBoard.gameFieldList)
                    {
                        if (DifferenceZ(field2, obj, distanceZ * 2) && DifferenceX(field2, obj, distanceX * 2) && NoBodyStay(field2))
                            PaintCell(field2, colorBlue);                        
                    }
                }             

            }
            if (DifferenceZ(field, obj, distanceZ) && DifferenceX(field, obj, distanceX*-1)) //подсвечиваем левую диагональ по ходу движения
            {
                if (NoBodyStay(field))
                    PaintCell(field, colorBlue);                             

                if ((CheckBlackChipStay(field) && СhessBoard.motionWhite) || (CheckWhiteChipStay(field) && !СhessBoard.motionWhite))
                {
                    foreach (GameObject field2 in СhessBoard.gameFieldList)
                    {
                        if (DifferenceZ(field2, obj, distanceZ * 2) && DifferenceX(field2, obj, distanceX *-2) && NoBodyStay(field2))
                            PaintCell(field2, colorBlue);                      
                    }
                }
            }
        }
    }     
}