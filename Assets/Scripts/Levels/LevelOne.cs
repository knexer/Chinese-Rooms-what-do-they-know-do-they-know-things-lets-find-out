using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOne : Level {

    public override TabletState Evaluate(TabletState input) {
        return new TabletState(input);
    }
}
