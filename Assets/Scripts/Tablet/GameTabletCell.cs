using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTabletCell : MonoBehaviour, ITabletCell
{
    public tk2dSpriteAnimator tabletSymbol;
    public tk2dSpriteAnimator tabletGlow;

    private TabletSymbol symbol;
    private TabletColor color;

    public TabletColor Color
    {
        get { return color; }
        set { SetGlow(value); color = value; }
    }

    public TabletSymbol Symbol
    {
        get { return symbol; }
        set { SetSymbol(value); symbol = value; }
    }

    // Use this for initialization
    void Start()
    {        
        SetSymbol(Symbol);
        SetGlow(TabletColor.None);
    }

    // Update is called once per frame
    void Update()
    {      
      SetGlow(Color);
    }

    private void SetSymbol(TabletSymbol symbol)
    {
        switch (symbol)
        {
            case TabletSymbol.Eye:
                tabletSymbol.gameObject.SetActive(true);
                tabletSymbol.SetFrame(0);
                tabletGlow.SetFrame(0);
                break;

            case TabletSymbol.Snake:
                tabletSymbol.gameObject.SetActive(true);
                tabletSymbol.SetFrame(1);
                tabletGlow.SetFrame(1);
                break;

            case TabletSymbol.Dream:
                tabletSymbol.gameObject.SetActive(true);
                tabletSymbol.SetFrame(2);
                tabletGlow.SetFrame(2);
                break;
        }
    }

    private void SetGlow (TabletColor glow) {
        switch (glow) 
        {
          case TabletColor.Blue:
              tabletGlow.gameObject.SetActive(true);
              tabletGlow.Sprite.color = Globals.Instance.BlueGlow;
              break;

          case TabletColor.Green:
              tabletGlow.gameObject.SetActive(true);
              tabletGlow.Sprite.color = Globals.Instance.GreenGlow;
              break;

          case TabletColor.None:
              tabletGlow.gameObject.SetActive(false);
              break;
        }
    }

    public bool Equals(GameTabletCell other)
    {
        return other.Color == Color && other.Symbol == Symbol;
    }
}
