using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridVertex : MonoBehaviour {
    [HideInInspector]
    public MachineGrid Grid { get; private set; }
    [HideInInspector]
    public int X { get; private set; }
    [HideInInspector]
    public int Y { get; private set; }

    public VertexMachine StartingVertexMachine;

    public VertexMachine VertexMachine
    {
        get { return vertexMachine; }
        set { vertexMachine = value; if (vertexMachine != null) vertexMachine.GridVertex = this; }
    }
    private VertexMachine vertexMachine;

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

	public GridCell[] GetSurroundingCells() {
		GridCell[] cells = new GridCell[4];
		cells [0] = Grid.GridCells [X - 1, Y].GetComponent<GridCell>(); // Top left.
		cells [1] = Grid.GridCells [X, Y].GetComponent<GridCell>(); // Top right.
		cells [2] = Grid.GridCells [X - 1, Y - 1].GetComponent<GridCell>(); // Bottom left.
		cells [3] = Grid.GridCells [X, Y - 1].GetComponent<GridCell>(); // Bottom right.
		return cells;
	}
}
