using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QadManager : MonoBehaviour
{

    public CameraControler mainCam;

    public PlayerControler Player;
    Vector3 PlayerInitPos;

    public GameObject Qad;
    public int x;
    public int y;

    public GameObject[] QadList;
    float halfHeight = 0;

    public Transform QadSpawnPos;

    public LayerMask layerQad;

    [HideInInspector]
    public Vector3 mousePosition;

    // Start is called before the first frame update
    void Start()
    {
        CreateMap();
        PlayerInitPos = new Vector3(gameObject.transform.position.x
            , gameObject.transform.position.y + QadList[0].GetComponent<Collider>().bounds.extents.y + 0.05f
            , gameObject.transform.position.z);
        
        Player.GetComponent<PlayerControler>().StartPlayer(PlayerInitPos);
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControler>();

        mainCam = Instantiate(mainCam);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //CREAR MAPA
    void CreateMap()
    {
        for (int i = 0; i < x; i++)
        {
            for (int b = 0; b < y; b++)
            {
                Instantiate(Qad, transform, true);
                MoveQadPos('y');
            }
            MoveQadPos('x');
        }

        QadList = GameObject.FindGameObjectsWithTag("Qad");

        for (int i = 0; i < QadList.Length; i++)
        {
            halfHeight = QadList[i].GetComponent<Collider>().bounds.extents.y;
        }
    }
    void MoveQadPos(char direction)
    {
        if (direction == 'x')
        {
            QadSpawnPos.Translate(1, 0, y * -1);
        }
        else if (direction == 'y')
        {
            QadSpawnPos.Translate(0, 0, 1);
        }
    }
    //CREAR MAPA

    public Qad SearchQadList(GameObject QadCheck)
    {
        Qad QadFind = null; 

        for (int i = 0; i < QadList.Length; i++)
        {
            if (QadCheck.gameObject == QadList[i].gameObject)
            {
                QadFind = QadList[i].gameObject.GetComponent<Qad>();
                Debug.Log(QadFind);
            }
        }
        return QadFind;
  
    }

}
