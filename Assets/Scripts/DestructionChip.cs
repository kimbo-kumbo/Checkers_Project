using UnityEngine;

public class DestructionChip : MonoBehaviour
{

    private void OnTriggerEnter(Collider other) //������������ ������������ �����
    {
        if(�hessBoard.motionWhite)
        {            
            �hessBoard.blackChipList.Remove(other.gameObject);
            WinConditions();
            Destroy(other.gameObject);
        }           
        else
        {           
            �hessBoard.whiteChipList.Remove(this.gameObject);
            WinConditions();
            Destroy(this.gameObject);
        }                  
    }


    private void WinConditions()
    {
        if (�hessBoard.whiteChipList.Count == 0) Debug.Log("������ ������"); //������� ������ ������
        if (�hessBoard.blackChipList.Count == 0) Debug.Log("������ �����"); //������� ������ �����
    }
}