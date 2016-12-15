using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeToMachineGrid : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Camera camera = GetComponent<Camera>();
        MachineGrid grid = MachineGrid.Obj;
        Vector2 lowerLeftPosition = grid.getVertexWorldPosition(0, 0);
        Vector2 upperRightPosition = grid.getVertexWorldPosition(grid.GridVertices.GetLength(0) - 1, grid.GridVertices.GetLength(1) - 1);
        Vector2 averagePosition = (lowerLeftPosition + upperRightPosition) / 2.0f;
        camera.transform.position = new Vector3(averagePosition.x, averagePosition.y, transform.position.z);

        GetComponent<tk2dCamera>().ZoomFactor /= grid.Height / 6.0f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
