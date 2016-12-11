using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridVertex : MonoBehaviour {
    private MachineGrid Grid;
    private int X;
    private int Y;

    public VertexMachine StartingVertexMachine;

    public VertexMachine VertexMachine
    {
        get { return vertexMachine; }
        set { vertexMachine = value; if (vertexMachine != null) vertexMachine.GridVertex = this; }
    }
    public VertexMachine vertexMachine;

	// Use this for initialization
	void Start () {
        this.VertexMachine = StartingVertexMachine;
	}

    public void Register(int X, int Y, MachineGrid Grid)
    {
        this.X = X;
        this.Y = Y;
        this.Grid = Grid;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
