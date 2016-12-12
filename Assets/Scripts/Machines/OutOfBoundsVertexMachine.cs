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

    public override void OnPlace()
    {
        return;
    }

    public override void OnRemove()
    {
        return;
    }
}
