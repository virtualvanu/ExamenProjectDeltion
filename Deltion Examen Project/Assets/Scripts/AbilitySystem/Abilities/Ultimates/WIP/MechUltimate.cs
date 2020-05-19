﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechUltimate : Entity
{
    private float myDamage;
    private MechUltimateAbility myAbility;
    private Movement movement;
    private Animator anim;
    private Rigidbody rigidbody;

    public void Initialize(float mechHp, float damage, MechUltimateAbility ability)
    {
        Debug.LogError("Ability not implemented");
        rigidbody = GetComponent<Rigidbody>();
        return;
        //maxHp = mechHp;
        //hp = maxHp;
        //movement = GetComponent<Movement>();
        //myAbility = ability;

        //InputManager.MovingEvent += Move;
        //InputManager.RotatingEvent += Rotate;
        //EntityManager.instance.AddPlayerOrAbility(this);
        ////InputManager.leftMouseButtonEvent += Shoot;

        //anim = GetComponentInChildren<Animator>();
        //myDamage = damage;
    }

    protected override void Death()
    {
        base.Death();
        myAbility.MechDestroyed();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        //InputManager.MovingEvent -= Move;
        //InputManager.RotatingEvent -= Rotate;
    }

    public void Move(float xAxis, float yAxis)
    {
        movement.Move(xAxis, yAxis, rigidbody);
        anim.SetFloat("X", xAxis);
        anim.SetFloat("Y", yAxis);
    }

    public void Rotate(float xAxis, float zAxis)
    {
        movement.Rotate(xAxis, zAxis);
    }

   
}
