using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamConditionalSkellMachine : ConditionalCellMachine {
	public override void Manipulate(float tickDelta) {
		TabletCell tabletCell = GridCell.GetInput ();
		if (tabletCell == null) {
			return;
		}
		if (tabletCell.Symbol == TabletCell.Symbols.Dream) {
			OnMeetsCondition ();
		}
	}
}
