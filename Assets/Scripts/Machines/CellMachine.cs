using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CellMachine : MonoBehaviour {

    public GridCell GridCell { get; set; }

	void Start () {
        TickController.ManipulateTickEvent += Manipulate;
	}

    void OnDestroy() {
        TickController.ManipulateTickEvent -= Manipulate;
    }

    public abstract void Manipulate(float tickDelta);

	public abstract void OnPlace ();

	public abstract void OnRemove ();
}
