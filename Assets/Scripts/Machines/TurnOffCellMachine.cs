using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffCellMachine : CellMachine {
	public override void Manipulate(float tickDelta) {
		TabletCell tabletCell = GridCell.GetInput ();
		if (tabletCell == null) {
			return;
		}
		tabletCell.Color = TabletCell.Colors.None;
	}
}
