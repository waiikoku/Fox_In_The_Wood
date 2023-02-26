using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T _instance;
    public static T Instance => _instance;

    protected virtual void Awake()
    {
        Singleton<T> singleton = this;
        if(_instance == null)
        {
            _instance = singleton as T;
        }
        else
        {
            DestroyImmediate(this);
            return;
        }
        //print($"{gameObject.name} is Singleton");
    }
}
