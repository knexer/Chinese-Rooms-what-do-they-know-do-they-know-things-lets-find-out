using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class SimRunningUIMask : MonoBehaviour {
	
	void Start() {
        TickController.ModeChangedEvent += UpdateVisibility;
	}

    void OnDestroy() {
        TickController.ModeChangedEvent -= UpdateVisibility;
    }

    private void UpdateVisibility(TickController.TimeState Mode) {
        GetComponent<Image>().enabled = Mode != TickController.TimeState.Stopped;
    }
}
