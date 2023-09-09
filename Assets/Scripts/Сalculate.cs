using UnityEngine;

public class Сalculate : MonoBehaviour
{
    protected static bool ParitetXZ(GameObject a, GameObject b) //равенство по Z
    {
        bool paritetZ = (a.transform.position.z == b.transform.position.z);
        bool paritetX = (a.transform.position.x == b.transform.position.x);
        if (paritetZ && paritetX) return true;
        else return false;
    }

    protected static bool DifferenceX(GameObject a, GameObject b, float X) //разница по X
    {
        bool result = (a.transform.position.x - b.transform.position.x == X);
        return result;
    }
    protected static bool DifferenceZ(GameObject a, GameObject b, float Z) //разница по Z
    {
        bool result = (a.transform.position.z - b.transform.position.z == Z);
        return result;
    }

    protected static bool NoBodyStay(GameObject a) //проверяем пустая ли ячейка
    {
        bool result = a.GetComponent<CellComponent>().noBodyStay;
        return result;
    }

    protected static bool CheckWhiteChipStay(GameObject a) //проверяем стоит ли белая фишка
    {
        bool result = a.GetComponent<CellComponent>().checkWhiteChipStay;
        return result;
    }
    protected static bool CheckBlackChipStay(GameObject a) //проверяем стоит ли черная фишка
    {
        bool result = a.GetComponent<CellComponent>().checkBlackChipStay;
        return result;
    }

    protected static void PaintCell(GameObject a, Color color) //раскраска клетки
    {
        a.GetComponent<Renderer>().material.color = color;
    }

    protected void StatusCell() //проходимся по списку всех игровых ячеек и обновляем их статус
    {

        foreach (GameObject field in СhessBoard.gameFieldList) 
        {
            CellComponent _field = field.GetComponent<CellComponent>();
            Renderer _renderer = field.GetComponent<Renderer>();
            _field.checkWhiteChipStay = false;
            _field.checkBlackChipStay = false;
            _field.noBodyStay = true;
            _renderer.material.color = Color.black;

            foreach (GameObject chip in СhessBoard.whiteChipList)
            {
                if (ParitetXZ(field, chip))
                {
                    _field.noBodyStay = false;
                    _field.checkWhiteChipStay = true;
                }
            }

            foreach (GameObject chip in СhessBoard.blackChipList)
            {
                if (ParitetXZ(field, chip))
                {
                    _field.noBodyStay = false;
                    _field.checkBlackChipStay = true;
                }
            }
        }
    }
}
