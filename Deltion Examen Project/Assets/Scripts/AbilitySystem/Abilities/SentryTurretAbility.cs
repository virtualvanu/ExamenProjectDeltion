﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentryTurretAbility : Ability
{
    public GameObject turretPrefab;
    public float range;
    public float aggroRadius;
    public float damage;
    public float rotSpeed;
    public float bulletforce;
    public float maxAmmo;
    public float reloadTime;
    public float fireRate;

    private GameObject spawnedTurret;
    protected override void AbilityMechanic(Vector3? mPos)
    {
        spawnedTurret = Instantiate(turretPrefab, (Vector3)mPos, Quaternion.identity);
        spawnedTurret.GetComponent<SentryTurret>().Initialize(range, damage, aggroRadius, myPlayer, bulletforce, maxAmmo, reloadTime, fireRate);

        active = true;
    }

    protected override IEnumerator AfterDuration()
    {
        yield return new WaitForSeconds(duration);
        Destroy(spawnedTurret);
        spawnedTurret = null;
        StartCooldown();
    }
}
