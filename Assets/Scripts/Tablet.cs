using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/** Tablet is the input to the room. It's a 4x4 grid. */
public class Tablet : MonoBehaviour {
    public float cellDistance;
    public GameObject TabletPiece;

    public Transform TabletCellContainer;

    public Mover.Direction MovementDirection;

	private GameObject TopLeft;
	private GameObject TopRight;
	private GameObject BottomLeft;
	private GameObject BottomRight;

    private MachineGrid Grid;

    /** gridVertexX/Y refers to the grid position of the top left tablet piece. */
    public int gridVertexX;
    public int gridVertexY;

    public int gridCellX { get { return gridVertexX - 1; } }
    public int gridCellY { get { return gridVertexY - 1; } }
    // Use this for initialization
    void Start() {
        transform.position = FindObjectOfType<MachineGrid>().getVertexWorldPosition(gridVertexX, gridVertexY);
        TopLeft = NewTablet(-1, 1);
        TopRight = NewTablet(1, 1);
        BottomLeft = NewTablet(-1, -1);
        BottomRight = NewTablet(1, -1);

        Grid = FindObjectOfType<MachineGrid>();
        Grid.CurrentInput = this;
        TickController.MoveTickEvent += TriggerMove;
    }

    /** Create a new tablet cell at the relative position of x, y. */
	private GameObject NewTablet(float x, 
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
	private GameObject NewTablet(int x, int y) {
        return NewTablet((float) x, (float) y);
    }

    // Update is called once per frame
    void Update () {
		
	}

    void TriggerMove(float lengthOfTickSeconds)
    {
        Vector2 direction = MovementDirection.ToUnitVector();

        // Check for pins
        if (NoFrontPins(gridVertexX, gridVertexY, direction))
        {
            // Do the move in the grid
            gridVertexX += (int)direction.x;
            gridVertexY += (int)direction.y;

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
                && offsetBlacklist.TrueForAll((vector) => !PinAtPosition(gridVertexX, gridVertexY, direction, (int)vector.x, (int)vector.y)))
            {
                // Start a rotation anticlockwise
                Debug.Log("Anticlockwise rotation starting.");
                StartCoroutine(DoRotation(direction, lengthOfTickSeconds, new Vector2(-1, 1), 90));
                gridVertexX += 2 * (int)direction.x;
                gridVertexY += 2 * (int)direction.y;

                GameObject temp = TopRight;
                TopRight = BottomRight;
                BottomRight = BottomLeft;
                BottomLeft = TopLeft;
                TopLeft = temp;
            } else if (PinAtPosition(gridVertexX, gridVertexY, direction, 1, 1)
                && offsetBlacklist.TrueForAll((vector) => !PinAtPosition(gridVertexX, gridVertexY, direction, (int)vector.x, (int)-vector.y)))
            {
                // Start a rotation clockwise
                Debug.Log("Clockwise rotation starting.");
                StartCoroutine(DoRotation(direction, lengthOfTickSeconds, new Vector2(1, 1), -90));
                gridVertexX += 2 * (int)direction.x;
                gridVertexY += 2 * (int)direction.y;

                GameObject temp = TopRight;
                TopRight = TopLeft;
                TopLeft = BottomLeft;
                BottomLeft = BottomRight;
                BottomRight = temp;
            } else
            {
                // There's a pin in the way, but still at least one pin in front of us; bounce back.
                // TODO(taylor): animate a partial swing in some cases.
                Debug.Log("Bouncing back the way we came.");
            }
        }
    }

    private IEnumerator DoRotation(Vector2 direction, float lengthOfTickSeconds, Vector2 offset, float angle)
    {
        Vector2 gridPosition = new Vector2(gridVertexX, gridVertexY);
        Vector2 newGridPosition = gridPosition + (Vector2)(Quaternion.AngleAxis(Vector2.Angle(Vector2.up, direction), Vector3.forward) * offset);
        Vector2 origin = Grid.getVertexWorldPosition((int)newGridPosition.x, (int)newGridPosition.y);

        float startTime = Time.time;
        while (Time.time < startTime + lengthOfTickSeconds)
        {
            transform.RotateAround(origin, Vector3.forward, Time.deltaTime / lengthOfTickSeconds * angle);
            yield return null;
        }

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
        Vector2 newGridPosition = gridPosition + (Vector2) (Quaternion.AngleAxis(Vector2.Angle(Vector2.up, direction), Vector3.forward) * offset);

        // out of bounds check
        if (newGridPosition.x < 0
            || newGridPosition.x > Grid.GridVertices.GetLength(0) - 1
            || newGridPosition.y < 0
            || newGridPosition.y > Grid.GridVertices.GetLength(1) - 1)
        {
            return false;
        }

        VertexMachine machineAtPosition = Grid.GridVertices[(int)newGridPosition.x, (int)newGridPosition.y].GetComponent<GridVertex>().VertexMachine;

        if (machineAtPosition == null)
        {
            return false;
        }

        return machineAtPosition.GetComponent<PinMachine>() != null;
    }

    IEnumerator DoMove(Vector2 delta, float lengthOfTickSeconds)
    {
        Vector2 from = transform.position;
        float startTime = Time.time;
        while (Time.time < startTime + lengthOfTickSeconds)
        {
            transform.position = Vector2.Lerp(from, from + delta, (Time.time - startTime) / lengthOfTickSeconds);
            yield return null;
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
}
