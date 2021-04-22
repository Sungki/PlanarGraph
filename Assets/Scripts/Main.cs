using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    [SerializeField] [Range(1,90)] private int TotalNodes = 20;
    [SerializeField] private float Height = 0;

    private GameObject nodePrefab;
    private List<GameObject> listNodes = new List<GameObject>();

    void Start()
    {
        nodePrefab = Resources.Load("Node") as GameObject;

        for (int i = 0; i < TotalNodes; i++)
            listNodes.Add(Instantiate(nodePrefab, new Vector3(Random.Range(0, 10), Random.Range(0, 10), Height), Quaternion.identity));
    }

    void Update()
    {
        
    }
}
