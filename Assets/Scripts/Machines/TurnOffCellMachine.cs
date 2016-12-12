using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffCellMachine : CellMachine {
	public override void Manipulate(float tickDelta) {
		TabletCell tabletCell = GridCell.GetInput ();
		if (tabletCell == null) {
			return;
		}
		SoundManager.Instance.PlaySound(SoundManager.SoundTypes.StampAppliedGlow);
		tabletCell.Color = TabletCell.Colors.None;
	}

	public override void OnPlace() {
	}

	public override void OnRemove() {}

	public override void CheckCondition() {}
}
