using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGrid : MonoBehaviour {

    public int Width;
    public int Height;
    public GameObject GridCellPrefab;

    public GameObject[,] GridCells;
    public GameObject[,] GridVertices;

    private void Awake() {
        GridCells = new GameObject[Width, Height];
        GridVertices = new GameObject[Width + 1, Height + 1];
    }

    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
