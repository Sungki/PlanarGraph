using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    [SerializeField] [Range(1,100)] private int TotalNodes = 20;
    [SerializeField] private float Height = 0;

    private int totalGrid = 100;                                                   // 10 x 10

    private GameObject nodePrefab;
    private List<GameObject> listNodes = new List<GameObject>();
    private Dictionary<int, Vector3> grid = new Dictionary<int, Vector3>();
    private List<int> listPos = new List<int>();

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

            if(listPos.Contains(key))
            {
                key = Random.Range(1, totalGrid);
            }
            else
            {
                listPos.Add(key);
                i++;
            }
        }

        listPos.Sort();
    }

    void Start()
    {
        SetGrid();
        RandomPositionInGrid();

//        foreach (int i in listPos)
//            print(i);

//        print(listPos.Count);

        nodePrefab = Resources.Load("Node") as GameObject;
        for (int i = 1; i <= TotalNodes; i++)
            listNodes.Add(Instantiate(nodePrefab, grid[listPos[i-1]] ,Quaternion.identity));
    }

    void Update()
    {
        
    }
}
