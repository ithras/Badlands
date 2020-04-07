using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : Weapon
{
    [Header("Explosive")]
    [SerializeField] private float timer;

    public float GetTimer => timer;

}
