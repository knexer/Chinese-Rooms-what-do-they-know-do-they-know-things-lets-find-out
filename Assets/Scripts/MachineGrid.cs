﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGrid : MonoBehaviour {

    public static MachineGrid Obj { get; private set; }

    public int Width;
    public int Height;
    public GridCell GridCellPrefab;
    public GridVertex GridVertexPrefab;
    public VertexMachine OutOfBoundsVertexPrefab;

    public Transform GridContainer;
    public GameTabletMover GridTablet;

    public Vector2 CellSize { get; private set; }

    [HideInInspector]
    public GridCell[,] GridCells;
    [HideInInspector]
    public GridVertex[,] GridVertices;

    private void Awake()
    {
        if (Obj != null)
            Debug.LogError("Multiple MachineGrids in scene!");
        Obj = this;

        if (GridTablet == null)
            GridTablet = FindObjectOfType<GameTabletMover>();

        GridCells = new GridCell[Width, Height];
        GridVertices = new GridVertex[Width + 1, Height + 1];

        Vector3 cellSize = GridCellPrefab.GetComponent<BoxCollider2D>().size;
        this.CellSize = new Vector2(cellSize.x, cellSize.y);

        for (int x = 0; x < GridCells.GetLength(0); x++)
        {
            for (int y = 0; y < GridCells.GetLength(1); y++)
            {
                GridCells[x, y] = Instantiate(GridCellPrefab);
                GridCells[x, y].Register(x, y, this);

                GridCells[x, y].transform.parent = GridContainer;
                GridCells[x, y].transform.localPosition = cellSize / 2 + new Vector3(x * CellSize.x, y * CellSize.y);
                GridCells[x, y].transform.localScale = Vector3.one;
            }
        }

        for (int x = 0; x < GridVertices.GetLength(0); x++)
        {
            for (int y = 0; y < GridVertices.GetLength(1); y++)
            {
                GridVertices[x, y] = Instantiate(GridVertexPrefab);
                GridVertices[x, y].Register(x, y, this);

                GridVertices[x, y].transform.parent = GridContainer;
                GridVertices[x, y].transform.localPosition = new Vector3(x * cellSize.x, y * cellSize.y);
                GridVertices[x, y].transform.localScale = Vector3.one;
            }
        }

        for (int x = 0; x < GridVertices.GetLength(0); x++)
        {
            for (int y = 0; y < GridVertices.GetLength(1); y++)
            {
                if (x == 0 || x == GridVertices.GetLength(0)-1 || y == 0 || y == GridVertices.GetLength(1)-1)
                {
                    VertexMachine OutOfBoundsVertexMachine = Instantiate(OutOfBoundsVertexPrefab);
                    GridVertex edge = GridVertices[x, y];
                    edge.VertexMachine = OutOfBoundsVertexMachine;
                }
            }
        }
    }

    public Vector2 GetCellSizeWorldSpace()
    {
        Vector3 cellSize = GridCellPrefab.GetComponent<BoxCollider2D>().size;
        cellSize.Scale(transform.FindChild("ScaleContainer").transform.lossyScale);

        return cellSize;
    }

    public GridVertex getClosestVertex(Vector3 position)
    {
        Vector2 cellSize = GetCellSizeWorldSpace();

        Vector3 relativePosition = position - transform.position;
        int xPosition = (int)Mathf.Clamp(relativePosition.x / cellSize.x + 0.5f, 0, GridVertices.GetLength(0) - 1);
        int yPosition = (int) Mathf.Clamp(relativePosition.y / cellSize.y + 0.5f, 0, GridVertices.GetLength(1) - 1);

        return GridVertices[xPosition, yPosition].GetComponent<GridVertex>();
    }

    public GridCell getClosestCell(Vector3 position)
    {
        Vector2 cellSize = GetCellSizeWorldSpace();

        Vector3 relativePosition = position - transform.position;
        int xPosition = (int)Mathf.Clamp(relativePosition.x / cellSize.x, 0, GridCells.GetLength(0) - 1);
        int yPosition = (int)Mathf.Clamp(relativePosition.y / cellSize.y, 0, GridCells.GetLength(1) - 1);

        return GridCells[xPosition, yPosition].GetComponent<GridCell>();
    }

    public Vector2 getVertexWorldPosition(int x, int y)
    {
        Vector2 cellSize = GetCellSizeWorldSpace();

        return new Vector2(transform.position.x + x * cellSize.x, transform.position.y + y * cellSize.y);
    }

    public GameTabletCell GetInputAt(int x, int y) {
        return GridTablet.GetTabletPieceByFactoryPosition(x, y);
    }

	public GridVertex GetTabletCenter() {
		return GridVertices [GridTablet.GridVertexX, GridTablet.GridVertexY].GetComponent<GridVertex>();
	}
}
