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
    
    public CellMachine StartingCellMachine;

    public CellMachine CellMachine {
		get { return cellMachine; }
		set { cellMachine = value; if (cellMachine != null) cellMachine.GridCell = this; }
	}
    public CellMachine cellMachine;

    // Use this for initialization
    void Start () {
        this.CellMachine = this.StartingCellMachine;
    }

    public void Register(int X, int Y, MachineGrid Grid)
    {
        this.X = X;
        this.Y = Y;
        this.Grid = Grid;
    }
    
    public TabletCell GetInput() {
        return Grid.GetInputAt(X, Y);
    }
}
