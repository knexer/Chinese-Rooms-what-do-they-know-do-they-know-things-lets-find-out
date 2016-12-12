using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class BlackBoxButton : MonoBehaviour {

    public static BlackBoxButton Obj;

    public TabletCellUI TopLeftInput;
    public TabletCellUI TopRightInput;
    public TabletCellUI BottomLeftInput;
    public TabletCellUI BottomRightInput;

    public TabletCellUI TopLeftOutput;
    public TabletCellUI TopRightOutput;
    public TabletCellUI BottomLeftOutput;
    public TabletCellUI BottomRightOutput;

    // Use this for initialization
    void Start () {
        GetComponent<Button>().onClick.AddListener(UpdateOutput);
        Obj = this;
	}

    public void HideOutput() {
        TopLeftOutput.gameObject.SetActive(false);
        TopRightOutput.gameObject.SetActive(false);
        BottomLeftOutput.gameObject.SetActive(false);
        BottomRightOutput.gameObject.SetActive(false);
    }
	
	private void UpdateOutput() {
        TabletState inputState = new TabletState();
        inputState.TopLeft = CreateTabletCellState(TopLeftInput);
        inputState.TopRight = CreateTabletCellState(TopRightInput);
        inputState.BottomLeft = CreateTabletCellState(BottomLeftInput);
        inputState.BottomRight = CreateTabletCellState(BottomRightInput);

        TabletState outputState = Level.Obj.Evaluate(inputState);
        SetTabletCellState(outputState.TopLeft, TopLeftOutput);
        SetTabletCellState(outputState.TopRight, TopRightOutput);
        SetTabletCellState(outputState.BottomLeft, BottomLeftOutput);
        SetTabletCellState(outputState.BottomRight, BottomRightOutput);
    }

    private TabletCellState CreateTabletCellState(TabletCellUI ui) {
        TabletCellState state = new TabletCellState();
        state.Color = ui.Color;
        state.Symbol = ui.Symbol;
        return state;
    }

    private void SetTabletCellState(TabletCellState state, TabletCellUI ui) {
        ui.gameObject.SetActive(true);
        ui.SetState(state.Symbol, state.Color);
    }
}
