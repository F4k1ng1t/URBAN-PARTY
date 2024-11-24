using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;

    public int damage;
    
    public int maxHP;
    public int  currentHP;
    
    public bool TakeDamage(int enemyDamage)
    {
        currentHP -= enemyDamage;
        if (currentHP >= 0)
        {
            currentHP = 0;
            return true;
        }
        return false;
    }
}
