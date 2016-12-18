using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell : MonoBehaviour {
    [HideInInspector]
    public MachineGrid Grid { get; private set; }
    [HideInInspector]
    public int X { get; private set; }
    [HideInInspector]
    public int Y { get; private set; }

    public CellMachine CellMachine {
		get { return cellMachine; }
		set { cellMachine = value; if (cellMachine != null) cellMachine.GridCell = this; }
	}
    public CellMachine cellMachine;

    // Use this for initialization
    void Start () {

    }

    public void Register(int X, int Y, MachineGrid Grid)
    {
        this.X = X;
        this.Y = Y;
        this.Grid = Grid;
    }
    
    public GameTabletCell GetInput() {
        return Grid.GetInputAt(X, Y);
    }

	public GridVertex GetTabletCenter() {
		return Grid.GetTabletCenter ();
	}

	public GridVertex[] GetSurroundingVertices() {
		GridVertex[] vertices = new GridVertex[4];
		vertices [0] = Grid.GridVertices [X, Y + 1].GetComponent<GridVertex>(); // Top left.
		vertices [1] = Grid.GridVertices [X + 1, Y + 1].GetComponent<GridVertex>(); // Top right.
		vertices [2] = Grid.GridVertices [X, Y].GetComponent<GridVertex>(); // Bottom left.
		vertices [3] = Grid.GridVertices [X + 1, Y].GetComponent<GridVertex>(); // Bottom right.
		return vertices;
    }

    public override bool Equals(object other) {
        if (other is GridVertex)
            return (other as GridVertex).X == this.X && (other as GridVertex).Y == this.Y;
        else
            return base.Equals(other);
    }

    public override int GetHashCode() {
        return X.GetHashCode() * 8 + Y.GetHashCode();
    }
}
