using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    /** gridX/Y refers to the grid position of the top left tablet piece. */
    public int gridX;
    public int gridY;

    // Use this for initialization
    void Start() {
        transform.position = FindObjectOfType<MachineGrid>().getVertexWorldPosition(gridX, gridY);
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
        if (NoFrontPins(gridX, gridY, direction))
        {
            // Do the move in the grid
            gridX += (int)direction.x;
            gridY += (int)direction.y;

            // Animate the move
            direction.Scale(FindObjectOfType<MachineGrid>().GetCellSizeWorldSpace());

            StartCoroutine(DoMove(direction, lengthOfTickSeconds));
        }
        else
        {
            // Do either a rotation or a bounceback
            // TODO(taylor): or a little of each
        }
    }

    private bool NoFrontPins(int gridX, int gridY, Vector2 direction)
    {

        return !PinAtPosition(gridX, gridY, direction, 1, -1)
            && !PinAtPosition(gridX, gridY, direction, 1, 0)
            && !PinAtPosition(gridX, gridY, direction, 1, 1);
    }

    private bool PinAtPosition(int gridX, int gridY, Vector2 direction, int parallelOffset, int perpendicularOffset)
    {
        Vector2 gridPosition = new Vector2(gridX, gridY);
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
        int tabletX = x - gridX;
        int tabletY = y - gridY;
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
