using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class VertexMachine : MonoBehaviour
{
    public GridVertex GridVertex { get; set; }

    // Use this for initialization
	protected abstract void Start();
    protected abstract void OnDestroy();

    protected abstract void Manipulate(float tickTime);

	public abstract void OnPlace ();

	public abstract void OnRemove ();

}
