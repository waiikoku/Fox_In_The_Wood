using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Vector3 originOffset;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Transform target;
    private PlayerCharacter pc;
    private void Start()
    {
        originOffset = offset;
        pc = FindObjectOfType<PlayerCharacter>();
        pc.OnDied += ResetOffset;
    }

    public void SetOffset(Vector3 v)
    {
        offset = v;
    }

    public void ResetOffset()
    {
        offset = originOffset;
    }

    private void FixedUpdate()
    {
        transform.position = offset + target.position;
    }
}
