using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TabletUI))]
public class OutputTabletUI : MonoBehaviour {
    
	void Start() {
        UpdateOutput(GlobalInput.InputTablet);
        GlobalInput.InputChanged += UpdateOutput;
	}
	
	void UpdateOutput(ITablet Input) {
        GetComponent<TabletUI>().SetState(Level.Obj.Evaluate(Input));
    }
}
