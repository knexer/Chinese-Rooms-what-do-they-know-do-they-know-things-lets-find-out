using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDreamSymbolCellMachine : CellMachine {
	public override void Manipulate(float tickDelta) {
		TabletCell tabletCell = GridCell.GetInput ();
		if (tabletCell == null) {
			return;
		}
		tabletCell.Symbol = TabletCell.Symbols.Dream;
	}

	public override void OnPlace() {}

	public override void OnRemove() {}
}