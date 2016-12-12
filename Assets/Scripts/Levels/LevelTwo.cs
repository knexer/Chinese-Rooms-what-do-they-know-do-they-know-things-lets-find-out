﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTwo : Level {

    public override TabletState Evaluate(TabletState input) {
        TabletState output = new TabletState(input);
        output.TopLeft.Symbol = TabletCell.Symbols.Eye;
        output.BottomRight.Symbol = TabletCell.Symbols.Snake;
        return output;
    }
}
