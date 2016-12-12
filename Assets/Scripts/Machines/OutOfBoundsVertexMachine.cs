using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBoundsVertexMachine : VertexMachine
{

    protected override void Start()
    {
        return;
    }

    protected override void Manipulate(float tickTime)
    {
        TickController.Obj.TriggerOutOfBounds();
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
