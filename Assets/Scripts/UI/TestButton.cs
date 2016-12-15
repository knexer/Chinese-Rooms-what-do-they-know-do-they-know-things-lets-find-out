using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class TestButton : MonoBehaviour {
    public const int NRuns = 20;

	public static bool RunCompleted = false;

	// Use this for initialization
	void Start () {
		GetComponent<Button> ().onClick.AddListener (StartTests);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void StartTests() {
		StartCoroutine (WaitEnumerator ());
	}

	private IEnumerator WaitEnumerator() {
		Debug.Log("We are now starting tests.");

		for (int i = 0; i < NRuns; i++) {
			TickController.Obj.Pause();
			TickController.Obj.ResetTablets();

			GameTabletCell[] tablets = MachineGrid.Obj.Input.GetAllCells();
			for (int j = 0; j < 4; j++) {
				tablets[j].Color = (TabletColor)UnityEngine.Random.Range(0, Enum.GetValues(typeof(TabletColor)).Length);
				tablets[j].Symbol = (TabletSymbol)UnityEngine.Random.Range(0, Enum.GetValues(typeof(TabletSymbol)).Length);
			}
            ITablet input = new RawTablet().SetState(MachineGrid.Obj.Input);

            TickController.Obj.Mode = TickController.TimeState.MaximumWarp;

			RunCompleted = false;
			while (RunCompleted == false) {
				yield return null;
			}
			RunCompleted = false;

            if (!MachineGrid.Obj.Input.TabletEquals(Level.Obj.Evaluate(input))) {
                Debug.Log("Input failed tests!");
                yield break;
            }
		}

        LevelManager.Obj.LoadNextLevel();

		yield return null;
	}
}
