using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface ITablet {
    ITabletCell TopLeft { get; set; }
    ITabletCell TopRight { get; set; }
    ITabletCell BottomLeft { get; set; }
    ITabletCell BottomRight { get; set; }
}

public interface ITabletCell {
    TabletColor Color { get; set; }
    TabletSymbol Symbol { get; set; }
}

public enum TabletColor {
    None,
    Green,
    Blue
}

public enum TabletSymbol {
    Eye,
    Snake,
    Dream
}

public static class TabletExtensions {
    public static bool TabletEquals(this ITabletCell one, ITabletCell two) {
        return two.Color == one.Color && two.Symbol == one.Symbol;
    }

    public static ITabletCell SetState(this ITabletCell one, ITabletCell two) {
        one.Color = two.Color;
        one.Symbol = two.Symbol;
        return one;
    }

    public static ITabletCell SetState(this ITabletCell cell, TabletSymbol symbol, TabletColor color) {
        cell.Symbol = symbol;
        cell.Color = color;
        return cell;
    }
    
    public static ITablet SetState(this ITablet tablet, ITablet other) {
        tablet.TopLeft.SetState(other.TopLeft);
        tablet.TopRight.SetState(other.TopRight);
        tablet.BottomLeft.SetState(other.BottomLeft);
        tablet.BottomRight.SetState(other.BottomRight);
        return tablet;
    }

    public static bool TabletEquals(this ITablet one, ITablet two) {
        return one.TopLeft.TabletEquals(two.TopLeft) 
            && one.TopRight.TabletEquals(two.TopRight) 
            && one.BottomLeft.TabletEquals(two.BottomLeft)
            && one.BottomRight.TabletEquals(two.BottomRight);
    }

    public static ITablet RotateCounterclockwise(this ITablet tablet) {
        ITabletCell temp = new RawTabletCell().SetState(tablet.TopLeft);
        tablet.TopLeft.SetState(tablet.TopRight);
        tablet.TopRight.SetState(tablet.BottomRight);
        tablet.BottomRight.SetState(tablet.BottomLeft);
        tablet.BottomLeft.SetState(temp);
        return tablet;
    }
}
