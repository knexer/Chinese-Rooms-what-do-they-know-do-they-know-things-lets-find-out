using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/** Tablet is the input to the room. It's a 4x4 grid. */
public class Tablet : MonoBehaviour {
    public float cellDistance;
    public TabletCell TabletPiece;

    public Transform TabletCellContainer;

    [HideInInspector]
    public Mover.Direction MovementDirection = Mover.Direction.UP;

	private TabletCell TopLeft;
	private TabletCell TopRight;
	private TabletCell BottomLeft;
	private TabletCell BottomRight;

    private MachineGrid Grid;

    private bool moveStopped = false;

    /** gridVertexX/Y refers to the grid position of the top left tablet piece. */
    public int startGridVertexX;
    public int startGridVertexY;

    [HideInInspector]
    public int gridVertexX;
    [HideInInspector]
    public int gridVertexY;

    public int gridCellX { get { return gridVertexX - 1; } }
    public int gridCellY { get { return gridVertexY - 1; } }
    // Use this for initialization
    void Start() {
        Reset();
        moveStopped = false;

        Grid = FindObjectOfType<MachineGrid>();
        Grid.CurrentInput = this;
        TickController.MoveTickEvent += TriggerMove;
        TickController.ResetTabletsEvent += Reset;
    }

    void OnDestroy() {
        TickController.MoveTickEvent -= TriggerMove;
        TickController.ResetTabletsEvent -= Reset;
    }

    private void Reset() {
        MovementDirection = Mover.Direction.UP;

        gridVertexX = startGridVertexX;
        gridVertexY = startGridVertexY;
        transform.position = FindObjectOfType<MachineGrid>().getVertexWorldPosition(gridVertexX, gridVertexY);
        transform.rotation = Quaternion.Euler(0, 0, 0);

        if (TopLeft != null)
            Destroy(TopLeft.gameObject);
        if (TopRight != null)
            Destroy(TopRight.gameObject);
        if (BottomLeft != null)
            Destroy(BottomLeft.gameObject);
        if (BottomRight != null)
            Destroy(BottomRight.gameObject);

        TopLeft = NewTablet(-1, 1);
        TopRight = NewTablet(1, 1);
        BottomLeft = NewTablet(-1, -1);
        BottomRight = NewTablet(1, -1);
        TickController.OutOfBoundEvent += () => TickController.Obj.SetSpeed(-1);
        InterruptMove();
    }
    
    private void InterruptMove()
    {
        moveStopped = true;
    }

    /** Create a new tablet cell at the relative position of x, y. */
	private TabletCell NewTablet(float x, 
								 float y, 
 						     	 TabletCell.Colors color = TabletCell.Colors.None,
		                         TabletCell.Symbols symbol = TabletCell.Symbols.Eye) {
        var tablet = Instantiate(TabletPiece, transform, true);
		// TODO(emmax): set values

		tablet.GetComponent<TabletCell>().Color = color;
		tablet.GetComponent<TabletCell>().Symbol = symbol;

        tablet.transform.parent = TabletCellContainer;
        tablet.transform.localPosition = new Vector3(x * cellDistance, y * cellDistance, 0);        
        tablet.transform.localScale = Vector3.one;

        print("Added tablet piece at " + x + ", " + y);
        return tablet;
    }

    /** Create a new table at the relative position of x, y. */
	private TabletCell NewTablet(int x, int y) {
        return NewTablet((float) x, (float) y);
    }

