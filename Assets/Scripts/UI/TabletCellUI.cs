using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TabletCellUI : MonoBehaviour, IPointerClickHandler, ITabletCell {
    public Sprite eyeSymbolBase;
    public Sprite eyeSymbolFill;
    public Sprite dreamSymbolBase;
    public Sprite dreamSymbolFill;
    public Sprite snakeSymbolBase;
    public Sprite snakeSymbolFill;
    public Color green;
    public Color blue;

    public Image symbolBaseTarget;
    public Image symbolFillTarget;

    public bool isUserControlled = true;
    
    public TabletSymbol Symbol { get { return symbol; } set { symbol = value; UpdateImages(); } }
    public TabletColor Color { get { return color; } set { color = value; UpdateImages(); } }

    private TabletSymbol symbol;
    private TabletColor color;

    public TabletUI parent;

    // Use this for initialization
    void Awake() {
        symbol = TabletSymbol.Dream;
        color = TabletColor.None;
        UpdateImages();
    }

    private void UpdateImages() {
        switch (symbol) {
            case TabletSymbol.Dream:
                symbolBaseTarget.sprite = dreamSymbolBase;
                symbolFillTarget.sprite = dreamSymbolFill;
                break;
            case TabletSymbol.Eye:
                symbolBaseTarget.sprite = eyeSymbolBase;
                symbolFillTarget.sprite = eyeSymbolFill;
                break;
            case TabletSymbol.Snake:
                symbolBaseTarget.sprite = snakeSymbolBase;
                symbolFillTarget.sprite = snakeSymbolFill;
                break;
            default:
                throw new Exception("Unexpected enum value " + symbol);
        }

        switch (color) {
            case TabletColor.None:
                symbolFillTarget.gameObject.SetActive(false);
                break;
            case TabletColor.Green:
                symbolFillTarget.gameObject.SetActive(true);
                symbolFillTarget.color = green;
                break;
            case TabletColor.Blue:
                symbolFillTarget.gameObject.SetActive(true);
                symbolFillTarget.color = blue;
                break;
            default:
                throw new Exception("Unexpected enum value " + color);
        }
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (!isUserControlled)
            return;

        BlackBoxButton.Obj.HideOutput();

        if (TabletCellModeToggle.CurrentCellMode == TabletCellModeToggle.CellMode.Color) {
            switch(color) {
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

        UpdateImages();
        GlobalInput.InputTablet = parent;
    }
}
