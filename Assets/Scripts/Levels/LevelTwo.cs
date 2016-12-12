using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTwo : Level {

    public override TabletState Evaluate(TabletState input) {
        TabletState output = new TabletState(input);
        output.TopLeft.Color = TabletCell.Colors.Green;
        output.BottomRight.Color = TabletCell.Colors.Green;
        return output;
    }
}
