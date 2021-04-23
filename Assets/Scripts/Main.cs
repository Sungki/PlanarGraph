using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    [SerializeField] [Range(1,60)] private int TotalNodes = 20;
    [SerializeField] private float Height = 0f;

    private int totalGrid = 100;                                                   // 10 x 10
    private List<GameObject> listNodes = new List<GameObject>();
    private List<GameObject> listLines = new List<GameObject>();

    private Dictionary<int, Vector3> grid = new Dictionary<int, Vector3>();
    private List<int> gridPos = new List<int>();

    private GameObject nodePrefab;
    private GameObject linePrefab;

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

    Vector3 FindClosestNode(List<Node> obj, Vector3 pos, bool withvisited = true)
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
                if(withvisited)
                {
                    if(!obj[i].visited)
                    {
                        closestPos = distance;
                        objIndex = i;
                    }
                }
                else
                {
                    closestPos = distance;
                    objIndex = i;
                }
            }
        }

        return obj[objIndex].transform.position;
    }

    void Start()
    {
        SetGrid();
        RandomPositionInGrid();

        nodePrefab = Resources.Load<GameObject>("Node");
        linePrefab = Resources.Load<GameObject>("Line");

        GeneratedNode();
        GeneratedLine();

/*        for (int i = 0; i < listNodes.Count; i++)
        {
            if(!listNodes[i].visited)
            {
                GameObject lastLine = Instantiate(linePrefab);
                lastLine.GetComponent<Line>().startPos = listNodes[0].position;
                lastLine.GetComponent<Line>().endPos = listNodes[1].position;
            }
        }*/
    }

    void GeneratedNode()
    {
        for (int i = 1; i <= TotalNodes; i++)
        {
            Vector3 pos = grid[gridPos[i - 1]];
            var node = Instantiate(nodePrefab, pos, Quaternion.identity);
            node.GetComponent<Node>().visited = false;
            listNodes.Add(node);
        }
    }

    void GeneratedLine()
    {
        for (int i = 0; i < TotalNodes - 1; i++)
        {
            listLines.Add(Instantiate(linePrefab));
            listLines[i].GetComponent<Line>().SetPoint(listNodes[i].transform.position, listNodes[i + 1].transform.position);
            listNodes[i].GetComponent<Node>().visited = true;

            //            listLines[i].GetComponent<Line>().endPos = FindClosestNode(listNodes, listLines[i].GetComponent<Line>().startPos);
        }
    }

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
