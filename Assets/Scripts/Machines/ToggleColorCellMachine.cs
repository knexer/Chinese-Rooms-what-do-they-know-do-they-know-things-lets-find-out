using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleColorCellMachine : CellMachine {
	public override void Manipulate(float tickDelta) {
		TabletCell tabletCell = GridCell.GetInput ();
		if (tabletCell == null) {
			return;
		}
		if (tabletCell.Color == TabletCell.Colors.Blue) {
			SoundManager.Instance.PlaySound(SoundManager.SoundTypes.StampAppliedGlow);
			tabletCell.Color = TabletCell.Colors.Green;
		}
		if (tabletCell.Color == TabletCell.Colors.Green) {
			SoundManager.Instance.PlaySound(SoundManager.SoundTypes.StampAppliedGlow);
			tabletCell.Color = TabletCell.Colors.Blue;
		}
	}

	public override void OnPlace() {}

	public override void OnRemove() {}

	public override void CheckCondition() {}
}
