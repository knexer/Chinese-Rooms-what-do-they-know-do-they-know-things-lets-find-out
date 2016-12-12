using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelThree : Level {

    public override TabletState Evaluate(TabletState input) {
        TabletState output = new TabletState(input);
        output.RotateCounterclockwise();
        output.TopRight.Symbol = TabletCell.Symbols.Snake;
        output.BottomRight.Symbol = TabletCell.Symbols.Snake;
        return output;
    }
}
