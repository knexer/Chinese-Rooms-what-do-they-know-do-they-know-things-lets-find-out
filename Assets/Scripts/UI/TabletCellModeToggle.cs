using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class TabletCellModeToggle : MonoBehaviour {

    public enum CellMode {
        Color, Symbol
    }

    public static CellMode CurrentCellMode;

    public CellMode ToggleCellMode;

	// Use this for initialization
	void Start () {
        UpdateState(GetComponent<Toggle>().isOn);
        GetComponent<Toggle>().onValueChanged.AddListener(UpdateState);
	}
	
	void UpdateState(bool isOn) {
        if (isOn)
            CurrentCellMode = ToggleCellMode;
    }
}
