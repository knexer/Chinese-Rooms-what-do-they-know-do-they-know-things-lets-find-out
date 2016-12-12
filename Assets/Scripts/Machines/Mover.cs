using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : VertexMachine {
    private Direction facing = Direction.DOWN;
    private SpriteRenderer sprite;

	// Use this for initialization
	protected void Start () {
        base.Start();
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

    protected override void Manipulate(float tickTime)
    {
        if (GridVertex.Grid.CurrentInput != null)
        {
            // is there a tile over us?
            if (GridVertex.Grid.CurrentInput.gridVertexX == this.GridVertex.X
                && GridVertex.Grid.CurrentInput.gridVertexY == this.GridVertex.Y)
            {
                GridVertex.Grid.CurrentInput.MovementDirection = facing;
            }
        }
    }

    public enum Direction
    {
        UP = 0, RIGHT = 1, DOWN = 2, LEFT = 3
    }
}

public static class DirectionExtensions
{
    public static Vector2 ToUnitVector(this Mover.Direction direction)
    {
        int x = 0;
        int y = 0;
        switch (direction)
        {
            case Mover.Direction.UP:
                y = 1;
                break;
            case Mover.Direction.RIGHT:
                x = 1;
                break;
            case Mover.Direction.DOWN:
                y = -1;
                break;
            case Mover.Direction.LEFT:
                x = -1;
                break;
        }

        return new Vector2(x, y);
    }

    public static Mover.Direction Clockwise(this Mover.Direction direction)
    {
        switch(direction)
        {
            case Mover.Direction.UP:
                return Mover.Direction.RIGHT;
            case Mover.Direction.RIGHT:
                return Mover.Direction.DOWN;
            case Mover.Direction.DOWN:
                return Mover.Direction.LEFT;
            case Mover.Direction.LEFT:
                return Mover.Direction.UP;
            default:
                throw new ArgumentException("OMGWTFBBQ");
        }
    }
}
