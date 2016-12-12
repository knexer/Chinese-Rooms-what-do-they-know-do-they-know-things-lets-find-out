using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleOnOffCellMachine : CellMachine {
	public override void Manipulate(float tickDelta) {
		TabletCell tabletCell = GridCell.GetInput ();
		if (tabletCell == null) {
			return;
		}
		if (tabletCell.Color == TabletCell.Colors.None) {
			tabletCell.Color = TabletCell.Colors.Green;
		} else {
			tabletCell.Color = TabletCell.Colors.None;
		}
	}

	public override void OnPlace() {}

	public override void OnRemove() {}

	public override void CheckCondition() {}
}
