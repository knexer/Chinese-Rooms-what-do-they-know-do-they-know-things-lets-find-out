using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class VertexMachine : MonoBehaviour
{
    public GridVertex GridVertex { get; set; }

    // Use this for initialization
    protected void Start()
    {
        TickController.ManipulateTickEvent += Manipulate;
    }

    protected abstract void Manipulate(float tickTime);
}
