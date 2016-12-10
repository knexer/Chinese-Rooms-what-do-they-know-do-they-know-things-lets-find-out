using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableVertexMachine : MonoBehaviour
{
    public float distanceThreshold = Mathf.Infinity;

    private MachineGrid grid;

    // Use this for initialization
    void Start()
    {
        grid = FindObjectOfType<MachineGrid>();
    }

    private void OnMouseDrag()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        Vector3 closestVertexPosition = grid.getClosestVertex(transform.position).transform.position;

        if (Vector2.Distance(transform.position, closestVertexPosition) < distanceThreshold)
        {
            transform.position = closestVertexPosition;
        }

        Debug.Log("FUCK");
    }
}
