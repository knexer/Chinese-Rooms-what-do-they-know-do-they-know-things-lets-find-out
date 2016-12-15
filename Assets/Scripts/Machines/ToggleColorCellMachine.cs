using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleColorCellMachine : CellMachine {
	public override void Manipulate(float tickDelta) {
		GameTabletCell tabletCell = GridCell.GetInput ();
		if (tabletCell == null) {
			return;
		}
		if (tabletCell.Color == TabletColor.Blue) {
			SoundManager.Instance.PlaySound(SoundManager.SoundTypes.StampAppliedGlow);
			tabletCell.Color = TabletColor.Green;
		}
		if (tabletCell.Color == TabletColor.Green) {
			SoundManager.Instance.PlaySound(SoundManager.SoundTypes.StampAppliedGlow);
			tabletCell.Color = TabletColor.Blue;
		}
	}

	public override void OnPlace() {}

	public override void OnRemove() {}

	public override void CheckCondition() {}
}
