using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinMachine : VertexMachine {

    // Use this for initialization
    protected override void Start () {
		TickController.ManipulateTickEvent += Manipulate;
	}

    protected override void OnDestroy()
    {
        TickController.ManipulateTickEvent -= Manipulate;
    }

    // Update is called once per frame
    void Update () {

    }

    protected override void Manipulate(float tickTime)
    {
        // no-op
    }

	public override void OnPlace() {
	}

	public override void OnRemove() {}
}
