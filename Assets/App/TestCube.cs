using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Colors
{
    Red,
    Yellow,
    Green,
    Magenta,
    Length
}
public class TestCube : CStatefulBehaviour
{
    public Renderer Renderer;
    public RedState RedState;
    public YellowState YellowState;
    public ColorSkipState ColorSkipState;
    private Colors CurrentColor;
    // Start is called before the first frame update
    override protected void Awake()
    {
        base.Awake();
        Renderer = GetComponent<Renderer>();
        RedState = new RedState(this);
        YellowState = new YellowState(this);
        ColorSkipState = new ColorSkipState(this);
        StateMachine.SubscribeToStateTransition(RedState,YellowState, e => { e.transform.position += Vector3.up * 5; }, this, false);
        RunStateMachine(RedState);

    }

    public void ChangeColor(Colors colors)
    {
        Color color = Renderer.material.color;

        switch (colors)
        {
            case Colors.Red:
                color = Color.red;
                break;
            case Colors.Yellow:
                color = Color.yellow;
                break;
            case Colors.Green:
                color = Color.green;
                break;
            case Colors.Magenta:
                color = Color.magenta;
                break;
            default:
                break;
        }

        Renderer.material.color = color;
        CurrentColor = colors;
    }

    public void ChangeColorNext()
    {
        ChangeColor((Colors)(((int)CurrentColor + 1) % (int)Colors.Length));
    }
}
