using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallShaderControler : MonoBehaviour
{


    public QadManager qadManager;

    public CameraControler coCam;
    Material thisMat;

    // Start is called before the first frame update
    void Awake()
    {
        qadManager = GameObject.FindGameObjectWithTag("QadManager").GetComponent<QadManager>();
        coCam = qadManager.mainCam;
        thisMat = gameObject.GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (coCam.GetComponent<CutOutCamera>().wallCutout)
        {
            Debug.Log("co");
            thisMat.SetVector("_CutOutPos", coCam.GetComponent<CutOutCamera>()._cutOutPos);
            thisMat.SetFloat("_CutOutSize", 0.1f);
            thisMat.SetFloat("_FallOfSize", 0.05f);
            thisMat.SetFloat("_Alpha", 1f);
        }
        else
        {
            thisMat.SetVector("_CutOutPos", coCam.GetComponent<CutOutCamera>()._cutOutPos);
            thisMat.SetFloat("_CutOutSize", 0.1f);
            thisMat.SetFloat("_FallOfSize", 0.05f);
            thisMat.SetFloat("_Alpha", 0f);
        }
    }
}
