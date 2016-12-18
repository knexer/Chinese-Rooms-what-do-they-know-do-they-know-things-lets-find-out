using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/** Tablet is the input to the room. It's a 4x4 grid. */
[RequireComponent(typeof(GameTabletRenderer))]
public class GameTabletMover : MonoBehaviour, ITablet {

    public Color successColor;
    public Color errorColor;
    public tk2dSprite tabletBaseSprite;

    public int startGridVertexX;
    public int startGridVertexY;

    [HideInInspector]
    public Mover.Direction MovementDirection = Mover.Direction.UP;

    public ITabletCell TopLeft {
        get { return GetComponent<GameTabletRenderer>().TopLeft; }
        set { GetComponent<GameTabletRenderer>().TopLeft.SetState(value); } }
    public ITabletCell TopRight {
        get { return GetComponent<GameTabletRenderer>().TopRight; }
        set { GetComponent<GameTabletRenderer>().TopRight.SetState(value); } }
    public ITabletCell BottomLeft {
        get { return GetComponent<GameTabletRenderer>().BottomLeft; }
        set { GetComponent<GameTabletRenderer>().BottomLeft.SetState(value); } }
    public ITabletCell BottomRight {
        get { return GetComponent<GameTabletRenderer>().BottomRight; }
        set { GetComponent<GameTabletRenderer>().BottomRight.SetState(value); } }

    private bool moveStopped = false;
    
    public int GridCellX { get { return GridVertexX - 1; } }
    public int GridCellY { get { return GridVertexY - 1; } }
    public int GridVertexX { get; private set; }
    public int GridVertexY { get; private set; }

    // Use this for initialization
    void Start() {
        Reset();
        moveStopped = false;
        
        TickController.MoveTickEvent += TriggerMove;
        TickController.ResetTabletsEvent += Reset;
        LevelStateManager.InputChanged += OnGlobalInputChanged;
        LevelStateManager.LevelCompletedEvent += SetLevelCompletedColor;
    }

    void OnDestroy() {
        TickController.MoveTickEvent -= TriggerMove;
        TickController.ResetTabletsEvent -= Reset;
        LevelStateManager.InputChanged -= OnGlobalInputChanged;
        LevelStateManager.LevelCompletedEvent -= SetLevelCompletedColor;
    }

    private void OnGlobalInputChanged(ITablet state) {
        this.SetState(state);
    }

    private void SetLevelCompletedColor(bool success) {
        tabletBaseSprite.color = success ? successColor : errorColor;
    }

    private void Reset() {
        tabletBaseSprite.color = Color.white;
        MovementDirection = Mover.Direction.UP;

        GridVertexX = startGridVertexX;
        GridVertexY = startGridVertexY;
        transform.position = MachineGrid.Obj.getVertexWorldPosition(GridVertexX, GridVertexY);
        transform.rotation = Quaternion.Euler(0, 0, 0);

        this.SetState(LevelStateManager.InputTablet);
        InterruptMove();
    }
    
    private void InterruptMove()
    {
        moveStopped = true;
    }

    void TriggerMove(float lengthOfTickSeconds)
    {
        Vector2 direction = MovementDirection.ToUnitVector();

        // Check for pins
        if (NoFrontPins(GridVertexX, GridVertexY, direction))
        {
            Vector2 oldPosition = MachineGrid.Obj.getVertexWorldPosition(GridVertexX, GridVertexY);

            // Do the move in the grid
            GridVertexX += Mathf.RoundToInt(direction.x);
            GridVertexY += Mathf.RoundToInt(direction.y);

            // Animate the move
            direction.Scale(FindObjectOfType<MachineGrid>().GetCellSizeWorldSpace());

            StartCoroutine(DoMove(oldPosition, direction, lengthOfTickSeconds));
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
            if (PinAtPosition(GridVertexX, GridVertexY, direction, 1, -1)
                && offsetBlacklist.TrueForAll((vector) => !PinAtPosition(GridVertexX, GridVertexY, direction, Mathf.RoundToInt(vector.x), Mathf.RoundToInt(vector.y))))
            {
                // Start a rotation anticlockwise
                Debug.Log("Anticlockwise rotation starting.");
                StartCoroutine(DoRotation(direction, lengthOfTickSeconds, new Vector2(-1, 1), 90));
                GridVertexX += 2 * Mathf.RoundToInt(direction.x);
                GridVertexY += 2 * Mathf.RoundToInt(direction.y);

                this.RotateCounterclockwise();
            } else if (PinAtPosition(GridVertexX, GridVertexY, direction, 1, 1)
                && offsetBlacklist.TrueForAll((vector) => !PinAtPosition(GridVertexX, GridVertexY, direction, Mathf.RoundToInt(vector.x), Mathf.RoundToInt(-vector.y))))
            {
                // Start a rotation clockwise
                Debug.Log("Clockwise rotation starting.");
                StartCoroutine(DoRotation(direction, lengthOfTickSeconds, new Vector2(1, 1), -90));
                GridVertexX += 2 * Mathf.RoundToInt(direction.x);
                GridVertexY += 2 * Mathf.RoundToInt(direction.y);

                this.RotateCounterclockwise();
                this.RotateCounterclockwise();
                this.RotateCounterclockwise();
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
        Vector2 gridPosition = new Vector2(GridVertexX, GridVertexY);
        Vector2 newGridPosition = gridPosition + (Vector2)(Quaternion.AngleAxis(AngleFromTo(direction, Vector2.up), Vector3.forward) * offset);
        Vector2 origin = MachineGrid.Obj.getVertexWorldPosition(
            Mathf.RoundToInt(newGridPosition.x), 
            Mathf.RoundToInt(newGridPosition.y));
        
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
        transform.position = MachineGrid.Obj.getVertexWorldPosition(
            Mathf.RoundToInt(newPosition.x), 
            Mathf.RoundToInt(newPosition.y));
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

        // out of bounds check
        if (newGridPosition.x < 0
            || newGridPosition.x > MachineGrid.Obj.GridVertices.GetLength(0) - 1
            || newGridPosition.y < 0
            || newGridPosition.y > MachineGrid.Obj.GridVertices.GetLength(1) - 1)
        {
            return false;
        }

        VertexMachine machineAtPosition = MachineGrid.Obj.GridVertices[
                Mathf.RoundToInt(newGridPosition.x), 
                Mathf.RoundToInt(newGridPosition.y)]
            .GetComponent<GridVertex>().VertexMachine;

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

    IEnumerator DoMove(Vector2 from, Vector2 delta, float lengthOfTickSeconds)
    {
        moveStopped = false;
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
    public GameTabletCell GetTabletPieceByFactoryPosition(int x, int y) {
        int tabletX = x - GridCellX;
        int tabletY = y - GridCellY;
        if (tabletX == 0 && tabletY == 0) {
            return GetComponent<GameTabletRenderer>().bottomLeft;
        } else if (tabletX == 0 && tabletY == 1) {
            return GetComponent<GameTabletRenderer>().topLeft;
        } else if (tabletX == 1 && tabletY == 0) {
            return GetComponent<GameTabletRenderer>().bottomRight;
        } else if (tabletX == 1 && tabletY == 1) {
            return GetComponent<GameTabletRenderer>().topRight;
        } else {
            return null;
        }
    }

    public GameTabletCell[] GetAllCells() {
        return GetComponent<GameTabletRenderer>().GetAllCells();
    }
}
