using UnityEngine;

public class EventManager : �alculate
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
        chip.On�hoiceChip += ChoiseChip;
       
    }

    private void OnDisable()
    {
        chip.OnColorChange -= ColorChipChange;
        chip.OnColorReturn -= ColorChipReturn;
        chip.On�hoiceChip -= ChoiseChip;   
    }    


    private void ColorChipChange(GameObject obj) //��������� �����
    {
        if(�hessBoard.motionWhite && (�hessBoard.whiteChipList.Contains(obj))) //��������� ��� �����
            render.material.color = Color.green; 

        if(!�hessBoard.motionWhite && (�hessBoard.blackChipList.Contains(obj))) //��������� ��� ������
            render.material.color = Color.yellow;
    }
    private void ColorChipReturn(GameObject obj) //����� ��������� �����
    {
        if (�hessBoard.whiteChipList.Contains(obj))
            render.material.color = new Color32(255, 214, 130, 255);
        if (�hessBoard.blackChipList.Contains(obj))
            render.material.color = new Color32(38, 23, 23, 255);
    }

    private void ChoiseChip(GameObject obj) //��������� ���������� ���� ����� ��� ������
    {  
        if (�hessBoard.motionWhite && (�hessBoard.whiteChipList.Contains(obj))) 
        {
            float distanceZ = 1; 
            float distanceX = 1;
            �hessBoard.currentSelectionChip = obj;
            Checkdistance(obj, distanceZ, distanceX);
        } 
        
        if (!�hessBoard.motionWhite && (�hessBoard.blackChipList.Contains(obj)))
        {
            float distanceZ = -1;
            float distanceX = 1;
            �hessBoard.currentSelectionChip = obj;
            Checkdistance(obj, distanceZ, distanceX);            
        }       
    }

    private void Checkdistance(GameObject obj, float distanceZ, float distanceX) //������ ��������� ���������� ����
    {       
        foreach (GameObject field in �hessBoard.gameFieldList) //������������ ������� ������ ��� ��������� ������
        {
            if (ParitetXZ(field , obj))
                PaintCell(field, colorBlue);       
            else
                PaintCell(field, colorBlack);                             
        }

        foreach (GameObject field in �hessBoard.gameFieldList)
        {            
            if(DifferenceZ(field, obj, distanceZ) && DifferenceX(field, obj, distanceX)) //������������ ������ ��������� �� ���� ��������
            {
                if (NoBodyStay(field))
                    PaintCell(field, colorBlue);                              

                if ((CheckBlackChipStay(field) && �hessBoard.motionWhite) || (CheckWhiteChipStay(field) && !�hessBoard.motionWhite))
                {
                    foreach (GameObject field2 in �hessBoard.gameFieldList)
                    {
                        if (DifferenceZ(field2, obj, distanceZ * 2) && DifferenceX(field2, obj, distanceX * 2) && NoBodyStay(field2))
                            PaintCell(field2, colorBlue);                        
                    }
                }             

            }
            if (DifferenceZ(field, obj, distanceZ) && DifferenceX(field, obj, distanceX*-1)) //������������ ����� ��������� �� ���� ��������
            {
                if (NoBodyStay(field))
                    PaintCell(field, colorBlue);                             

                if ((CheckBlackChipStay(field) && �hessBoard.motionWhite) || (CheckWhiteChipStay(field) && !�hessBoard.motionWhite))
                {
                    foreach (GameObject field2 in �hessBoard.gameFieldList)
                    {
                        if (DifferenceZ(field2, obj, distanceZ * 2) && DifferenceX(field2, obj, distanceX *-2) && NoBodyStay(field2))
                            PaintCell(field2, colorBlue);                      
                    }
                }
            }
        }
    }     
}