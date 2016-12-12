using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabletCell : MonoBehaviour
{
    public tk2dSpriteAnimator tabletSymbol;
    public tk2dSpriteAnimator tabletGlow;

    public enum Colors
    {
        None,
        Green,
        Blue
    }

    public enum Symbols
    {
        Eye,
        Snake,
        Dream
    }

    private Symbols symbol;
    private Colors color;


    public Colors Color
    {
        get { return color; }
        set { SetGlow(value); color = value; }
    }

    public Symbols Symbol
    {
        get { return symbol; }
        set { SetSymbol(value); symbol = value; }
    }

    // Use this for initialization
    void Start()
    {        
        SetSymbol(Symbol);
        SetGlow(Colors.None);
    }

    // Update is called once per frame
    void Update()
    {      
      SetGlow(Color);
    }

    private void SetSymbol(Symbols symbol)
    {
        switch (symbol)
        {
            case Symbols.Eye:
                tabletSymbol.gameObject.SetActive(true);
                tabletSymbol.SetFrame(0);
                tabletGlow.SetFrame(0);
                break;

            case Symbols.Snake:
                tabletSymbol.gameObject.SetActive(true);
                tabletSymbol.SetFrame(1);
                tabletGlow.SetFrame(1);
                break;

            case Symbols.Dream:
                tabletSymbol.gameObject.SetActive(true);
                tabletSymbol.SetFrame(2);
                tabletGlow.SetFrame(2);
                break;
        }
    }

    private void SetGlow (Colors glow) {
        switch (glow) 
        {
          case Colors.Blue:
              tabletGlow.gameObject.SetActive(true);
              tabletGlow.Sprite.color = UnityEngine.Color.blue;
              break;

          case Colors.Green:
              tabletGlow.gameObject.SetActive(true);
              tabletGlow.Sprite.color = UnityEngine.Color.green;
              break;

          case Colors.None:
              tabletGlow.gameObject.SetActive(false);
              break;
        }
    }

    public bool Equals(TabletCell other)
    {
        return other.Color == Color && other.Symbol == Symbol;
    }
}
