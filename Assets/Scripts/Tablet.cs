using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** Tablet is the input to the room. It's a 4x4 grid. */
public class Tablet : MonoBehaviour {
    public float cellDistance;
    public GameObject TabletPiece;

    public Mover.Direction MovementDirection;

	private GameObject TopLeft;
	private GameObject TopRight;
	private GameObject BottomLeft;
	private GameObject BottomRight;

    /** gridX/Y refers to the grid position of the top left tablet piece. */
    public int gridX;
    public int gridY;

    // Use this for initialization
    void Start() {
        TopLeft = NewTablet(-1, 1);
        TopRight = NewTablet(1, 1);
        BottomLeft = NewTablet(-1, -1);
        BottomRight = NewTablet(1, -1);

        FindObjectOfType<MachineGrid>().CurrentInput = this;
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

        tablet.transform.position = new Vector3(x * cellDistance, y * cellDistance, 0);
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
        direction.Scale(FindObjectOfType<MachineGrid>().GridCellPrefab.GetComponent<BoxCollider2D>().size);

        StartCoroutine(DoMove(direction, lengthOfTickSeconds));
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
