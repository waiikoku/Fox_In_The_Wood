using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectMystic : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Mystic"))
        {
            GameController.Instance.TheEnd();
        }
    }
}
