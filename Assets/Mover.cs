using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {
    private Direction facing = Direction.DOWN;
    private SpriteRenderer sprite;

	// Use this for initialization
	void Start () {
        sprite = GetComponent<SpriteRenderer>();
	}

    // Update is called once per frame
    void OnMouseOver () {
        if (Input.GetKeyUp(KeyCode.R))
        {
            rotateClockwise();
        }
    }

    public void rotateClockwise()
    {
        facing = (Direction)(((int)facing + 1) % 4);
        transform.Rotate(Vector3.forward * -90);
    }

    public enum Direction
    {
        UP = 0, RIGHT = 1, DOWN = 2, LEFT = 3
    }
}
