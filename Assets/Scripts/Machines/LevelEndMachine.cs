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
        Debug.Log("Manipulating.");
        Debug.Log(this.GetInstanceID());
        if (GridVertex == null)
        {
            Debug.Log("fuuu");
            return;
        }
        if (GridVertex.Grid.CurrentInput.gridVertexX == GridVertex.X
            && GridVertex.Grid.CurrentInput.gridVertexY == GridVertex.Y)
        {
            // TODO complete the level.
            Debug.Log("Level is done!!");
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
