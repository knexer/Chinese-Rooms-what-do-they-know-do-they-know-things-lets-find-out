using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleColorCellMachine : CellMachine {
	public override void Manipulate(float tickDelta) {
		TabletCell tabletCell = GridCell.GetInput ();
		if (tabletCell == null) {
			return;
		}
		if (tabletCell.Color == TabletCell.Colors.Blue) {
			tabletCell.Color = TabletCell.Colors.Green;
		}
		if (tabletCell.Color == TabletCell.Colors.Green) {
			tabletCell.Color = TabletCell.Colors.Blue;
		}
	}
}
