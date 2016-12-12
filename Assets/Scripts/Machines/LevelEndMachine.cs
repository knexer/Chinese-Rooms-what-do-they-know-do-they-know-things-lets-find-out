using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndMachine : VertexMachine
{
    public override void OnPlace()
    {

    }

    public override void OnRemove()
    {

    }

    protected override void Manipulate(float tickTime)
    {
        if (GridVertex.Grid.CurrentInput.gridVertexX == GridVertex.X
            && GridVertex.Grid.CurrentInput.gridVertexY == GridVertex.Y)
        {
            // TODO just reset the thing, don't load the next level.
            LevelManager.Obj.LoadNextLevel();
        }
    }

    protected override void Start()
    {
        TickController.ManipulateTickEvent += Manipulate;
    }

    protected override void OnDestroy()
    {
        TickController.ManipulateTickEvent -= Manipulate;
    }
}
