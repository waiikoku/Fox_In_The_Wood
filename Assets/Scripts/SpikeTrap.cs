using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
         if(collision.CompareTag("Player"))
        {
            PlayerCharacter c = collision.GetComponent<PlayerCharacter>();
            if(c != null)
            {
                c.TakeDamage(5);
                c.TakeDamage(1);
            }
        }
    }
}
