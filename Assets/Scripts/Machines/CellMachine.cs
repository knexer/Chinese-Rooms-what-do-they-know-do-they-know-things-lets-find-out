using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CellMachine : MonoBehaviour {
	private MachineGrid grid;
	private int x;
	private int y;
    
	void Start () {
        TickController.ManipulateTickEvent += Manipulate;
	}

    void OnDestroy() {
        TickController.ManipulateTickEvent -= Manipulate;
    }

	public void Register(int x, int y, MachineGrid grid)
	{
		this.x = x;
		this.y = y;
		this.grid = grid;
	}

    protected abstract void Manipulate(float tickDelta);

	protected TabletCell GetTabletCell() {
		return grid.CurrentInput.GetTabletPieceByFactoryPosition (x, y);
	}
}
