using UnityEngine;

public class DestructionChip : MonoBehaviour
{

    private void OnTriggerEnter(Collider other) //обрабатываем столкновение фишек
    {
        if(СhessBoard.motionWhite)
        {            
            СhessBoard.blackChipList.Remove(other.gameObject);
            WinConditions();
            Destroy(other.gameObject);
        }           
        else
        {           
            СhessBoard.whiteChipList.Remove(this.gameObject);
            WinConditions();
            Destroy(this.gameObject);
        }                  
    }


    private void WinConditions()
    {
        if (СhessBoard.whiteChipList.Count == 0) Debug.Log("Победа черных"); //условие победы черных
        if (СhessBoard.blackChipList.Count == 0) Debug.Log("Победа белых"); //условие победы белых
    }
}