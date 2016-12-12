using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBoundsVertexMachine : VertexMachine
{

    protected override void Start()
    {
        TickController.ManipulateTickEvent += Manipulate;
    }

    protected override void Manipulate(float tickTime)
    {
        if (GridVertex.Grid.CurrentInput != null)
        {
            // is there a tile over us?
            if (GridVertex.Grid.CurrentInput.gridVertexX == this.GridVertex.X
                && GridVertex.Grid.CurrentInput.gridVertexY == this.GridVertex.Y)
            {
                TickController.Obj.TriggerOutOfBounds();
            }
        }
    }

    protected override void OnDestroy()
    {
    }

    public override void OnPlace()
    {
        return;
    }

    public override void OnRemove()
    {
        return;
    }
}