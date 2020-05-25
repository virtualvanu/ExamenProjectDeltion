﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapIcon : MonoBehaviour
{
    public Color playerColor;
    public Color EnemyColor;
    public Color ObjectiveColor;
    public Color interactableColor;
    public Color deployableColor;
    private LineRenderer lineRend;
    [HideInInspector]
    public bool rendPlayer;
    public Transform miniMapCam = null;

    public enum IconType
    {
        Player,
        Enemy,
        Objective,
        Interactable,
        Deployable
    }

    public IconType myIconType;
    private SpriteRenderer icon;
    public Vector3 overrideScale;
    void Start()
    {
        icon = GetComponent<SpriteRenderer>();
        lineRend = GetComponent<LineRenderer>();

        switch (myIconType)
        {
            case IconType.Player:
                if(overrideScale == Vector3.zero)
                {
                    transform.localScale = new Vector3(0.4F, 0.4F, 0.4F);
                }
                else
                {
                    transform.localScale = overrideScale;
                }
                icon.color = playerColor;
                if (transform.parent.GetComponentInParent<PlayerController>().playerNumber == 0)
                {
                    rendPlayer = true;
                    SetLineRendererTarget(FindNearestDestroyObjective());
                }
                break;
            case IconType.Enemy:
                if (overrideScale == Vector3.zero)
                {
                    transform.localScale = new Vector3(0.4F, 0.4F, 0.4F);
                }
                else
                {
                    transform.localScale = overrideScale;
                }
                icon.color = EnemyColor;
                break;
            case IconType.Objective:
                if (overrideScale == Vector3.zero)
                {
                    transform.localScale = new Vector3(8F, 8F, 8F);
                }
                else
                {
                    transform.localScale = overrideScale;
                }
                icon.color = ObjectiveColor;
                break;
            case IconType.Interactable:
                if (overrideScale == Vector3.zero)
                {
                    transform.localScale = new Vector3(2.5F, 2.5F, 2.5F);
                }
                else
                {
                    transform.localScale = overrideScale;
                }
                icon.color = interactableColor;
                break;
            case IconType.Deployable:
                if (overrideScale == Vector3.zero)
                {
                    transform.localScale = new Vector3(0.3F, 0.3F, 0.3F);
                }
                else
                {
                    transform.localScale = overrideScale;
                }
                icon.color = deployableColor;
                break;
        }
    }

    private void Update()
    {
        if (rendPlayer)
        {
            SetLineRendererTarget(FindNearestDestroyObjective());
        }
    }

    public void SetLineRendererNextAlive()
    {
        rendPlayer = false;
        lineRend.positionCount = 0;
        foreach (PlayerController player in GameManager.instance.GetPlayers())
        {
            if (!player.GetComponent<Player>().death)
            {
                MinimapIcon newIcon = player.GetComponentInChildren<MinimapIcon>();
                newIcon.rendPlayer = true;

            }
        }
    }

    private void SetLineRendererTarget(Transform target)
    {
        if (target)
        {
            Vector3 targetPos = new Vector3(target.position.x, 5, target.position.z);
            if(lineRend.positionCount < 2)
                lineRend.positionCount = 2;

            if (lineRend.GetPosition(0) != transform.position)
                lineRend.SetPosition(0, transform.position);

            if (lineRend.GetPosition(1) != targetPos)
                lineRend.SetPosition(1, targetPos);
        }
        else
        {
            Debug.LogWarning("No target found");
            return;
        }
    }

    private Transform FindNearestDestroyObjective()
    {
        DestroyObjective[] objectives = FindObjectsOfType<DestroyObjective>();
        if(objectives.Length > 0)
        {
            Transform closestEntity = null;
            float rangeToClosest = Mathf.Infinity;
            foreach (DestroyObjective obj in objectives)
            {
                float newDistance = Vector3.Distance(transform.position, obj.transform.position);
                if (rangeToClosest > newDistance && obj.enabled && !obj.death)
                {
                    rangeToClosest = newDistance;
                    closestEntity = obj.transform;
                }
            }

            return closestEntity;
        }
        else
        {
            return null;
        }
    }

}
