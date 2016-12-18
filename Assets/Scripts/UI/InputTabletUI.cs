using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TabletUI))]
public class InputTabletUI : MonoBehaviour {

    void Start() {
        UpdateInput(LevelStateManager.InputTablet);
        LevelStateManager.InputChanged += UpdateInput;
    }

    void OnDestroy() {
        LevelStateManager.InputChanged -= UpdateInput;
    }

    void UpdateInput(ITablet Input) {
        GetComponent<TabletUI>().SetState(Input);
    }
}
