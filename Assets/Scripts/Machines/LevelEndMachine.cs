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
        if (((GridVertex.Grid.CurrentInput.gridVertexX == GridVertex.X - 1
            && GridVertex.Grid.CurrentInput.gridVertexY == GridVertex.Y
			&& GridVertex.Grid.CurrentInput.MovementDirection == Mover.Direction.RIGHT))
			|| ((GridVertex.Grid.CurrentInput.gridVertexX == GridVertex.X
				&& GridVertex.Grid.CurrentInput.gridVertexY == GridVertex.Y - 1
				&& GridVertex.Grid.CurrentInput.MovementDirection == Mover.Direction.UP)))
        {
            // just reset the thing, don't load the next level.
			TickController.Obj.Pause();
			TestButton.RunCompleted = true;
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