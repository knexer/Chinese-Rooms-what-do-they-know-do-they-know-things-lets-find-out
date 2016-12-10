using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell : MonoBehaviour {
    private MachineGrid Grid;
    private int X;
    private int Y;

    public GameObject CellMachine;

	// Use this for initialization
	void Start () {

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
