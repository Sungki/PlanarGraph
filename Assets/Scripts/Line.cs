using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Line : MonoBehaviour
{    
    private LineRenderer render;
    private Vector3[] pos = new Vector3[2];
    public Vector3 startPos;
    public Vector3 endPos;

    // Setting the start and the end points
    public void SetPoint(Vector3 start, Vector3 end)
    {
        startPos = start;
        endPos = end;
    }

    void Start()
    {
        render = GetComponent<LineRenderer>();
        render.material = new Material(Shader.Find("Standard"));
        render.startWidth = 0.2f;
        render.endWidth = 0.2f;
        render.positionCount = 2;
        pos[0] = startPos;
        pos[1] = endPos;
        render.SetPositions(pos);
    }
}
