using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    private GameObject nodePrefab;

    void Start()
    {
        nodePrefab = Resources.Load("Node") as GameObject;
        Instantiate(nodePrefab);
    }

    void Update()
    {
        
    }
}
