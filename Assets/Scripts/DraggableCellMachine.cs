using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableCellMachine : DraggableMachine
{
    public float distanceThreshold = 0.2f;
    
    private const float DRAG_Z_DEPTH = -300.0f;

    private MachineGrid grid;

    private bool dragging;

    private bool wasDraggingLastFrame;

    // Use this for initialization
    void Start()
    {
        grid = FindObjectOfType<MachineGrid>();
    }

    private void Update()
    {
        if (dragging) {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(transform.position.x, transform.position.y, DraggableCellMachine.DRAG_Z_DEPTH);
            Vector3 closestCellPosition = grid.getClosestCell(transform.position).transform.position;

            if (Vector2.Distance(transform.position, closestCellPosition) < distanceThreshold) {
                closestCellPosition.z = 0.0f;
                transform.position = closestCellPosition;
            }

            if (wasDraggingLastFrame && Input.GetMouseButtonUp(0))
            {
                StopDrag();
            }

            wasDraggingLastFrame = dragging;
        }
    }

    private void OnMouseDown()
    {
        StartDrag();
    }

    /// <summary>
    /// Place the item if you can, destroy it if you can't.
    /// </summary>
    private void StopDrag()
    {
        dragging = false;
        // place yoself
        GridCell closestCell = grid.getClosestCell(transform.position);
        if (closestCell != null
            && closestCell.CellMachine == null
            && Vector2.Distance(closestCell.transform.position, transform.position) < 0.01f)
        {
            closestCell.CellMachine = GetComponent<CellMachine>();
            closestCell.CellMachine.OnPlace();

            SoundManager.Instance.PlaySound(SoundManager.SoundTypes.PlaceDownMachine);
        }
        else
        {
            SoundManager.Instance.PlaySound(SoundManager.SoundTypes.MachineDestroyed);
            Destroy(this.gameObject);
        }
    }

    public override void StartDrag() {
        dragging = true;

        SoundManager.Instance.PlaySound(SoundManager.SoundTypes.PickupMachine);

        // remove from whatever it's attached to
        if (grid != null) {
            GridCell closestCell = grid.getClosestCell(transform.position);
            if (closestCell != null
                && closestCell.CellMachine != null
                && Vector2.Distance(closestCell.transform.position, transform.position) < 0.01f) {
                if (closestCell.CellMachine == GetComponent<CellMachine>()) {
					closestCell.CellMachine.OnRemove ();
                    closestCell.CellMachine = null;
                }
            }
        }
    }
}
