using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cherry : MonoBehaviour
{
    [SerializeField] private CherryTree tree;
    private IngameUIController iguc;
    private void Awake()
    {
        iguc = FindObjectOfType<IngameUIController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            tree.CollectCherry();
            if (iguc != null)
            {
                iguc.SfxPickup();
            }
            GameController.Instance.AddCherry();
        }
    }
}
