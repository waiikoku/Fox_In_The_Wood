using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSeeker : MonoBehaviour
{
    private Rigidbody2D playerRB = null;

    public Vector2 playerPos;

    private void Update()
    {
        if (playerRB != null)
        {
            playerPos = playerRB.position;
        }
        else
        {
            playerPos = Vector2.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.CompareTag("Player"))
        {
            playerRB = coll.GetComponent<Rigidbody2D>();
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            playerRB = null;
        }
    }
}
