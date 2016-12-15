using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeConditionalCellMachine : ConditionalCellMachine {
	public override void CheckCondition() {
		GameTabletCell tabletCell = GridCell.GetInput ();
		if (tabletCell == null) {
			return;
		}
		if (tabletCell.Symbol == TabletSymbol.Eye) {
			OnMeetsCondition ();
		}
	}
}
