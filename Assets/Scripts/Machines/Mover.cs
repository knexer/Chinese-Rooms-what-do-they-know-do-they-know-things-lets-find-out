using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : VertexMachine {  
  public tk2dSpriteAnimator moverAnimator;

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

	// This is used to determine whether to show a sound
	// effect during CanMove.
	private bool meetsConditions;

    private Direction facing = Direction.DOWN;

    private MoverTypes moverType;

    public MoverTypes MoverType {
      get {
        return moverType;
      }
      set {
        moverType = value;
        SetMoverSprite(moverType);
      }
    }

    public enum MoverTypes {
      Default,
      Conditional
    }

	// Use this for initialization
	protected override void Start () {
		TickController.ManipulateTickEvent += Manipulate;

		// Initialize conditionals.
		TopLeft = Conditionals.None;
		TopRight = Conditionals.None;
		BottomLeft = Conditionals.None;
		BottomRight = Conditionals.None;

		// Set to default arrow
		UpdateImage();
	}

    protected override void OnDestroy()
    {
        TickController.ManipulateTickEvent -= Manipulate;
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyUp(KeyCode.R)
            && gameObject.GetComponent<DraggableVertexMachine>().dragging
            && TickController.Obj.Mode == TickController.TimeState.Stopped)
        {
            SoundManager.Instance.PlaySound(SoundManager.SoundTypes.RotateMover);
            rotateClockwise();
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetKeyUp(KeyCode.R)
            && !gameObject.GetComponent<DraggableVertexMachine>().dragging
            && TickController.Obj.Mode == TickController.TimeState.Stopped)
        {
            SoundManager.Instance.PlaySound(SoundManager.SoundTypes.RotateMover);
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
		if (GridVertex.Grid.Input != null)
        {
            // is there a tile over us?
            if (GridVertex.Grid.Input.gridVertexX == this.GridVertex.X
                && GridVertex.Grid.Input.gridVertexY == this.GridVertex.Y
				&& CanMove())
            {
                GridVertex.Grid.Input.MovementDirection = facing;
            }
        }
		UnmeetAllConditionals ();
    }

    public enum Direction
    {
        UP = 0, RIGHT = 1, DOWN = 2, LEFT = 3
    }

	private bool CanMove() {
		meetsConditions = false;
		GridCell[] cells = GridVertex.GetSurroundingCells ();
		for (int i = 0; i < 4; i++) {
			CellMachine machine = cells [i].CellMachine;
			if (machine != null) {
				machine.CheckCondition ();
			}
		}

		if ((TopLeft == Conditionals.False) || (TopRight == Conditionals.False) ||
			(BottomLeft == Conditionals.False) || (BottomRight == Conditionals.False)) {
			SoundManager.Instance.PlaySound(SoundManager.SoundTypes.ContitionalFailed);
			return false;
		}
		if (meetsConditions) {
			SoundManager.Instance.PlaySound (SoundManager.SoundTypes.ConditionalMet);
		}
		return true;
	}

	private void UpdateImage() {
		if ((TopLeft == Conditionals.None) && (TopRight == Conditionals.None) &&
			(BottomLeft == Conditionals.None) && (BottomRight == Conditionals.None)) {
			MoverType = MoverTypes.Default;
		} else {
			// Set as conditional arrow.
			MoverType = MoverTypes.Conditional;
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
			meetsConditions = true;
			TopLeft = Conditionals.True;
		}
		if (cells [1].X == gridX && cells [1].Y == gridY) {
			meetsConditions = true;
			TopRight = Conditionals.True;
		}
		if (cells [2].X == gridX && cells [2].Y == gridY) {
			meetsConditions = true;
			BottomLeft = Conditionals.True;
		}
		if (cells [3].X == gridX && cells [3].Y == gridY) {
			meetsConditions = true;
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

	public override void OnPlace() {
		GridCell[] cells = GridVertex.GetSurroundingCells ();
		for (int i = 0; i < 4; i++) {
			CellMachine machine = cells [i].CellMachine;
			if (machine != null) {
				machine.OnPlace ();
			}
		}
	}

	public override void OnRemove() {
		RemoveAllConditionals ();
	}

  	private void SetMoverSprite (MoverTypes type) {
    	switch (type) {
    		case MoverTypes.Conditional:
     		  moverAnimator.SetFrame(1);
      		  break;

    		default:
      	  	  moverAnimator.SetFrame(0);     
      		  break;
    	}
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
