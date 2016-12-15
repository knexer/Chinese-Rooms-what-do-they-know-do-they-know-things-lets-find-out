using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Does not actually end the level.  Sends the Tablet back to the beginning instead :|
 */
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
        if (this.GridVertex.X == MachineGrid.Obj.Input.GridVertexX 
            && this.GridVertex.Y == MachineGrid.Obj.Input.GridVertexY)
        {
			TickController.Obj.Pause();
			TestButton.RunCompleted = true;
        }
    }

    protected override void Start()
    {
        GridVertex upperRightVertex = MachineGrid.Obj.GridVertices[
            MachineGrid.Obj.GridVertices.GetLength(0) - 2,
            MachineGrid.Obj.GridVertices.GetLength(1) - 2].GetComponent<GridVertex>();
        upperRightVertex.VertexMachine = this;
        Vector3 position = upperRightVertex.transform.position;
        position.z = 0.0f;
        transform.position = position;

        TickController.ManipulateTickEvent += Manipulate;
    }

    protected override void OnDestroy()
    {
        TickController.ManipulateTickEvent -= Manipulate;
    }
}