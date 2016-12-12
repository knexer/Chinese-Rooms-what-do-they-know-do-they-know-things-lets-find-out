using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelThree : Level {

    public override TabletState Evaluate(TabletState input) {
        TabletState output = new TabletState(input);
        TabletCellState temp = output.TopLeft;
        output.TopLeft = output.TopRight;
        output.TopRight = output.BottomRight;
        output.BottomRight = output.BottomLeft;
        output.BottomLeft = temp;
        output.TopRight.Color = TabletCell.Colors.Green;
        output.BottomRight.Color = TabletCell.Colors.Green;
        return output;
    }
}
