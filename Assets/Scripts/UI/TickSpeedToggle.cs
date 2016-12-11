using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TickSpeedToggle : MonoBehaviour {

    public int speed;
    
	void Start () {
        GetComponent<Toggle>().onValueChanged.AddListener((bool isOn) => {
            if (isOn)
                TickController.Obj.SetSpeed(speed);
        });
	}
}
