using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetEyeSymbolCellMachine : CellMachine {
	public override void Manipulate(float tickDelta) {
		GameTabletCell tabletCell = GridCell.GetInput ();
		if (tabletCell == null) {
			return;
		}
		SoundManager.Instance.PlaySound(SoundManager.SoundTypes.StampAppliedSymbol);
		tabletCell.Symbol = TabletSymbol.Eye;
	}

	public override void OnPlace() {}

	public override void OnRemove() {}

	public override void CheckCondition() {
	}
}