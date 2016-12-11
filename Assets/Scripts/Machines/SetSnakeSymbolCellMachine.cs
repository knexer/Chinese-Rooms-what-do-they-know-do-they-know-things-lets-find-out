using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSnakeSymbolCellMachine : CellMachine {
	public override void Manipulate(float tickDelta) {
		TabletCell tabletCell = GridCell.GetInput ();
		if (tabletCell == null) {
			return;
		}
		tabletCell.Symbol = TabletCell.Symbols.Snake;
	}
}