using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamConditionalSkellMachine : ConditionalCellMachine {
	public override void CheckCondition() {
		GameTabletCell tabletCell = GridCell.GetInput ();
		if (tabletCell == null) {
			return;
		}
		if (tabletCell.Symbol == TabletSymbol.Dream) {
			OnMeetsCondition ();
		}
	}
}