    void TriggerMove(float lengthOfTickSeconds)
    {
        Vector2 direction = MovementDirection.ToUnitVector();

        // Check for pins
        if (NoFrontPins(gridVertexX, gridVertexY, direction))
        {
            // Do the move in the grid
            gridVertexX += Mathf.RoundToInt(direction.x);
            gridVertexY += Mathf.RoundToInt(direction.y);

            // Animate the move
            direction.Scale(FindObjectOfType<MachineGrid>().GetCellSizeWorldSpace());

            StartCoroutine(DoMove(direction, lengthOfTickSeconds));
        }
        else
        {
            // Do either a rotation or a bounceback
            List<Vector2> offsetBlacklist = new List<Vector2>();
            offsetBlacklist.Add(new Vector2(0, 0));
            offsetBlacklist.Add(new Vector2(0, 1));
            offsetBlacklist.Add(new Vector2(1, 0));
            offsetBlacklist.Add(new Vector2(1, 1));
            offsetBlacklist.Add(new Vector2(2, 0));
            offsetBlacklist.Add(new Vector2(2, 1));
            if (PinAtPosition(gridVertexX, gridVertexY, direction, 1, -1)
                && offsetBlacklist.TrueForAll((vector) => !PinAtPosition(gridVertexX, gridVertexY, direction, Mathf.RoundToInt(vector.x), Mathf.RoundToInt(vector.y))))
            {
                // Start a rotation anticlockwise
                Debug.Log("Anticlockwise rotation starting.");
                StartCoroutine(DoRotation(direction, lengthOfTickSeconds, new Vector2(-1, 1), 90));
                gridVertexX += 2 * Mathf.RoundToInt(direction.x);
                gridVertexY += 2 * Mathf.RoundToInt(direction.y);

                TabletCell temp = TopRight;
                TopRight = BottomRight;
                BottomRight = BottomLeft;
                BottomLeft = TopLeft;
                TopLeft = temp;
            } else if (PinAtPosition(gridVertexX, gridVertexY, direction, 1, 1)
                && offsetBlacklist.TrueForAll((vector) => !PinAtPosition(gridVertexX, gridVertexY, direction, Mathf.RoundToInt(vector.x), Mathf.RoundToInt(-vector.y))))
            {
                // Start a rotation clockwise
                Debug.Log("Clockwise rotation starting.");
                StartCoroutine(DoRotation(direction, lengthOfTickSeconds, new Vector2(1, 1), -90));
                gridVertexX += 2 * Mathf.RoundToInt(direction.x);
                gridVertexY += 2 * Mathf.RoundToInt(direction.y);

                TabletCell temp = TopRight;
                TopRight = TopLeft;
                TopLeft = BottomLeft;
                BottomLeft = BottomRight;
                BottomRight = temp;
            } else
            {
                // There's a pin in the way, but still at least one pin in front of us; bounce back.
                // TODO(taylor): animate a partial swing in some cases.
                Debug.Log("Bouncing back the way we came.");
                MovementDirection = MovementDirection.Clockwise().Clockwise();
                TriggerMove(lengthOfTickSeconds);
            }
        }
    }

    private IEnumerator DoRotation(Vector2 direction, float lengthOfTickSeconds, Vector2 offset, float angle)
    {
        moveStopped = false;
        Vector2 gridPosition = new Vector2(gridVertexX, gridVertexY);
        Vector2 newGridPosition = gridPosition + (Vector2)(Quaternion.AngleAxis(AngleFromTo(direction, Vector2.up), Vector3.forward) * offset);
        Vector2 origin = Grid.getVertexWorldPosition(Mathf.RoundToInt(newGridPosition.x), Mathf.RoundToInt(newGridPosition.y));
        
        float startTime = Time.time;
        Vector2 originalPosition = transform.position;
        Quaternion originalRotation = transform.rotation;
        while (Time.time < startTime + lengthOfTickSeconds)
        {
            yield return null;
            if (moveStopped)
            {
                moveStopped = false;
                yield break;
            }
            transform.position = originalPosition;
            transform.rotation = originalRotation;
            transform.RotateAround(origin, Vector3.forward, (Time.time - startTime) / lengthOfTickSeconds * angle);
        }

        // set position and rotation to correct for drift
        Vector2 newPosition = gridPosition + direction * 2;
        transform.position = Grid.getVertexWorldPosition(Mathf.RoundToInt(newPosition.x), Mathf.RoundToInt(newPosition.y));
        transform.rotation = originalRotation * Quaternion.AngleAxis(angle, Vector3.forward);

        yield break;
    }

