using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(TabletCellUI))]
public class InputTabletCellUI : MonoBehaviour, IPointerClickHandler {

    private TabletColor color {
        get { return GetComponent<TabletCellUI>().Color; }
        set { GetComponent<TabletCellUI>().Color = value; }
    }
    private TabletSymbol symbol {
        get { return GetComponent<TabletCellUI>().Symbol; }
        set { GetComponent<TabletCellUI>().Symbol = value; }
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (TabletCellModeToggle.CurrentCellMode == TabletCellModeToggle.CellMode.Color) {
            switch (color) {
                case TabletColor.None:
                    color = TabletColor.Green;
                    break;
                case TabletColor.Green:
                    color = TabletColor.Blue;
                    break;
                case TabletColor.Blue:
                    color = TabletColor.None;
                    break;
                default:
                    throw new Exception("Unexpected enum value " + color);
            }
        } else if (TabletCellModeToggle.CurrentCellMode == TabletCellModeToggle.CellMode.Symbol) {
            switch (symbol) {
                case TabletSymbol.Dream:
                    symbol = TabletSymbol.Eye;
                    break;
                case TabletSymbol.Eye:
                    symbol = TabletSymbol.Snake;
                    break;
                case TabletSymbol.Snake:
                    symbol = TabletSymbol.Dream;
                    break;
                default:
                    throw new Exception("Unexpected enum value " + symbol);
            }
        }
        
        GlobalInput.InputTablet = GetComponent<TabletCellUI>().parent;
    }
}
