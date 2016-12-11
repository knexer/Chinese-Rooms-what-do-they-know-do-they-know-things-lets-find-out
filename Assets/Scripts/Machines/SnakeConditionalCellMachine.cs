using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeConditionalCellMachine : ConditionalCellMachine {
	public override void Manipulate(float tickDelta) {
		TabletCell tabletCell = GridCell.GetInput ();
		if (tabletCell == null) {
			return;
		}
	}
}
