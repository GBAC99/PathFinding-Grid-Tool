using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QadMovable : MonoBehaviour
{

    protected QadManager qadManager;

    public float jumpHeight = 2;
    public float moveSpeed = 2;
    public int move = 5;
    public bool moving = false;
    List<Qad> selectableQads = new List<Qad>();

    GameObject[] QadList;

    public Stack<Qad> path = new Stack<Qad>();
    public Qad currentQad;

    Vector3 velocity = new Vector3();
    Vector3 heading = new Vector3();

    float halfHeight = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Init()
    {
        qadManager = FindObjectOfType<QadManager>();
        QadList = qadManager.QadList;
    }

    //Pathfinding Movement
    public void GetCurrentQad()
    {
        currentQad = GetTargetQad(gameObject);
        currentQad.current = true;
    }

    public Qad GetTargetQad(GameObject target)
    {
        RaycastHit hit;
        Qad qad = null;

        if (Physics.Raycast(target.transform.position, -Vector3.up, out hit, 1))
        {
            if (hit.collider.gameObject.tag == "Qad")
            {
                qad = hit.collider.GetComponent<Qad>();
            }
        }

        return qad;
    }

    public void ComputeAdjacencyLists()
    {
        foreach (GameObject qad in QadList)
        {
            Qad q = qad.GetComponent<Qad>();
            q.FindNeighbors(jumpHeight);
        }
    }

    //BFS algorithm
    public void FindSelectableQads()
    {
        ComputeAdjacencyLists();
        GetCurrentQad();
        Queue<Qad> process = new Queue<Qad>();

        process.Enqueue(currentQad);
        currentQad.visited = true;
        //currentQad.qParent = null;

        while (process.Count > 0)
        {
            Qad q = process.Dequeue();

            selectableQads.Add(q);
            q.selectable = true;

            if (q.distance < move)
            {
                foreach (Qad qad in q.adjacencyList)
                {
                    if (!qad.visited)
                    {
                        qad.qParent = q;
                        qad.visited = true;
                        qad.distance = 1 + q.distance;
                        process.Enqueue(qad);
                    }
                }
            }
        }
    }

    public void MoveToQad(Qad qad)
    {
        path.Clear();
        qad.target = true;
        moving = true;

        Qad next = qad;
        while (next != null)
        {
            path.Push(next);
            next = next.qParent;
        }

    }

    public void Move()
    {
        if (path.Count > 0)
        {
            Qad q = path.Peek();
            Vector3 targetPos = q.transform.position;

            targetPos.y += halfHeight + q.GetComponent<Collider>().bounds.extents.y+0.05f;

            if (Vector3.Distance(transform.position, targetPos) >= 0.05f)
            {
                CalculateHeading(targetPos);
                SetHorizontalVelocity();

                transform.forward = heading;
                transform.position += velocity * Time.deltaTime;

            }
            else
            {
                transform.position = targetPos;
                path.Pop();
            }

        }
        else
        {
            RemoveSelectableTiles();
            moving = false;
        }
    }

    protected void RemoveSelectableTiles()
    {

        if (currentQad != null)
        {
            currentQad.current = false;
            currentQad = null;
        }

        foreach (Qad q in selectableQads)
        {
            q.Reset();
        }

        selectableQads.Clear();

    }


    void CalculateHeading(Vector3 targetPos)
    {
        heading = targetPos - transform.position;
        heading.Normalize();
            
    }
    
    void SetHorizontalVelocity()
    {
        velocity = heading * moveSpeed;
    }

}
