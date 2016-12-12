using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4 : Level {

    public override TabletState Evaluate(TabletState input) {
        TabletState output = new TabletState(input);
        output.TopRight.Symbol = output.TopLeft.Symbol;
        output.TopRight.Color = TabletCell.Colors.Green;
        return output;
    }
}
