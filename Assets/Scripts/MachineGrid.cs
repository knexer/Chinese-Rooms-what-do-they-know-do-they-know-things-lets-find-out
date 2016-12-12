using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGrid : MonoBehaviour {
    public int Width;
    public int Height;
    public GameObject GridCellPrefab;
    public GameObject GridVertexPrefab;
    public GameObject EndVertexPrefab;
    public GameObject OutOfBoundsVertexPrefab;

    public Transform GridContainer;  
    public Tablet CurrentInput;

    [HideInInspector]
    public GameObject[,] GridCells;
    [HideInInspector]
    public GameObject[,] GridVertices;

    private void Awake()
    {
        GridCells = new GameObject[Width, Height];
        GridVertices = new GameObject[Width + 1, Height + 1];

        Vector3 cellSize = GridCellPrefab.GetComponent<BoxCollider2D>().size;

        for (int x = 0; x < GridCells.GetLength(0); x++)
        {
            for (int y = 0; y < GridCells.GetLength(1); y++)
            {
                GridCells[x, y] = Instantiate(GridCellPrefab);
                GridCells[x, y].GetComponent<GridCell>().Register(x, y, this);

                GridCells[x, y].transform.parent = GridContainer;
                GridCells[x, y].transform.localPosition = cellSize / 2 + new Vector3(x * cellSize.x, y * cellSize.y);
                GridCells[x, y].transform.localScale = Vector3.one;
            }
        }

        for (int x = 0; x < GridVertices.GetLength(0); x++)
        {
            for (int y = 0; y < GridVertices.GetLength(1); y++)
            {
                GridVertices[x, y] = Instantiate(GridVertexPrefab);
                GridVertices[x, y].GetComponent<GridVertex>().Register(x, y, this);

                GridVertices[x, y].transform.parent = GridContainer;
                GridVertices[x, y].transform.localPosition = new Vector3(x * cellSize.x, y * cellSize.y);
                GridVertices[x, y].transform.localScale = Vector3.one;
            }
        }

        GameObject EndVertexMachine = Instantiate(EndVertexPrefab);
        GridVertex upperRightVertex = GridVertices[GridVertices.GetLength(0) - 2, GridVertices.GetLength(1) - 2].GetComponent<GridVertex>();
        upperRightVertex.VertexMachine = EndVertexMachine.GetComponent<VertexMachine>();
        EndVertexPrefab.transform.position = upperRightVertex.transform.position;


        for (int x = 0; x < GridVertices.GetLength(0); x++)
        {
            for (int y = 0; y < GridVertices.GetLength(1); y++)
            {
                if (x == 0 || x == GridVertices.GetLength(0)-1 || y == 0 || y == GridVertices.GetLength(1)-1)
                {
                    GameObject OutOfBoundsVertexMachine = Instantiate(OutOfBoundsVertexPrefab);
                    GridVertex edge = GridVertices[x, y].GetComponent<GridVertex>();
                    edge.VertexMachine = OutOfBoundsVertexMachine.GetComponent<VertexMachine>();
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

    public TabletCell GetInputAt(int x, int y) {
        return CurrentInput.GetTabletPieceByFactoryPosition(x, y);
    }

	public GridVertex GetTabletCenter() {
		return GridVertices [CurrentInput.gridVertexX, CurrentInput.gridVertexY].GetComponent<GridVertex>();
	}

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
