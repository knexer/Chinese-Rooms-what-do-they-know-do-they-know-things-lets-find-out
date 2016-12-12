using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenConditionalCellMachine : ConditionalCellMachine {
	public override void CheckCondition() {
		TabletCell tabletCell = GridCell.GetInput ();
		if (tabletCell == null) {
			return;
		}
		if (tabletCell.Color == TabletCell.Colors.Green) {
			OnMeetsCondition ();
		}
	}
}
