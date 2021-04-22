using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    [SerializeField] [Range(1,60)] private int TotalNodes = 20;
    [SerializeField] private float Height = 0f;

    private int totalGrid = 100;                                                   // 10 x 10

    private GameObject nodePrefab;
    private GameObject linePrefab;

    struct Node
    {
        public GameObject obj;
        public bool visited;
        public Vector3 position;
    };

    //    private List<GameObject> listNodes = new List<GameObject>();

    private List<Node> listNodes = new List<Node>();

    private List<GameObject> listLines = new List<GameObject>();

    private Dictionary<int, Vector3> grid = new Dictionary<int, Vector3>();
    private List<int> gridPos = new List<int>();

    void SetGrid()
    {
        int x = 0;
        int y = 0;
        for(int i=1; i <= totalGrid ; i++)
        {
            grid.Add(i, new Vector3(x++ * 2, y * 2, Height));
            if(i%10 == 0)
            {
                x = 0;
                y++;
            }
        }
    }

    void RandomPositionInGrid()
    {
        for(int i =0; i < TotalNodes;)
        {
            int key = Random.Range(1, totalGrid);

            if(gridPos.Contains(key))
            {
                key = Random.Range(1, totalGrid);
            }
            else
            {
                gridPos.Add(key);
                i++;
            }
        }

        gridPos.Sort();
    }

    Vector3 FindClosestNode(List<GameObject> obj, Vector3 pos)
    {
        float closestPos = 0f;
        float distance = 0f;
        int objIndex = 0;

        closestPos = Vector3.Distance(pos, obj[0].transform.position);

        for(int i = 1; i < obj.Count; i++)
        {
            distance = Vector3.Distance(pos, obj[i].transform.position);
            if(distance < closestPos && pos != obj[i].transform.position)
            {
                closestPos = distance;
                objIndex = i;
            }
        }

        return obj[objIndex].transform.position;
    }

    void Start()
    {
        SetGrid();
        RandomPositionInGrid();

        nodePrefab = Resources.Load("Node") as GameObject;
        for (int i = 1; i <= TotalNodes; i++)
        {
            Vector3 pos = grid[gridPos[i - 1]];

            Node n = new Node();
            n.obj = Instantiate(nodePrefab, pos, Quaternion.identity);
            n.visited = false;
            n.position = pos;
            listNodes.Add(n);
//            listNodes.Add(Instantiate(nodePrefab, grid[listPos[i - 1]], Quaternion.identity));
        }

        linePrefab = Resources.Load("Line") as GameObject;
        for (int i = 0; i < TotalNodes - 1; i++)
        {
            listLines.Add(Instantiate(linePrefab));
            listLines[i].GetComponent<Line>().startPos = listNodes[i].position;
            listLines[i].GetComponent<Line>().endPos = listNodes[i+1].position;

//            listLines[i].GetComponent<Line>().endPos = FindClosestNode(listNodes, listLines[i].GetComponent<Line>().startPos);
        }
    }
}
