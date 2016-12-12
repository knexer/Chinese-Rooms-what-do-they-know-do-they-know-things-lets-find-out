using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnCellMachine : CellMachine {
	public override void Manipulate(float tickDelta) {
		TabletCell tabletCell = GridCell.GetInput ();
		if (tabletCell == null) {
			return;
		}
		tabletCell.Color = TabletCell.Colors.Green;
	}

	public override void OnPlace() {}

	public override void OnRemove() {}

	public override void CheckCondition() {
	}
}