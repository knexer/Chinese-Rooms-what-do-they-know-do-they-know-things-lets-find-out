using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TabletCellUI : MonoBehaviour, IPointerClickHandler {

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

    private TabletCell.Symbols symbol;
    private TabletCell.Colors color;

    // Use this for initialization
    void Start () {
        symbol = TabletCell.Symbols.Dream;
        color = TabletCell.Colors.None;
        UpdateImages();
    }

    private void UpdateImages() {
        switch (symbol) {
            case TabletCell.Symbols.Dream:
                symbolBaseTarget.sprite = dreamSymbolBase;
                symbolFillTarget.sprite = dreamSymbolFill;
                break;
            case TabletCell.Symbols.Eye:
                symbolBaseTarget.sprite = eyeSymbolBase;
                symbolFillTarget.sprite = eyeSymbolFill;
                break;
            case TabletCell.Symbols.Snake:
                symbolBaseTarget.sprite = snakeSymbolBase;
                symbolFillTarget.sprite = snakeSymbolFill;
                break;
            default:
                throw new Exception("Unexpected enum value " + symbol);
        }

        switch (color) {
            case TabletCell.Colors.None:
                symbolFillTarget.gameObject.SetActive(false);
                break;
            case TabletCell.Colors.Green:
                symbolFillTarget.gameObject.SetActive(true);
                symbolFillTarget.color = green;
                break;
            case TabletCell.Colors.Blue:
                symbolFillTarget.gameObject.SetActive(true);
                symbolFillTarget.color = blue;
                break;
            default:
                throw new Exception("Unexpected enum value " + color);
        }
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (TabletCellModeToggle.CurrentCellMode == TabletCellModeToggle.CellMode.Color) {
            switch(color) {
                case TabletCell.Colors.None:
                    color = TabletCell.Colors.Green;
                    break;
                case TabletCell.Colors.Green:
                    color = TabletCell.Colors.Blue;
                    break;
                case TabletCell.Colors.Blue:
                    color = TabletCell.Colors.None;
                    break;
                default:
                    throw new Exception("Unexpected enum value " + color);
            }
        } else if (TabletCellModeToggle.CurrentCellMode == TabletCellModeToggle.CellMode.Symbol) {
            switch (symbol) {
                case TabletCell.Symbols.Dream:
                    symbol = TabletCell.Symbols.Eye;
                    break;
                case TabletCell.Symbols.Eye:
                    symbol = TabletCell.Symbols.Snake;
                    break;
                case TabletCell.Symbols.Snake:
                    symbol = TabletCell.Symbols.Dream;
                    break;
                default:
                    throw new Exception("Unexpected enum value " + symbol);
            }
        }

        UpdateImages();
    }
}
