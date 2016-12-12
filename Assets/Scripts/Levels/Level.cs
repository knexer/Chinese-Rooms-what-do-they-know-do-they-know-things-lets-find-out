using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Level : MonoBehaviour {

    public static Level Obj { get; private set; }

    public MachineSource[] availableMachines;
    public Tablet[] testCases;

    void Awake() {
        if (Obj != null) {
            Debug.Log("Multiple Levels!  Destroying first");
            Destroy(Obj);
        }

        Obj = this;
    }

    public abstract Tablet Evaluate(Tablet input);
}
