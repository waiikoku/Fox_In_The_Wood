using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemUI : MonoBehaviour
{
    [SerializeField] private GameObject[] gems;
    [SerializeField] private GameController gc;

    private void Start()
    {
        gc.OnGemUpdated += UpdateGem;
        gc.RaiseGemEvent();
    }

    private void UpdateGem(int amount)
    {
        for (int i = 0; i < gems.Length; i++)
        {
            gems[i].SetActive(i < amount ? true : false);
        }
    }
}
