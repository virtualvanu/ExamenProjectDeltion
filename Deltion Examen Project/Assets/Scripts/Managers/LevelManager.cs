﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//For this script to work it needs to be put in a level with a player (to avoid EnemyAI errors) and EntitySpawners
//To make it work the way you want you will need to asign all the public values in the inspector

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    private Player player;
    private List<EntitySpawner> allAvailableSpawners = new List<EntitySpawner>();
    private List<EntitySpawner> closestSpawners = new List<EntitySpawner>();

    //objectiveList
    public List<DestroyObjective> Levelobjectives = new List<DestroyObjective>();

    //Wave values
    [Tooltip("Fill from easy to hard for increased difficulty in later waves")]
    public List<GameObject> enemyTypes = new List<GameObject>();
    private List<GameObject> currentWaveEntitys = new List<GameObject>();
    private int curentWave;
    private int curentType;
    private int enemiesToAdd;
    public int minimumSpawnerSpread;
    public float timeBetweenIndividualSpawns;
    public float spawnTickTime;

    //Enemy values
    [HideInInspector]
    public float healthModifier;
    [HideInInspector]
    public float damageModifier;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
        SetdifficultyVariables(GameManager.instance.difficulty);
        if(GameObject.FindObjectOfType<EntitySpawner>())
        {
            foreach(EntitySpawner spawner in GameObject.FindObjectsOfType<EntitySpawner>())
            {
                if(!spawner.objectiveSpawner)
                {
                    allAvailableSpawners.Add(spawner);
                    spawner.timeBetweenSpawns = timeBetweenIndividualSpawns;
                }
            }
            StartCoroutine(SpawnTick(3));
        }
        if(GameObject.FindObjectOfType<DestroyObjective>())
        {
            foreach (DestroyObjective objective in GameObject.FindObjectsOfType<DestroyObjective>())
            {
                Levelobjectives.Add(objective);
            }
        }
    }

    public void CheckObjectives()
    {
        bool victory = true;

        foreach(DestroyObjective objective in Levelobjectives)
        {
            if(!objective.ObjectiveDone)
            {
                victory = false;
                break;
            }
        }

        if (victory)
            TriggerVictory();
    }

    private void TriggerVictory()
    {
        GameManager.instance.GameOver(true);
    }

    private void SetdifficultyVariables(int difficulty)
    {
        switch(difficulty)
        {
            case 1:
                break;
            case 2:
                healthModifier = 1;
                damageModifier = 1;
                enemiesToAdd = 2;
                break;
            case 3:
                break;
        }
    }

    private void SetupWave()
    {
        curentWave++;

        int amount = enemiesToAdd * Mathf.FloorToInt(curentWave / (curentType + 1));

        Debug.Log(amount);

        for (int i = 0; i < amount; i++)
        {
            currentWaveEntitys.Add(enemyTypes[curentType]);
        }

        if (curentType + 1 == enemyTypes.Count)
            curentType = 0;
        else
            curentType++;

        SpawnWave();
    }

    private void SpawnWave()
    {
        float timeToSpawnAllEnemys;
        timeToSpawnAllEnemys = timeBetweenIndividualSpawns * (currentWaveEntitys.Count - 1) / minimumSpawnerSpread;

        if(spawnTickTime * 0.5f > timeToSpawnAllEnemys || minimumSpawnerSpread == allAvailableSpawners.Count)
        {
            GetNearbySpawners(minimumSpawnerSpread);
        }
        else
        {
            minimumSpawnerSpread++;
            SpawnWave();
            return;
        }

        //These values are used to randomly asign a entity from the wave to a spawner
        int spawnsPerSpawner = Mathf.CeilToInt((float)currentWaveEntitys.Count / closestSpawners.Count);
        List<GameObject> wave = new List<GameObject>();
        wave.AddRange(currentWaveEntitys);

        foreach(EntitySpawner spawner in closestSpawners)
        {
            for(int i = 0; i < spawnsPerSpawner; i++)
            {
                if (wave.Count != 0)
                {
                    int randomEntity = Random.Range(0, wave.Count - 1);
                    spawner.AddToSpawnQue(wave[randomEntity]);
                    wave.Remove(wave[randomEntity]);
                }
            }
        }

        StartCoroutine(SpawnTick(spawnTickTime));
    }

    private void GetNearbySpawners(int spawnerSpread)
    {
        closestSpawners.Clear();

        for (int i = 0; i < spawnerSpread; i++)
        {
            float distance = Mathf.Infinity;
            EntitySpawner closestSpawner = allAvailableSpawners[0];

            foreach(EntitySpawner spawner in allAvailableSpawners)
            {
                float newDistance = Vector3.Distance(spawner.gameObject.transform.position, player.transform.position);
                if (distance > newDistance && !closestSpawners.Contains(spawner))
                {
                    distance = newDistance;
                    closestSpawner = spawner;
                }
            }

            closestSpawners.Add(closestSpawner);
        }
    }

    private IEnumerator SpawnTick(float time)
    {
        yield return new WaitForSeconds(time);
        SetupWave();
    }
}