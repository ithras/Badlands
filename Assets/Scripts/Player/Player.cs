using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Gun primaryWeapon = null;
    [SerializeField] private Gun secondaryWeapon = null;
    [SerializeField] private Gun explosive = null;

    public Gun GetPrimaryWeapon => primaryWeapon;
    public Gun GetSecondaryWeapon => secondaryWeapon;
    public Gun GetExplosive => explosive;

    public int level;
    public int score;
    public int maxLevel;
    public int ZombiesKilled;
    public int maxScore;
    public int maxZombiesKilledInALevel;
}
