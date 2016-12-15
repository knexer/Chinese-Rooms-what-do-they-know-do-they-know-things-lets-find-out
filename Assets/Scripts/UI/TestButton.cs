using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TestButton : MonoBehaviour {
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
		Debug.Log ("We are now starting tests.");
		//Level.Obj.

		for (int i = 0; i < 10; i++) {
			TickController.Obj.Pause();
			TickController.Obj.ResetTablets ();

			GameTabletCell[] tablets = FindObjectOfType<MachineGrid> ().CurrentInput.GetAllPieces ();
			for (int j = 0; j < 4; j++) {
				tablets [j].Color = (TabletColor)Random.Range (0, 3);
				tablets [j].Symbol = (TabletSymbol)Random.Range (0, 3);
			}
            TickController.Obj.Mode = TickController.TimeState.FastForward;

			RunCompleted = false;
			while (RunCompleted == false) {
				yield return null;
			}
			RunCompleted = false;

		}
		yield return null;
	}
}
