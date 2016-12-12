using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeConditionalCellMachine : ConditionalCellMachine {
	public override void CheckCondition() {
		TabletCell tabletCell = GridCell.GetInput ();
		if (tabletCell == null) {
			return;
		}
		if (tabletCell.Symbol == TabletCell.Symbols.Snake) {
			OnMeetsCondition ();
		}
	}
}
