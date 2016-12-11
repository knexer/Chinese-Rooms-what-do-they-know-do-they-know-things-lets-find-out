﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableVertexMachine : MonoBehaviour
{
    public float distanceThreshold = Mathf.Infinity;

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
        GridVertex closestVertex = grid.getClosestVertex(transform.position);
        if (closestVertex != null
            && closestVertex.VertexMachine != null
            && Vector2.Distance(closestVertex.transform.position, transform.position) < 0.01f)
        {
            if (closestVertex.VertexMachine == GetComponent<VertexMachine>())
            {
                closestVertex.VertexMachine = null;
            }
        }
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
                // TODO put it back on the shelf
            }
        }
    }
}