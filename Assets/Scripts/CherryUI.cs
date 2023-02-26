using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryUI : MonoBehaviour
{
    [SerializeField] private GameObject[] cherries;

    private void Awake()
    {
        PlayerCharacter pc = FindObjectOfType<PlayerCharacter>();
        if (pc != null)
        {
            pc.OnHealthChanged += UpdateCherry;
        }
    }

    private void UpdateCherry(int amount)
    {
        for (int i = 0; i < cherries.Length; i++)
        {
            cherries[i].SetActive(i < amount? true:false);
        }
    }
}
