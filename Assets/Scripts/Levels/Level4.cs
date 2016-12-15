using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4 : Level {

    public override ITablet Evaluate(ITablet input) {
        ITablet output = new RawTablet().SetState(input);
        output.BottomRight.Symbol = output.TopLeft.Symbol;
        return output;
    }
}
