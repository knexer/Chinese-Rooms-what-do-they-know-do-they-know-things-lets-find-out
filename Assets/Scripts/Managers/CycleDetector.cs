using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class CycleDetector : MonoBehaviour {

    private static HashSet<SimState> ObservedStates = new HashSet<SimState>();
    private SimState LastObservedState;

    void Start() {
        LastObservedState = new SimState();
        LevelStateManager.LevelCompletedEvent += LevelCompleted;
        TickController.ModeChangedEvent += ModeChanged;
    }

    void OnDestroy() {
        LevelStateManager.LevelCompletedEvent -= LevelCompleted;
        TickController.ModeChangedEvent -= ModeChanged;
    }

    void Update() {
        if (TickController.Obj.IsRunning()) {
            SimState currentState = new SimState();
            if (!currentState.Equals(LastObservedState)) {
                ObservedStates.Add(LastObservedState);
                LastObservedState = currentState;

                if (ObservedStates.Contains(LastObservedState)) {
                    Debug.Log("Cycle detected.");
                    Clear();
                    LevelStateManager.LevelCompleted();
                }
            }
        }
    }

    private void ModeChanged(TickController.TimeState mode) {
        if (mode == TickController.TimeState.Stopped)
            Clear();
    }

    private void LevelCompleted(bool success) {
        Clear();
    }

    private void Clear() {
        ObservedStates.Clear();
        LastObservedState = new SimState();
    }

    private class SimState {
        public ITablet Tablet { get; }
        public int GridX { get; }
        public int GridY { get; }
        public Mover.Direction MoveDirection { get; }

        public SimState() {
            Tablet = new RawTablet().SetState(MachineGrid.Obj.GridTablet);
            GridX = MachineGrid.Obj.GridTablet.GridVertexX;
            GridY = MachineGrid.Obj.GridTablet.GridVertexY;
            MoveDirection = MachineGrid.Obj.GridTablet.MovementDirection;
        }

        public override bool Equals(System.Object other) {
            SimState otherState = other as SimState;
            if (otherState == null) return false;

            return otherState.Tablet.Equals(Tablet)
                && otherState.GridX.Equals(GridX)
                && otherState.GridY.Equals(GridY)
                && otherState.MoveDirection.Equals(MoveDirection);
        }

        public override int GetHashCode() {
            return Tablet.GetHashCode() * 17
                + GridX.GetHashCode() * 17 * 17
                + GridY.GetHashCode() * 17 * 17 * 17
                + MoveDirection.GetHashCode() * 17 * 17 * 17 * 17;
        }
    }
}
