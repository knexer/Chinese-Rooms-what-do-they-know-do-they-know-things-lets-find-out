using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOne : Level {

    public override ITablet Evaluate(ITablet input) {
        return new RawTablet().SetState(input);
    }
}
