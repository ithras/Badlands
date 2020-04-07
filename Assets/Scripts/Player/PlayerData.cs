using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int level;
    public int score;
    public int maxLevel;
    public int ZombiesKilled;

    public PlayerData(Player player)
    {
        level = player.level;
        score = player.score;
        maxLevel = player.maxLevel;
        ZombiesKilled = player.ZombiesKilled;
    }
}
