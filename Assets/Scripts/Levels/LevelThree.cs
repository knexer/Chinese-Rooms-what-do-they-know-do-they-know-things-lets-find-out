using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelThree : Level {

    public override ITablet Evaluate(ITablet input) {
        ITablet output = new RawTablet().SetState(input);
        output.RotateCounterclockwise();
        output.TopRight.Symbol = TabletSymbol.Snake;
        output.BottomRight.Symbol = TabletSymbol.Snake;
        return output;
    }
}
