using System;
using UnityEngine;

[ExecuteInEditMode] 
[SelectionBase] //selects the object this is on so you don't accidently grab a child of that object
[RequireComponent(typeof(Waypoint))]
public class CubeEditor : MonoBehaviour
{
    Waypoint waypoint;

    private void Awake()
    {
        waypoint = GetComponent<Waypoint>();
    }

    void Update()
    {
        SnapToGrid();
        UpdateLabel();
    }

    private void SnapToGrid()
    {
        int gridSize = waypoint.GetGridSize();
        transform.position = new Vector3(
                    waypoint.GetGridPos().x * gridSize, 
                    0f, 
                    waypoint.GetGridPos().y * gridSize
                    ); //is y because it is 2D but is being mapped from 3D. y corresponds to z in world
    }

    private void UpdateLabel()
    {
        TextMesh textMesh = GetComponentInChildren<TextMesh>();
        string labelText =
                waypoint.GetGridPos().x + 
                "," + 
                waypoint.GetGridPos().y;
        textMesh.text = labelText;
        gameObject.name = labelText; ;
    }
}

