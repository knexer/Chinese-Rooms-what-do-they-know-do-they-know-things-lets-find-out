using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class TestButton : MonoBehaviour {
    public const int NRuns = 20;

    private int remainingTests = 0;

	// Use this for initialization
	void Start () {
		GetComponent<Button> ().onClick.AddListener (StartTests);
    }

	public void StartTests() {
        remainingTests = NRuns;
        TickController.ModeChangedEvent += StopTestsOnStop;
        LevelStateManager.LevelCompletedEvent += CompleteTest;
        RunTest();
	}

    private void CompleteTest(bool success) {
        if (success) {
            remainingTests--;
            if (remainingTests <= 0)
                LevelManager.Obj.LoadNextLevel();
            else
                RunTest();
        } else {
            ResetTests();
        }
    }

    private void RunTest() {
        TickController.ModeChangedEvent -= StopTestsOnStop;
        TickController.Obj.Stop(); // Don't want our own stop call to reset the tests
        TickController.ModeChangedEvent += StopTestsOnStop;

        GameTabletCell[] tablets = MachineGrid.Obj.GridTablet.GetAllCells();
        for (int j = 0; j < 4; j++) {
            tablets[j].Color = (TabletColor)UnityEngine.Random.Range(0, Enum.GetValues(typeof(TabletColor)).Length);
            tablets[j].Symbol = (TabletSymbol)UnityEngine.Random.Range(0, Enum.GetValues(typeof(TabletSymbol)).Length);
        }
        ITablet input = new RawTablet().SetState(MachineGrid.Obj.GridTablet);
        LevelStateManager.InputTablet = input;
        
        TickController.Obj.Mode = TickController.TimeState.MaximumWarp;
    }

    private void StopTestsOnStop(TickController.TimeState mode) {
        if (!TickController.Obj.IsRunning())
            ResetTests();
    }

    private void ResetTests() {
        remainingTests = 0;
        LevelStateManager.LevelCompletedEvent -= CompleteTest;
        TickController.ModeChangedEvent -= StopTestsOnStop;
    }
}
