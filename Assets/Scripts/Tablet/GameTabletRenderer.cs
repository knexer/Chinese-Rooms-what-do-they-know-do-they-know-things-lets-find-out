using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTabletRenderer : MonoBehaviour, ITablet {
    public float SpriteOffset;
    public GameTabletCell TabletCellPrefab;
    public Transform TabletCellContainer;

    public ITabletCell TopLeft { get { return topLeft; } set { topLeft.SetState(value); } }
    public ITabletCell TopRight { get { return topRight; } set { topRight.SetState(value); } }
    public ITabletCell BottomLeft { get { return bottomLeft; } set { bottomLeft.SetState(value); } }
    public ITabletCell BottomRight { get { return bottomRight; } set { bottomRight.SetState(value); } }

    public GameTabletCell topLeft { get; private set; }
    public GameTabletCell topRight { get; private set; }
    public GameTabletCell bottomLeft { get; private set; }
    public GameTabletCell bottomRight { get; private set; }
    
    void Awake() {
        topLeft = NewTablet(-1, 1);
        topRight = NewTablet(1, 1);
        bottomLeft = NewTablet(-1, -1);
        bottomRight = NewTablet(1, -1);
    }

    /** Create a new tablet cell at the relative position of x, y. */
    private GameTabletCell NewTablet(float x, float y, TabletColor color = TabletColor.None, TabletSymbol symbol = TabletSymbol.Eye) {
        GameTabletCell tablet = Instantiate(TabletCellPrefab, transform, true);

        tablet.SetState(symbol, color);

        tablet.transform.parent = TabletCellContainer;
        tablet.transform.localScale = Vector3.one;
        tablet.transform.localPosition = new Vector3(x * SpriteOffset, y * SpriteOffset, 0);
        
        return tablet;
    }

    public GameTabletCell[] GetAllCells() {
        GameTabletCell[] tablets = new GameTabletCell[4];
        tablets[0] = topLeft;
        tablets[1] = topRight;
        tablets[2] = bottomLeft;
        tablets[3] = bottomRight;
        return tablets;
    }
}
