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

    public static event Action TestFailed;

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
            GlobalInput.InputTablet = input;

            TickController.Obj.Mode = TickController.TimeState.MaximumWarp;

            HashSet<SimState> previousStates = new HashSet<SimState>();

			RunCompleted = false;
			while (RunCompleted == false)
            {
                yield return null;

                if (RunCompleted == false)
                {
                    SimState currentState = new SimState
                    {
                        Tablet = new RawTablet().SetState(MachineGrid.Obj.Input),
                        GridX = MachineGrid.Obj.Input.GridVertexX,
                        GridY = MachineGrid.Obj.Input.GridVertexY
                    };

                    if (previousStates.Contains(currentState))
                    {
                        Debug.Log("Cycle detected.");
                        if (TestFailed != null) TestFailed();
                        yield break;
                    }
                    else
                    {
                        previousStates.Add(currentState);
                    }
                }
			}
			RunCompleted = false;

            if (!MachineGrid.Obj.Input.TabletEquals(Level.Obj.Evaluate(input))) {
                Debug.Log("Output doesn't equal expected output.");
                if (TestFailed != null) TestFailed();
                yield break;
            }
		}

        LevelManager.Obj.LoadNextLevel();

		yield return null;
	}

    public class SimState
    {
        public ITablet Tablet;
        public int GridX;
        public int GridY;

        public override bool Equals(System.Object other)
        {
            SimState otherState = other as SimState;
            if (otherState == null) return false;

            return otherState.Tablet.Equals(Tablet)
                && otherState.GridX.Equals(GridX)
                && otherState.GridY.Equals(GridY);
        }

        public override int GetHashCode()
        {
            return Tablet.GetHashCode() * 17
                + GridX.GetHashCode() * 17 * 17
                + GridY.GetHashCode() * 17 * 17 * 17;
        }
    }
}
