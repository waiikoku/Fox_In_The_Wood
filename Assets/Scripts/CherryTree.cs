using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryTree : MonoBehaviour
{
    [SerializeField] private GameObject[] cherries;
    [SerializeField] private float respawnDelay = 5f;
    [SerializeField] private int currentCherry = 0;
    public void CollectCherry()
    {
        currentCherry--;
        if(currentCherry == 0)
        {
            StartCoroutine(DelaySpawn());
        }
    }
    private IEnumerator DelaySpawn()
    {
        yield return new WaitForSeconds(respawnDelay);
        RespawnCherry();
    }
    private void RespawnCherry()
    {
        //2 Cherry
        if(Rand() > 50)
        {
            for (int i = 0; i < cherries.Length; i++)
            {
                cherries[i].SetActive(true);
            }
            currentCherry = 2;
        }
        else
        {
            int side;
            if (Rand() > 50)
            {
                side = 1;
            }
            else
            {
                side = 0;
            }
            cherries[side].SetActive(true);
            currentCherry = 1;
        }
    }
    private float Rand()
    {
        int seed = Mathf.RoundToInt(Time.time * 100);
        UnityEngine.Random.InitState(seed);
        int rand = UnityEngine.Random.Range(0, 100);
        return rand;
    }
}
