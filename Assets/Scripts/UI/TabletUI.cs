using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup))]
public class TabletUI : MonoBehaviour, ITablet
{
    public ITabletCell TopLeft { get { return topLeft; } set { topLeft.SetState(value); } }
    public ITabletCell TopRight { get { return topRight; } set { topRight.SetState(value); } }
    public ITabletCell BottomLeft { get { return bottomLeft; } set { bottomLeft.SetState(value); } }
    public ITabletCell BottomRight { get { return bottomRight; } set { bottomRight.SetState(value); } }

    private TabletCellUI topLeft;
    private TabletCellUI topRight;
    private TabletCellUI bottomLeft;
    private TabletCellUI bottomRight;

    void Start() {
        GetComponent<GridLayoutGroup>().startCorner = GridLayoutGroup.Corner.UpperLeft;
        GetComponent<GridLayoutGroup>().startAxis = GridLayoutGroup.Axis.Horizontal;

        TabletCellUI[] cells = GetComponentsInChildren<TabletCellUI>();
        if (cells.Length != 4)
            throw new Exception("Expected 4 TabletCellUI components in children, but found " + cells.Length);
        topLeft = cells[0];
        topRight = cells[1];
        bottomLeft = cells[2];
        bottomRight = cells[3];
    }
}
