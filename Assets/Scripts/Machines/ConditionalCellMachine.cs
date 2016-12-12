using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ConditionalCellMachine : CellMachine {
	public void OnMeetsCondition() {
		VertexMachine inputMachine = GridCell.GetTabletCenter ().VertexMachine;
		if (inputMachine != null && inputMachine.GetType () == typeof(Mover)) {
			Mover moveMachine = (Mover) inputMachine;
			moveMachine.MeetConditional (GridCell.X, GridCell.Y);
		}
	}

	public void OnPlace() {
		GridVertex[] vertices = GridCell.GetSurroundingVertices ();
		for (int i = 0; i < 4; i++) {
			VertexMachine machine = vertices [i].VertexMachine;
			if (machine != null && machine.GetType () == typeof(Mover)) {
				Mover moveMachine = (Mover) machine;
				moveMachine.AddConditional (GridCell.X, GridCell.Y);
			}
		}
	}

	public void OnRemove() {
		GridVertex[] vertices = GridCell.GetSurroundingVertices ();
		for (int i = 0; i < 4; i++) {
			VertexMachine machine = vertices [i].VertexMachine;
			if (machine != null && machine.GetType () == typeof(Mover)) {
				Mover moveMachine = (Mover) machine;
				moveMachine.RemoveConditional (GridCell.X, GridCell.Y);
			}
		}
	}
}
