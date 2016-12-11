using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Level : MonoBehaviour {

    public static Level Obj { get; private set; }

    public MachineSource[] availableMachines;

    void Awake() {
        if (Obj != null) {
            Debug.LogError("Multiple Levels!  Destroying second");
            Destroy(this);
            return;
        }

        Obj = this;
    }

    public abstract Tablet Evaluate(Tablet input);
}
