﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeConditionalCellMachine : ConditionalCellMachine {
	public override void Manipulate(float tickDelta) {
		TabletCell tabletCell = GridCell.GetInput ();
		if (tabletCell == null) {
			return;
		}
	}
}