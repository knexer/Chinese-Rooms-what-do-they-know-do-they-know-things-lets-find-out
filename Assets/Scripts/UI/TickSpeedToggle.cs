using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TickSpeedToggle : MonoBehaviour {

    public TickController.TimeState Mode;
    
	void Start () {
        GetComponent<Toggle>().onValueChanged.AddListener((bool isOn) => {
            if (isOn)
                TickController.Obj.Mode = Mode;
        });
        TickController.ModeChangedEvent += UpdateToggle;
	}

    void OnDestroy() {
        TickController.ModeChangedEvent -= UpdateToggle;
    }

    void UpdateToggle(TickController.TimeState newMode) {
        GetComponent<Toggle>().isOn = newMode == Mode;
    }
}
