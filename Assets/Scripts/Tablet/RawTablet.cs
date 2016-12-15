using System;
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

    public override bool Equals(object obj)
    {
        ITablet otherTablet = obj as ITablet;
        if (otherTablet == null) return false;

        return this.TabletEquals(otherTablet);
    }

    public override int GetHashCode()
    {
        return TopLeft.GetHashCode()
            + TopRight.GetHashCode() * 17
            + BottomLeft.GetHashCode() * 17 * 17
            + BottomRight.GetHashCode() * 17 * 17 * 17;
    }
}

public class RawTabletCell : ITabletCell {
    public TabletColor Color { get; set; }
    public TabletSymbol Symbol { get; set; }

    public override bool Equals(object obj)
    {
        ITabletCell otherTablet = obj as ITabletCell;
        if (otherTablet == null) return false;

        return this.TabletEquals(otherTablet);
    }

    public override int GetHashCode()
    {
        return Color.GetHashCode() + 17 * Symbol.GetHashCode();
    }
}
