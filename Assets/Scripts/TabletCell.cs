using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabletCell : MonoBehaviour
{
    public tk2dSpriteAnimator tabletAnimator;

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

    private Color color;
    private Symbols symbol;

    public Colors Color;
    public Symbols Symbol
    {
        get { return symbol; }
        set { SetSymbol(value); symbol = value; }
    }

    // Use this for initialization
    void Start()
    {
        SetSymbol(Symbol);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SetSymbol(Symbols symbol)
    {
        switch (symbol)
        {
            case Symbols.Eye:
                tabletAnimator.SetFrame(0);
                break;

            case Symbols.Snake:
                tabletAnimator.SetFrame(1);
                break;

            case Symbols.Dream:
                tabletAnimator.SetFrame(2);
                break;
        }
    }
}
