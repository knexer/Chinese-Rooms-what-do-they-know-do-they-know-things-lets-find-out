using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableCellMachine : MonoBehaviour
{
    public float distanceThreshold = 0.2;

    private MachineGrid grid;

    private bool dragging;

    // Use this for initialization
    void Start()
    {
        grid = FindObjectOfType<MachineGrid>();
    }

    private void OnMouseDown()
    {
        dragging = true;

        // remove from whatever it's attached to
        GridCell closestCell = grid.getClosestCell(transform.position);
        if (closestCell != null
            && closestCell.CellMachine != null
            && Vector2.Distance(closestCell.transform.position, transform.position) < 0.01f)
        {
            if (closestCell.CellMachine == GetComponent<VertexMachine>())
            {
                closestCell.CellMachine = null;
            }
        }
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

    private void OnMouseUp()
    {
        if (dragging)
        {
            dragging = false;
            // place yoself
            GridCell closestCell = grid.getClosestCell(transform.position);
            if (closestCell != null
                && closestCell.CellMachine == null
                && Vector2.Distance(closestCell.transform.position, transform.position) < 0.01f)
            {
                closestCell.CellMachine = GetComponent<CellMachine>();
            }
            else
            {
                // TODO put it back on the shelf
            }
        }
    }
}