    private bool NoFrontPins(int gridVertexX, int gridVertexY, Vector2 direction)
    {
        return !PinAtPosition(gridVertexX, gridVertexY, direction, 1, -1)
            && !PinAtPosition(gridVertexX, gridVertexY, direction, 1, 0)
            && !PinAtPosition(gridVertexX, gridVertexY, direction, 1, 1);
    }

    private bool PinAtPosition(int gridVertexX, int gridVertexY, Vector2 direction, int parallelOffset, int perpendicularOffset)
    {
        Vector2 gridPosition = new Vector2(gridVertexX, gridVertexY);
        Vector2 offset = new Vector2(perpendicularOffset, parallelOffset);
        Vector2 newGridPosition = gridPosition + (Vector2) (Quaternion.AngleAxis(AngleFromTo(direction, Vector2.up), Vector3.forward) * offset);

        //Debug.Log("===================");
        //Debug.Log("Direction:" + direction);
        //Debug.Log("Offset:" + offset);
        //Debug.Log("Grid Position:" + gridPosition);
        //Debug.Log("New Grid position: " + newGridPosition);

        // out of bounds check
        if (newGridPosition.x < 0
            || newGridPosition.x > Grid.GridVertices.GetLength(0) - 1
            || newGridPosition.y < 0
            || newGridPosition.y > Grid.GridVertices.GetLength(1) - 1)
        {
            return false;
        }

        VertexMachine machineAtPosition = Grid.GridVertices[Mathf.RoundToInt(newGridPosition.x), Mathf.RoundToInt(newGridPosition.y)].GetComponent<GridVertex>().VertexMachine;

        if (machineAtPosition == null)
        {
            return false;
        }

        return machineAtPosition.GetComponent<PinMachine>() != null;
    }

    private float AngleFromTo(Vector2 from, Vector2 to)
    {
        float angle = Vector2.Angle(from, to);
        if (Vector3.Cross(from, to).z > 0)
            angle = 360 - angle;

        return angle;
    }

    IEnumerator DoMove(Vector2 delta, float lengthOfTickSeconds)
    {
        moveStopped = false;
        Vector2 from = transform.position;
        float startTime = Time.time;
        while (Time.time < startTime + lengthOfTickSeconds)
        {
            transform.position = Vector2.Lerp(from, from + delta, (Time.time - startTime) / lengthOfTickSeconds);
            yield return null;
            if (moveStopped)
            {
                moveStopped = false;
                yield break;
            }
        }
        transform.position = from + delta;
    }

    /** Gets the piece that is on position x,y of the room floor. */
	public TabletCell GetTabletPieceByFactoryPosition(int x, int y) {
        int tabletX = x - gridCellX;
        int tabletY = y - gridCellY;
		if (tabletX == 0 && tabletY == 0) {
			return BottomLeft.GetComponent<TabletCell>();
		} else if (tabletX == 0 && tabletY == 1) {
			return TopLeft.GetComponent<TabletCell>();
		} else if (tabletX == 1 && tabletY == 0) {
			return BottomRight.GetComponent<TabletCell>();
		} else if (tabletX == 1 && tabletY == 1) {
			return TopRight.GetComponent<TabletCell>();
		} else {
			return null;
		}
    }

	public TabletCell[] GetAllPieces() {
		TabletCell[] tablets = new TabletCell[4];
		tablets [0] = TopLeft;
		tablets [1] = TopRight;
		tablets [2] = BottomLeft;
		tablets [3] = BottomRight;
		return tablets;
	}

    public bool Equals(Tablet other)
    {
        bool equality_state = TopLeft.Equals(other.TopLeft);
        equality_state &= TopRight.Equals(other.TopRight);
        equality_state &= BottomLeft.Equals(other.BottomLeft);
        equality_state &= BottomRight.Equals(other.BottomRight);
        return equality_state;
    }
}
