using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** Tablet is the input to the room. It's a 4x4 grid. */
public class Tablet : MonoBehaviour {
    public float cellDistance;
    public GameObject TabletPiece;

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
    }

    /** Create a new table at the relative position of x, y. */
    private GameObject NewTablet(float x, float y) {
        var tablet = Instantiate(TabletPiece, transform, true);
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

    /** Gets the piece that is on position x,y of the room floor. */
    public GameObject GetTabletPieceByFactoryPosition(int x, int y) {
        int tabletX = x - gridX;
        int tabletY = y - gridY;
        if (tabletX == 0 && tabletY == 0) {
            return BottomLeft;
        } else if (tabletX == 0 && tabletY == 1) {
            return TopLeft;
        } else if (tabletX == 1 && tabletY == 0) {
            return BottomRight;
        } else if (tabletX == 1 && tabletY == 1) {
            return TopRight;
        } else {
            return null;
        }
    }
}
