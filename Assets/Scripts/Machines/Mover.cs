﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : VertexMachine {
	public enum Conditionals
	{
		None,
		False,
		True
	}

	private Conditionals TopLeft;
	private Conditionals TopRight;
	private Conditionals BottomLeft;
	private Conditionals BottomRight;

    private Direction facing = Direction.DOWN;
    private SpriteRenderer sprite;

	// Use this for initialization
	protected void Start () {
        base.Start();

		// Initialize conditionals.
		TopLeft = Conditionals.None;
		TopRight = Conditionals.None;
		BottomLeft = Conditionals.None;
		BottomRight = Conditionals.None;

		// Set to default arrow
		UpdateImage();
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
		if (GridVertex.Grid.CurrentInput != null && CanMove())
        {
            // is there a tile over us?
            if (GridVertex.Grid.CurrentInput.gridVertexX == this.GridVertex.X
                && GridVertex.Grid.CurrentInput.gridVertexY == this.GridVertex.Y)
            {
                GridVertex.Grid.CurrentInput.MovementDirection = facing;
				UnmeetAllConditionals ();
            }
        }
    }

    public enum Direction
    {
        UP = 0, RIGHT = 1, DOWN = 2, LEFT = 3
    }

	private bool CanMove() {
		if ((TopLeft == Conditionals.False) || (TopRight == Conditionals.False) ||
			(BottomLeft == Conditionals.False) || (TopRight == Conditionals.False)) {
			return false;
		}
		return true;
	}

	private void UpdateImage() {
		if ((TopLeft == Conditionals.None) && (TopRight == Conditionals.None) &&
			(BottomLeft == Conditionals.None) && (TopRight == Conditionals.None)) {
			sprite = GetComponent<SpriteRenderer>();
		} else if ((TopLeft == Conditionals.False) || (TopRight == Conditionals.False) ||
			(BottomLeft == Conditionals.False) || (TopRight == Conditionals.False)) {
			// Set as unfulfilled conditional arrow.
		} else {
			// Set as fulfilled conditional arrow.
		}
	}

	public void AddConditional(int gridX, int gridY) {
		GridCell[] cells = GridVertex.GetSurroundingCells ();
		if (cells [0].X == gridX && cells [0].Y == gridY) {
			TopLeft = Conditionals.False;
		}
		if (cells [1].X == gridX && cells [1].Y == gridY) {
			TopRight = Conditionals.False;
		}
		if (cells [2].X == gridX && cells [2].Y == gridY) {
			BottomLeft = Conditionals.False;
		}
		if (cells [3].X == gridX && cells [3].Y == gridY) {
			BottomRight = Conditionals.False;
		}
		UpdateImage ();
	}

	public void RemoveConditional(int gridX, int gridY) {
		GridCell[] cells = GridVertex.GetSurroundingCells ();
		if (cells [0].X == gridX && cells [0].Y == gridY) {
			TopLeft = Conditionals.None;
		}
		if (cells [1].X == gridX && cells [1].Y == gridY) {
			TopRight = Conditionals.None;
		}
		if (cells [2].X == gridX && cells [2].Y == gridY) {
			BottomLeft = Conditionals.None;
		}
		if (cells [3].X == gridX && cells [3].Y == gridY) {
			BottomRight = Conditionals.None;
		}
		UpdateImage ();
	}

	public void RemoveAllConditionals() {
		TopLeft = Conditionals.None;
		TopRight = Conditionals.None;
		BottomLeft = Conditionals.None;
		BottomRight = Conditionals.None;
		UpdateImage ();
	}

	public void MeetConditional(int gridX, int gridY) {
		GridCell[] cells = GridVertex.GetSurroundingCells ();
		if (cells [0].X == gridX && cells [0].Y == gridY) {
			TopLeft = Conditionals.True;
		}
		if (cells [1].X == gridX && cells [1].Y == gridY) {
			TopRight = Conditionals.True;
		}
		if (cells [2].X == gridX && cells [2].Y == gridY) {
			BottomLeft = Conditionals.True;
		}
		if (cells [3].X == gridX && cells [3].Y == gridY) {
			BottomRight = Conditionals.True;
		}
		UpdateImage ();
	}

	public void UnmeetAllConditionals() {
		if (TopLeft == Conditionals.True) {
			TopLeft = Conditionals.False;
		}
		if (TopRight == Conditionals.True) {
			TopRight = Conditionals.False;
		}
		if (BottomLeft == Conditionals.True) {
			BottomLeft = Conditionals.False;
		}
		if (BottomRight == Conditionals.True) {
			BottomRight = Conditionals.False;
		}
		UpdateImage ();
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
}
