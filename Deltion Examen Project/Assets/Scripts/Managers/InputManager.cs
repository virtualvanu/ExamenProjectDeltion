﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This Manager is used as a centralized point for all of the Players Input.
//This makes it easier to find problems related to input for debugging purposes.

public class InputManager : MonoBehaviour
{
    //Input Delegates
    public delegate void BaseInput();
    public delegate void FloatInput(float value);
    public delegate void AxisInput(float xAxis, float yAxis);

    public BaseInput leftMouseButtonEvent;
    public BaseInput rightMouseButtonEvent;
    public BaseInput reloadEvent;
    public BaseInput interactEvent;
    public FloatInput abilityEvent;
    public FloatInput scrollEvent;
    public AxisInput Moving;

    //Movement input values
    private float xAxis;
    private float yAxis;

    //Asigning empty functions to the delegates to avoid Errors
    private void Awake()
    {
        leftMouseButtonEvent += Empty;
        rightMouseButtonEvent += Empty;
        reloadEvent += Empty;
        interactEvent += Empty;
        abilityEvent += EmptyFloat;
        scrollEvent += EmptyFloat;
        Moving += EmptyAxis;
    }

    private void Update()
    {
        //Generic input
        if (Input.GetMouseButtonDown(0))
            LeftMouse();
        if (Input.GetMouseButtonDown(1))
            RightMouse();
        if (Input.GetButtonDown("Reload"))
            Reload();
        if (Input.GetButtonDown("Interact"))
            Interact();
        if (Input.GetButtonDown("Ability 01"))
            AbilityHotkeys(0);
        if (Input.GetButtonDown("Ability 02"))
            AbilityHotkeys(1);
        if (Input.GetButtonDown("Ability 03"))
            AbilityHotkeys(2);
        if (Input.GetButtonDown("Ability 04"))
            AbilityHotkeys(3);
        if (Input.GetAxis("Mouse ScrollWheel") > 0f || Input.GetAxis("Mouse ScrollWheel") < 0f)
            SwitchWeapon(Input.GetAxis("Mouse ScrollWheel"));

        //Movement input
        if (Input.GetAxis("Horizontal") > 0f || Input.GetAxis("Horizontal") < 0f)
            MovingHorizontal(Input.GetAxis("Horizontal"));
        if (Input.GetAxis("Vertical") > 0f || Input.GetAxis("Vertical") < 0f)
            MovingVertical(Input.GetAxis("Vertical"));
    }

    //All functions related to the inputs
    #region
        //Generic input
        private void LeftMouse()
        {
            Debug.Log("Left mouse");
            leftMouseButtonEvent.Invoke();
        }
        private void RightMouse()
        {
            Debug.Log("Right mouse");
            rightMouseButtonEvent.Invoke();
        }
        private void Reload()
        {
            Debug.Log("Reload");
            reloadEvent.Invoke();
        }
        private void Interact()
        {
            Debug.Log("Interact");
            interactEvent.Invoke();
        }
        private void AbilityHotkeys(int inputAbility)
        {
            Debug.Log(inputAbility);
            abilityEvent.Invoke(inputAbility);
        }
        private void SwitchWeapon(float inputScroll)
        {
            Debug.Log(inputScroll);
            scrollEvent(inputScroll);
        }

        //Movement input
        private void MovingHorizontal(float x)
        {
            Debug.Log(x);
            xAxis = x;
            Moving.Invoke(x, yAxis);
        }
        private void MovingVertical(float y)
        {
            Debug.Log(y);
            yAxis = y;
            Moving.Invoke(xAxis, y);
        }
    #endregion

    //Empty functions to avoid "nothing inside the delegate" Errors with the delegates
    #region
        public void Empty()
        {
        }
        public void EmptyFloat(float value)
        {
        }
        public void EmptyAxis(float xAxis, float yAxis)
        {
        }
    #endregion
}