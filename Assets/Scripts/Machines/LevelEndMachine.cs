using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Does not actually end the level.  Sends the Tablet back to the beginning instead :|
 */
public class LevelEndMachine : VertexMachine {

    public static LevelEndMachine Obj { get; private set; }

    public override void OnPlace() { }
    public override void OnRemove() { }

    protected override void Manipulate(float tickTime)
    {
        if (this.GridVertex.X == MachineGrid.Obj.GridTablet.GridVertexX
            && this.GridVertex.Y == MachineGrid.Obj.GridTablet.GridVertexY)
            LevelStateManager.LevelCompleted();
    }

    protected override void Start()
    {
        base.Start();

        if (Obj != null)
            Debug.LogError("Multiple LevelEndMachines in scene!");
        Obj = this;

        GridVertex upperRightVertex = MachineGrid.Obj.GridVertices[
            MachineGrid.Obj.GridVertices.GetLength(0) - 2,
            MachineGrid.Obj.GridVertices.GetLength(1) - 2].GetComponent<GridVertex>();
        upperRightVertex.VertexMachine = this;
        Vector3 position = upperRightVertex.transform.position;
        position.z = 0.0f;
        transform.position = position;
    }
}