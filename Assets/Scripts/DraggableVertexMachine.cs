using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableVertexMachine : DraggableMachine
{
    public float distanceThreshold = Mathf.Infinity;

    private const float DRAG_Z_DEPTH = -300.0f;

    private MachineGrid grid;

    private bool dragging;

    // Use this for initialization
    void Start()
    {
        grid = FindObjectOfType<MachineGrid>();
    }

    private void OnMouseDown()
    {
        StartDrag();
    }

    private void Update()
    {
        if (dragging) {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(transform.position.x, transform.position.y, DraggableVertexMachine.DRAG_Z_DEPTH);
            Vector3 closestVertexPosition = grid.getClosestVertex(transform.position).transform.position;

            if (Vector2.Distance(transform.position, closestVertexPosition) < distanceThreshold) {
                closestVertexPosition.z = 0.0f;
                transform.position = closestVertexPosition;
            }
        }
    }

    private void OnMouseUp()
    {
        if (dragging)
        {
            dragging = false;
            // place yoself
            GridVertex closestVertex = grid.getClosestVertex(transform.position);
            if (closestVertex != null
                && closestVertex.VertexMachine == null
                && Vector2.Distance(closestVertex.transform.position, transform.position) < 0.01f)
            {
                closestVertex.VertexMachine = GetComponent<VertexMachine>();
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }

    public override void StartDrag() {
        dragging = true;

        // remove from whatever it's attached to
        GridVertex closestVertex = grid.getClosestVertex(transform.position);
        if (closestVertex != null
            && closestVertex.VertexMachine != null
            && Vector2.Distance(closestVertex.transform.position, transform.position) < 0.01f) {
            if (closestVertex.VertexMachine == GetComponent<VertexMachine>()) {
                closestVertex.VertexMachine = null;
            }
        }
    }
}
