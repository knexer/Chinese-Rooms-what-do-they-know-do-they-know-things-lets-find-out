using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell : MonoBehaviour {
    private MachineGrid Grid;
    private int X;
    private int Y;
    
    public CellMachine StartingCellMachine;

    public CellMachine CellMachine {
        get { return cellMachine; }
        set { cellMachine = value; cellMachine.GridCell = this; } }
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
    
}
