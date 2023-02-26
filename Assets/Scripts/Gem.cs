using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    private IngameUIController iguc;

    private void Awake()
    {
        iguc = FindObjectOfType<IngameUIController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (iguc != null)
            {
                iguc.SfxPickup();
            }
            gameObject.SetActive(false);
            GameController.Instance.AddGem();
            GameController.Instance.PlayVFX(transform);

        }
    }
}
