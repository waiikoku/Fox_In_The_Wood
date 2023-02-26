using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private readonly string AXIS_HORIZONTAL = "Horizontal";
    private readonly string AXIS_VERTICAL = "Vertical";
    private float horizontalAxis;
    private float verticalAxis;
    private Vector2 axis;
    public Vector2 Axis
    {
        get
        {
            return axis;
        }
    }
    private float axisSqrMagnitude;

    //Mouse Events
    public event Action OnLeftClick;
    public event Action OnLeftUp;
    public event Action OnRightClick;
    //Keyboard Events
    public event Action OnSpacebar;
    private void Update()
    {
        InputAxis();
        InputMouse();
        InputKeyboard();
    }

    private void InputAxis()
    {
        horizontalAxis = Input.GetAxis(AXIS_HORIZONTAL);
        verticalAxis = Input.GetAxis(AXIS_VERTICAL);
        axis.x = horizontalAxis;
        axis.y = verticalAxis;
        if(axis.sqrMagnitude >= 1f)
        {
            axis.Normalize();
        }
        axisSqrMagnitude = axis.sqrMagnitude;
    }

    private void InputMouse()
    {
        //Left Click
        if (Input.GetMouseButtonDown(0))
        {
            OnLeftClick?.Invoke();
        }
        //Left Mouse Up
        if (Input.GetMouseButtonUp(0))
        {
            OnLeftUp?.Invoke();
        }

        //Right Click
        if (Input.GetMouseButtonDown(1))
        {
            OnRightClick?.Invoke();
        }
    }

    private void InputKeyboard()
    {
        //Spacebar
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnSpacebar?.Invoke();
        }
    }
}
