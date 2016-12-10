using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGrid : MonoBehaviour {
    public int Width;
    public int Height;
    public GameObject GridCellPrefab;
    public GameObject GridVertexPrefab;

    [HideInInspector]
    public GameObject[,] GridCells;
    [HideInInspector]
    public GameObject[,] GridVertices;

    private void Awake() {
        GridCells = new GameObject[Width, Height];
        GridVertices = new GameObject[Width + 1, Height + 1];

        for (int x = 0; x < GridCells.GetLength(0); x++)
        {
            for (int y = 0; y < GridCells.GetLength(1); y++)
            {
                GridCells[x, y] = Instantiate(GridCellPrefab);
                GridCells[x, y].GetComponent<GridCell>().Register(x, y, this);
            }
        }

        for (int x = 0; x < GridVertices.GetLength(0); x++)
        {
            for (int y = 0; y < GridVertices.GetLength(1); y++)
            {
                GridVertices[x, y] = Instantiate(GridVertexPrefab);
                GridVertices[x, y].GetComponent<GridVertex>().Register(x, y, this);
            }
        }
    }

    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
