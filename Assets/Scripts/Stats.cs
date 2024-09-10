using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterStats", menuName = "Stats", order = 0)]
public class Stats : ScriptableObject
{
    public string playerForm;
    public int maxHP;
    public float currentHP;
    public float maxMoveSpeed;
    public float baseMoveSpeed;
    public float moveSpeed;
    public float baseDamage;
    public float damage;
    public float maxDamage;
    public float baseShotDelay;
    public float shotDelay;
    public int killCount;
    public int upgradePoints;
}
