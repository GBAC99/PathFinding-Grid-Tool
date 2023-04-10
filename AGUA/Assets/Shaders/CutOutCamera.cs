using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutOutCamera : MonoBehaviour
{
    [SerializeField]
    private Transform targetObject;

    [SerializeField]
    private LayerMask wallMask;

    private Camera mainCam;
    private CameraControler cameraControler;

    public Material cutoutMat;
    public Material fullMat;

    public bool wallCutout;
    public Vector2 _cutOutPos;

    private void Awake()
    {
        mainCam = GetComponent<Camera>();
        cameraControler = GetComponent<CameraControler>();
    }

    private void Start()
    {
        targetObject = cameraControler.target;
        wallCutout = false;
    }

   

    // Update is called once per frame
    void Update()
    {
        //Posicion del player y lugar del cutout en pantalla
        Vector2 cutoutPos = mainCam.WorldToViewportPoint(targetObject.position);
        cutoutPos.y /= (Screen.width / Screen.height);
        _cutOutPos = cutoutPos;
        Vector3 offset = targetObject.position - transform.position;

        //RayCasting para detectar un muro entre el player y la camara

        Ray cameraRay = Camera.main.ScreenPointToRay(targetObject.position);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, offset,out hit))
        {
            Debug.DrawRay(transform.position, hit.point);
            if (hit.collider.tag == "WALL")
            {
                Debug.Log("WallCutout : "+wallCutout);
                wallCutout = true;
            }
            else
            {
                wallCutout = false;
            }
        }
        else
        {
            wallCutout = false;
        }



        /*Ray cameraRay = Camera.main.ScreenPointToRay(targetObject.position);
        RaycastHit[] hitObjects = Physics.RaycastAll(transform.position, offset,offset.magnitude, wallMask);
        for (int i = 0; i < hitObjects.Length; i++)
        {
            if (hitObjects[i].collider.tag == "WALL")
            {
                wallCutout = true;
               /* hitObjects[i].transform.GetComponent<Renderer>().material = cutoutMat;
                Material[] materials = hitObjects[i].transform.GetComponent<Renderer>().materials;
                for (int m = 0; m < materials.Length; m++)
                {
                    if ((hitObjects[i].point - transform.position).magnitude < offset.magnitude)
                    {
                        materials[m].SetVector("_CutOutPos", cutoutPos);
                        materials[m].SetFloat("_CutOutSize", 0.1f);
                        materials[m].SetFloat("_FallOfSize", 0.05f);
                        materials[m].SetFloat("_Alpha", 1f);
                    }
                    else
                    {
                        materials[m].SetVector("_CutOutPos", cutoutPos);
                        materials[m].SetFloat("_CutOutSize", 0.1f);
                        materials[m].SetFloat("_FallOfSize", 0.05f);
                        materials[m].SetFloat("_Alpha", 0f);
                    }

                }
                
            }
            else if (hitObjects[i].collider.tag != "WALL" || hitObjects[i].collider == null)
            {
                wallCutout = false;
            }*/

    }

}

