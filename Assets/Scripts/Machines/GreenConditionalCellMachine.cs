using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenConditionalCellMachine : ConditionalCellMachine {
	public override void Manipulate(float tickDelta) {
		TabletCell tabletCell = GridCell.GetInput ();
		if (tabletCell == null) {
			return;
		}
	}
}
