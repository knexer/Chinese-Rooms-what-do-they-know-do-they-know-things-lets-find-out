using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TabletUI))]
public class OutputTabletUI : MonoBehaviour {
    
	void Start() {
        UpdateOutput(LevelStateManager.InputTablet);
        LevelStateManager.InputChanged += UpdateOutput;
	}
	
    void OnDestroy() {
        LevelStateManager.InputChanged -= UpdateOutput;
    }

	void UpdateOutput(ITablet Input) {
        GetComponent<TabletUI>().SetState(Level.Obj.Evaluate(Input));
    }
}
