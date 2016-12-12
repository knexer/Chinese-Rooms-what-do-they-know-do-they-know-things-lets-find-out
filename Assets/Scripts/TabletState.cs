﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class TabletState {  
    public TabletCellState TopLeft;
    public TabletCellState TopRight;
    public TabletCellState BottomLeft;
    public TabletCellState BottomRight;

    public bool Equals(TabletState other) {
        bool equality_state = TopLeft.Equals(other.TopLeft);
        equality_state &= TopRight.Equals(other.TopRight);
        equality_state &= BottomLeft.Equals(other.BottomLeft);
        equality_state &= BottomRight.Equals(other.BottomRight);
        return equality_state;
    }
}

public class TabletCellState {
    public TabletCell.Colors Color;
    public TabletCell.Symbols Symbol;

    public bool Equals(TabletCellState other) {
        return other.Color == Color && other.Symbol == Symbol;
    }
}
