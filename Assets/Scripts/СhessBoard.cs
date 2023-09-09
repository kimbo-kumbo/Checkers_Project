using System.Collections.Generic;
using UnityEngine;

public class СhessBoard : Сalculate
{
    public GameObject[] fieldPrefab; //префаб клетки и фишек   
    public Vector3 stopPoint; //стартовая точка генерации игрового поля
    public Vector3 directionGeneration; // направление генерации игрового поля
    public static bool colorWhite; //булевая переменная смены цвета фишек    
    public static List<GameObject> whiteChipList = new List<GameObject>(); //коллекция белых фишек
    public static List<GameObject> blackChipList = new List<GameObject>(); //коллекция черных фишек
    public static List<GameObject> gameFieldList = new List<GameObject>(); //коллекция игровых полей   
    public static Vector3 targetPosition; //координаты выбранной клетки для движения
    public static GameObject currentSelectionChip; //фишка выбранная на данный момент
    public static bool motionWhite = true;
    public static bool isRotate = false;    
    public Transform spheraCamera;


    void Start()
    {               
        stopPoint = new Vector3(-0.5f, 0, 0.5f);
        directionGeneration = Vector3.right;
        colorWhite = true;

        for (int i = 0; i < 8; i++) //генерируем игровое поле и раставляем фишки на стартовые позиции
        {
            for (int j = 0; j < 8; j++)
            {
                GameObject pref = Instantiate(fieldPrefab[0]);
                pref.transform.position = stopPoint + directionGeneration;
                stopPoint = pref.transform.position;
                if (((j % 2 == 0) && colorWhite) || (!(j % 2 == 0) && !colorWhite))
                    pref.gameObject.GetComponent<Renderer>().material.color = Color.white; //окрашиваем ячейку в белый
                if (((j % 2 == 0) && !colorWhite) || (!(j % 2 == 0) && colorWhite))
                {
                    pref.gameObject.GetComponent<Renderer>().material.color = Color.black; //окрашиваем ячейку в черный
                    gameFieldList.Add(pref);//добавляем черную ячейку в коллекцию игровых ячеек
                    if (i > 4)
                    {
                        GameObject chipprefabBlack = Instantiate(fieldPrefab[1]);
                        chipprefabBlack.transform.position = pref.transform.position + new Vector3(0f, 0.2f, 0f);                       
                        blackChipList.Add(chipprefabBlack); //добавляем фишку в коллекцию черных                       
                    }
                    if (i < 3)
                    {
                        GameObject chipprefabWhite = Instantiate(fieldPrefab[2]);
                        chipprefabWhite.transform.position = pref.transform.position + new Vector3(0f, 0.2f, 0f);                                                   
                        whiteChipList.Add(chipprefabWhite); //добавляем фишку в коллекцию белых                       
                    }                    
                }
            }
            stopPoint += new Vector3(-8f, 0, 1f); //задаём новые координаты для стартовой точки генерации
            colorWhite = !colorWhite; //инверсируем переменную смены цвета
        }

        StatusCell(); // обновляем статус игровых ячеек
    }


    private void Update()
    {
        if (!isRotate) //поворачиваем обзор при смене хода
        {
            spheraCamera.transform.rotation = Quaternion.Lerp(spheraCamera.transform.rotation, Quaternion.Euler(0, 0, 0), 5*Time.deltaTime);
        }
        if (isRotate)
        {
            spheraCamera.transform.rotation = Quaternion.Lerp(spheraCamera.transform.rotation, Quaternion.Euler(0, 180, 0), 5* Time.deltaTime);            
        }       
    }
}