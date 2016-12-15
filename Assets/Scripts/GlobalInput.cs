using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class GlobalInput {

    public delegate void GlobalInputChangeHandler(ITablet inputState);
    public static event GlobalInputChangeHandler InputChanged;

    public static ITablet InputTablet {
        get { return inputTablet; }
        set {
            inputTablet = value;
            if (InputChanged != null)
                InputChanged(new RawTablet().SetState(inputTablet));
        }
    }
    private static ITablet inputTablet = new RawTablet();
}
