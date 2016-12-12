﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueConditionalCellMachine : ConditionalCellMachine {
	public override void Manipulate(float tickDelta) {
		TabletCell tabletCell = GridCell.GetInput ();
		if (tabletCell == null) {
			return;
		}
		if (tabletCell.Color == TabletCell.Colors.Blue) {
			OnMeetsCondition ();
		}
	}
}
