using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    [HideInInspector] public bool destroy = false;

    public void DestroyEntity()
    {
        destroy = true;
    }
}
