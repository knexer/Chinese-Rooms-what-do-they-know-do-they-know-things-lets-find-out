using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class VertexMachine : MonoBehaviour
{
    public GridVertex GridVertex { get; set; }
    
	protected virtual void Start() {
        TickController.ManipulateTickEvent += Manipulate;
    }

    protected virtual void OnDestroy() {
        TickController.ManipulateTickEvent -= Manipulate;
    }

    protected abstract void Manipulate(float tickTime);

	public abstract void OnPlace ();

	public abstract void OnRemove ();

}
