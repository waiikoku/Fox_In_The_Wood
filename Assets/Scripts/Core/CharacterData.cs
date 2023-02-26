using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Profile",menuName = "Character/Profile")]
public class CharacterData : ScriptableObject
{
    [SerializeField] private string characterName = "Null";

    [SerializeField] private int maxHp = 1;
    [SerializeField] private float maxSpeed = 1;
    [SerializeField] private float acceleration = 1;

    public int MaxHp => maxHp;
    public float MaxSpeed => maxSpeed;
    public float Acceleration => acceleration;
}

