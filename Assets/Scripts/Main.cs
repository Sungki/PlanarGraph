using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    [SerializeField] [Range(1,90)] private int TotalNodes = 20;
    [SerializeField] private float Height = 0;

    private GameObject nodePrefab;
    private List<GameObject> listNodes = new List<GameObject>();

    private Dictionary<int, Vector3> grid = new Dictionary<int, Vector3>();

    void SetGrid()
    {
        int x = 0;
        int y = 0;
        for(int i=1; i <= TotalNodes; i++)
        {
            grid.Add(i, new Vector3(x++ * 5, y * 5, Height));
            if(i%10 == 0)
            {
                x = 0;
                y++;
            }
        }
    }

    void Start()
    {
        SetGrid();
        nodePrefab = Resources.Load("Node") as GameObject;
        for (int i = 1; i <= TotalNodes; i++)
            listNodes.Add(Instantiate(nodePrefab, grid[i], Quaternion.identity));
    }

    void Update()
    {
        
    }
}
