using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class LevelStateManager {

    public delegate void LevelCompletedHandler(bool wasSuccessful);
    public static event LevelCompletedHandler LevelCompletedEvent;

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

    /// <summary>
    /// Triggers a level completed event.  Returns whether the level was completed successfully.
    /// </summary>
    /// <returns></returns>
    public static bool LevelCompleted() {
        TickController.Obj.Pause();
        bool isSuccess = 
            LevelEndMachine.Obj.GridVertex.Equals(MachineGrid.Obj.GetTabletCenter())
            && Level.Obj.Evaluate(InputTablet).TabletEquals(MachineGrid.Obj.GridTablet);
        if (LevelCompletedEvent != null)
            LevelCompletedEvent(isSuccess);
        return isSuccess;
    }
}
