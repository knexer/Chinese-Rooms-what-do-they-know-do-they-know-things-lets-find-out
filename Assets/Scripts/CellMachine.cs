using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellMachine : MonoBehaviour {

    public GridCell GridCell { get; set; }

	// Use this for initialization
	void Start () {
        TickController.ManipulateTickEvent += Manipulate;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void Manipulate(float tickTime) {
        
    }
}
