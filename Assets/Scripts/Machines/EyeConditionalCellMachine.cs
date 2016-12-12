using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeConditionalCellMachine : ConditionalCellMachine {
	public override void CheckCondition() {
		TabletCell tabletCell = GridCell.GetInput ();
		if (tabletCell == null) {
			return;
		}
		if (tabletCell.Symbol == TabletCell.Symbols.Eye) {
			OnMeetsCondition ();
		}
	}
}
