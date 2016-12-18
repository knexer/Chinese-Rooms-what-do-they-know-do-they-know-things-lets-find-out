using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBoundsVertexMachine : VertexMachine
{
    protected override void Manipulate(float tickTime)
    {
        if (GridVertex.Grid.GridTablet != null
            && GridVertex.Grid.GridTablet.GridVertexX == this.GridVertex.X
            && GridVertex.Grid.GridTablet.GridVertexY == this.GridVertex.Y)
                LevelStateManager.LevelCompleted();
    }

    public override void OnPlace() { }
    public override void OnRemove() { }
}