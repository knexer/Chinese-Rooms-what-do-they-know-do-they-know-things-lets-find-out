using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGrid : MonoBehaviour {
    public int Width;
    public int Height;
    public GameObject GridCellPrefab;
    public GameObject GridVertexPrefab;
    public Tablet CurrentInput;

    [HideInInspector]
    public GameObject[,] GridCells;
    [HideInInspector]
    public GameObject[,] GridVertices;

    private void Awake() {
        GridCells = new GameObject[Width, Height];
        GridVertices = new GameObject[Width + 1, Height + 1];

        Vector3 cellSize = GridCellPrefab.GetComponent<BoxCollider2D>().size;

        for (int x = 0; x < GridCells.GetLength(0); x++)
        {
            for (int y = 0; y < GridCells.GetLength(1); y++)
            {
                GridCells[x, y] = Instantiate(GridCellPrefab);
                GridCells[x, y].GetComponent<GridCell>().Register(x, y, this);
                GridCells[x, y].transform.position = transform.position + cellSize / 2 + new Vector3(x * cellSize.x, y * cellSize.y);
            }
        }

        for (int x = 0; x < GridVertices.GetLength(0); x++)
        {
            for (int y = 0; y < GridVertices.GetLength(1); y++)
            {
                GridVertices[x, y] = Instantiate(GridVertexPrefab);
                GridVertices[x, y].GetComponent<GridVertex>().Register(x, y, this);
                GridVertices[x, y].transform.position += new Vector3(x * cellSize.x, y * cellSize.y);
            }
        }
    }

    public GridVertex getClosestVertex(Vector3 position)
    {
        Vector3 cellSize = GridCellPrefab.GetComponent<BoxCollider2D>().size;

        Vector3 relativePosition = position - transform.position;
        int xPosition = (int)Mathf.Clamp(relativePosition.x / cellSize.x + 0.5f, 0, GridVertices.GetLength(0) - 1);
        int yPosition = (int) Mathf.Clamp(relativePosition.y / cellSize.y + 0.5f, 0, GridVertices.GetLength(1) - 1);

        return GridVertices[xPosition, yPosition].GetComponent<GridVertex>();
    }

    public GridCell getClosestCell(Vector3 position)
    {
        Vector3 cellSize = GridCellPrefab.GetComponent<BoxCollider2D>().size;

        Vector3 relativePosition = position - transform.position;
        int xPosition = (int)Mathf.Clamp(relativePosition.x / cellSize.x, 0, GridCells.GetLength(0) - 1);
        int yPosition = (int)Mathf.Clamp(relativePosition.y / cellSize.y, 0, GridCells.GetLength(1) - 1);

        return GridCells[xPosition, yPosition].GetComponent<GridCell>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
