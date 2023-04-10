using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Qad : MonoBehaviour
{
    public bool walkable = true;
    public bool current = false;
    public bool target = false;
    public bool selectable = false;

    public List<Qad> adjacencyList = new List<Qad>();

    //bfs variables 
    public bool visited = false;
    public Qad qParent = null;
    public int distance = 0;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (current) GetComponent<Renderer>().material.color = Color.magenta;
        else if (target) GetComponent<Renderer>().material.color = Color.green;
        else if (selectable) GetComponent<Renderer>().material.color = Color.red;
        else GetComponent<Renderer>().material.color = Color.white;
    }

    public void Reset()
    {
        walkable = true;
        current = false;
        target = false;
        selectable = false;

        visited = false;
        qParent = null;
        distance = 0;
    }

    //Pathfinding

    public void FindNeighbors(float jumpHeight)
    {
        Reset();

        CheckQads(Vector3.forward, jumpHeight);
        CheckQads(-Vector3.forward, jumpHeight);
        CheckQads(Vector3.right, jumpHeight);
        CheckQads(-Vector3.right, jumpHeight);
    }

    public void CheckQads(Vector3 direction, float jumpHeight)
    {
        Vector3 halfExtents = new Vector3(0.25f, (1 + jumpHeight) / 2.0f, 0.25f);
        Collider[] colliders = Physics.OverlapBox(transform.position + direction, halfExtents);

        foreach (Collider col in colliders)
        {
            Qad qad = col.GetComponent<Qad>();
            if (qad != null && qad.walkable)
            {
                RaycastHit hit;

                if (!Physics.Raycast(qad.transform.position, Vector3.up, out hit, 1))
                {
                    adjacencyList.Add(qad);
                }

            }
        }
    }
}
