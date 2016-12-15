using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableVertexMachine : DraggableMachine
{
    public float distanceThreshold = Mathf.Infinity;

    private const float DRAG_Z_DEPTH = -300.0f;

    private MachineGrid grid;

    public bool dragging { get; private set; }

    private bool wasDraggingLastFrame;

    // Use this for initialization
    void Awake()
    {
        grid = FindObjectOfType<MachineGrid>();
    }

    private void Update()
    {
        if (TickController.Obj.Mode != TickController.TimeState.Stopped)
        {
            if (dragging)
            {
                AbortDrag();
            }
        }
        if (dragging) {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(transform.position.x, transform.position.y, DraggableVertexMachine.DRAG_Z_DEPTH);
            Vector3 closestVertexPosition = grid.getClosestVertex(transform.position).transform.position;

            if (Vector2.Distance(transform.position, closestVertexPosition) < distanceThreshold) {
                closestVertexPosition.z = 0.0f;
                transform.position = closestVertexPosition;                
            }
        }

        if (wasDraggingLastFrame && Input.GetMouseButtonUp(0))
        {
            StopDrag();
        }

        wasDraggingLastFrame = dragging;
    }

    private void OnMouseDown()
    {
        StartDrag();
    }

    private void StopDrag()
    {
        dragging = false;
        // place yoself
        GridVertex closestVertex = grid.getClosestVertex(transform.position);
        if (closestVertex != null
            && closestVertex.VertexMachine == null
            && Vector2.Distance(closestVertex.transform.position, transform.position) < 0.01f)
        {
            closestVertex.VertexMachine = GetComponent<VertexMachine>();
            closestVertex.VertexMachine.OnPlace();

            SoundManager.Instance.PlaySound(SoundManager.SoundTypes.PlaceDownMachine);
        }
        else
        {
            AbortDrag();
        }
    }

    private void AbortDrag()
    {
        dragging = false;
        wasDraggingLastFrame = false;
        SoundManager.Instance.PlaySound(SoundManager.SoundTypes.MachineDestroyed);
        Destroy(this.gameObject);
    }

    public override void StartDrag()
    {
        GridVertex parentVertex = GetComponent<VertexMachine>().GridVertex;
        // Can't pick up if it's currently running or paused.
        if (TickController.Obj.Mode != TickController.TimeState.Stopped)
        {
            if (parentVertex == null)
            {
                Destroy(gameObject);
                return;
            }

            return;
        }

        dragging = true;
        
        SoundManager.Instance.PlaySound(SoundManager.SoundTypes.PickupMachine);

        // remove from whatever it's attached to
        if (parentVertex != null)
        {
            parentVertex.VertexMachine.OnRemove();
            parentVertex.VertexMachine = null;
        }
    }
}
