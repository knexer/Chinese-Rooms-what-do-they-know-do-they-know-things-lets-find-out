using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableCellMachine : MonoBehaviour
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
        Vector3 closestCellPosition = grid.getClosestCell(transform.position).transform.position;

        if (Vector2.Distance(transform.position, closestCellPosition) < distanceThreshold)
        {
            transform.position = closestCellPosition;
        }
    }
}
