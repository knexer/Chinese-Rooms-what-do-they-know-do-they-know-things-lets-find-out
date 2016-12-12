using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TickResetToggle : MonoBehaviour {
    
    void Start() {
        GetComponent<Toggle>().onValueChanged.AddListener((bool isOn) => {
            if (isOn) {
                TickController.Obj.SetSpeed(0);
                TickController.Obj.ResetTablets();
            }
        });
    }
}
