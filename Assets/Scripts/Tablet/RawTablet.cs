﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class RawTablet : ITablet {
    public ITabletCell TopLeft { get; set; }
    public ITabletCell TopRight { get; set; }
    public ITabletCell BottomLeft { get; set; }
    public ITabletCell BottomRight { get; set; }

    public RawTablet() {
        TopLeft = new RawTabletCell();
        TopRight = new RawTabletCell();
        BottomLeft = new RawTabletCell();
        BottomRight = new RawTabletCell();
    }
}

public class RawTabletCell : ITabletCell {
    public TabletColor Color { get; set; }
    public TabletSymbol Symbol { get; set; }
}
