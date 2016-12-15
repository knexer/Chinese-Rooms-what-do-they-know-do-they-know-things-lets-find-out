using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTwo : Level {

    public override ITablet Evaluate(ITablet input) {
        ITablet output = new RawTablet().SetState(input);
        output.TopLeft.Symbol = TabletSymbol.Eye;
        output.BottomRight.Symbol = TabletSymbol.Snake;
        return output;
    }
}
