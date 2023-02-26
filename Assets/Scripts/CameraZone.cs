using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZone : MonoBehaviour
{
    private CameraFollow cf;
    [SerializeField] private Vector3 ZoneOffset;
    [SerializeField] private Transform entryPoint;
    [SerializeField] private Transform exitPoint;

    private void Start()
    {
        cf = FindObjectOfType<CameraFollow>();
    }

    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            cf.SetOffset(ZoneOffset);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            cf.ResetOffset();
        }
    }
    */

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (CheckIsEntry(collision.transform))
            {
                cf.SetOffset(ZoneOffset);
            }
            else
            {
                cf.ResetOffset();
            }
        }
    }

    private bool CheckIsEntry(Transform t)
    {
        Vector3 pos = t.position;
        if(pos.x > entryPoint.position.x)
        {
            return true;
        }
        else if(pos.x < exitPoint.position.x)
        {
            return false;
        }
        else
        {
            return false;
        }
    }
}
