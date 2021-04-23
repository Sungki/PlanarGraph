using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    [SerializeField] [Range(1,60)] private int TotalNodes = 20;                     // Totoal Number of Nodes
    [SerializeField] private float Height = 0f;                                     // z-value

    private int totalGrid = 100;                                                    // 10 x 10
    private List<GameObject> listNodes = new List<GameObject>();                    // List of Nodes
    private List<GameObject> listLines = new List<GameObject>();                    // List of Lines

    private Dictionary<int, Vector3> grid = new Dictionary<int, Vector3>();         // Grid for Nodes in order to avoid to touch each other
    private List<int> gridPos = new List<int>();                                    // Random positions on Grid

    private GameObject nodePrefab;                                                  // Prefab for Node
    private GameObject linePrefab;                                                  // Prefab for Line

    // Initialoze Grids
    void InitGrid()
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

    // Random positions for Nodes on the Grid
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

    // I try to use this function
    // However, the one of nodes is not connecting to others.
    Vector3 FindClosestNode(List<GameObject> obj, Vector3 pos)
    {
        float closestPos = 0f;
        float distance = 0f;
        int objIndex = 0;

        closestPos = Vector3.Distance(pos, obj[0].transform.position);

        for(int i = 0; i < obj.Count; i++)
        {
            distance = Vector3.Distance(pos, obj[i].transform.position);

            if(distance < closestPos && pos != obj[i].transform.position && !obj[i].GetComponent<Node>().visited)
            {
                closestPos = distance;
                objIndex = i;
            }
        }

        return obj[objIndex].transform.position;
    }

    void Start()
    {
        InitGrid();
        RandomPositionInGrid();

        // Load Prefabs from the Resource folder
        nodePrefab = Resources.Load<GameObject>("Node");
        linePrefab = Resources.Load<GameObject>("Line");

        // Generated Nodes and lines
        GeneratedNode();
        GeneratedLine();
    }

    void GeneratedNode()
    {
        for (int i = 1; i <= TotalNodes; i++)
        {
            Vector3 pos = grid[gridPos[i - 1]];
            var node = Instantiate(nodePrefab, pos, Quaternion.identity);
            node.GetComponent<Node>().visited = false;
            listNodes.Add(node);                                            // Adding into the list
        }
    }

    void GeneratedLine()
    {
        for (int i = 0; i < TotalNodes - 1; i++)
        {
            listLines.Add(Instantiate(linePrefab));                         // Adding into the list

            // Draw Line with the start point and the end point
            listLines[i].GetComponent<Line>().SetPoint(listNodes[i].transform.position, listNodes[i + 1].transform.position);

            // I try to use this function
            // However, the one of nodes is not connecting to others.
//            listLines[i].GetComponent<Line>().SetPoint(listNodes[i].transform.position,
//                FindClosestNode(listNodes, listNodes[i].transform.position));

            // Setting visited boolean value
            listNodes[i].GetComponent<Node>().visited = true;
        }
    }

    // New button clicked on UI
    public void NewButtonClicked()
    {
        foreach (GameObject obj in listNodes)
            Destroy(obj);

        foreach (GameObject obj in listLines)
            Destroy(obj);

        listNodes.Clear();
        listLines.Clear();
        gridPos.Clear();

        RandomPositionInGrid();
        GeneratedNode();
        GeneratedLine();
    }
}
